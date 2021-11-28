using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace SecureClient
{
	public class ServiceClientBase
	{
		// The Redirect URI is the URI where Azure AD will return OAuth responses.
		//
		// The AAD Instance is the instance of Azure, for example public Azure or Azure China.
		private static readonly string _AadInstance = ConfigurationManager.AppSettings["ida:AADInstance"];
		// The Tenant is the name of the Azure AD tenant in which this application is registered.
		private static readonly string _Tenant = ConfigurationManager.AppSettings["ida:Tenant"];
		// The Client ID is used by the application to uniquely identify itself to Azure AD.
		private static readonly string _ClientId = ConfigurationManager.AppSettings["ida:ClientId"];
		// The Authority is the sign-in URL of the tenant.
		private static readonly string _Authority = string.Format(CultureInfo.InvariantCulture, _AadInstance, _Tenant);

		// To authenticate to the service, the client needs to know the service's App ID URI and URL
		private static readonly string _AppAccessScope = ConfigurationManager.AppSettings["app:AppAcessScope"];
		private static readonly string _AppBaseAddress = ConfigurationManager.AppSettings["app:AppBaseAddress"];
		private static readonly string _AddressSlash = (_AppBaseAddress?.EndsWith("/") ?? false) ? "" : "/";
		private static readonly string[] _Scopes = { _AppAccessScope };

		private readonly IPublicClientApplication _App;
		private readonly string _Controller;
		private HttpClient _HttpClient = new HttpClient();
		private List<IAccount> _Accounts = new List<IAccount>();
		private AuthenticationResult _Result;


		protected long MaxResponseContentBufferSize { get; set; } = 1024 * 1024 * 20; //	20MB 
		protected TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(60);

		public ServiceClientBase(string controller)
		{
			_App = PublicClientApplicationBuilder.Create(_ClientId)
				  .WithAuthority(_Authority)
				  .WithRedirectUri("http://localhost") // needed only for the system browser
				  .Build();

			TokenCacheHelper.EnableSerialization(_App.UserTokenCache);

			SignIn().Wait();

			_Controller = controller;
		}

		JsonSerializerOptions SerialzationOptions =>
			new JsonSerializerOptions() {
				PropertyNameCaseInsensitive = true
			};

		private Uri ApiAddress
		{
			get
			{
				return new Uri($"{_AppBaseAddress}{_AddressSlash}{_Controller}");
			}
		}

		private async Task Authenticate(bool isAppStarting)
		{
			_Accounts = (await _App.GetAccountsAsync()).ToList();
			if (!_Accounts.Any())
			{
				//	Need to create an account
				await ShowAuthenticate();
			}

			await AcquireToken();

			// Once the token has been returned by MSAL, add it to the http authorization header, before making the call to access the app service.
			_HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _Result.AccessToken);

		}

		private Uri GetTarget(string relative)
		{
			string slash = relative.StartsWith("/") ? "" : "/";
			return new Uri($"{ApiAddress}{slash}{relative}");
		}

		private async Task AcquireToken()
		{
			try
			{
				_Result = await _App.AcquireTokenSilent(_Scopes, _Accounts.FirstOrDefault())
									.ExecuteAsync()
									.ConfigureAwait(false);
			}
			// There is no access token in the cache, so prompt the user to sign-in.
			catch (MsalUiRequiredException)
			{
			}
			catch (MsalException ex)
			{
				// An unexpected error occurred.
			}

			return;
		}

		private async Task SignIn()
		{
			foreach (var account in _Accounts)
			{
				await _App.RemoveAsync(account);
			}
			_Accounts.Clear();

			// Get an access token to call the To Do list service.
			try
			{
				await Authenticate(false);
			}
			catch (MsalUiRequiredException)
			{
				try
				{
					await ShowAuthenticate().ConfigureAwait(false);
				}
				catch (MsalException ex)
				{
					if (ex.ErrorCode == "access_denied")
					{
						// The user canceled sign in, take no action.
					}
					else
					{
						// An unexpected error occurred.
						string message = ex.Message;
						if (ex.InnerException != null)
						{
							message += "Error Code: " + ex.ErrorCode + "Inner Exception : " + ex.InnerException.Message;
						}

					}
					//	User has failed to login.
				}
			}
		}

		private async Task ShowAuthenticate()
		{
			// Force a sign-in (Prompt.SelectAccount), as the MSAL web browser might contain cookies for the current user
			// and we don't necessarily want to re-sign-in the same user
			var builder = _App.AcquireTokenInteractive(_Scopes)
				.WithAccount(_Accounts.FirstOrDefault())
				.WithPrompt(Prompt.SelectAccount);

			// in this case, the redirect uri needs to be set to "http://localhost"
			builder = builder.WithUseEmbeddedWebView(true);

			var result = await builder.ExecuteAsync();
		}

		async public Task<TDto> Fetch<TDto>(string targetRelativeUri) where TDto : class
		{
			Uri target = GetTarget(targetRelativeUri);
			try
			{
				var response = await _HttpClient.GetAsync(target);

				response.EnsureSuccessStatusCode();
				var str = await response.Content.ReadAsStringAsync();
				var result = System.Text.Json.JsonSerializer.Deserialize<TDto>(str, SerialzationOptions);
				return result;
			}
			catch (Exception ex)
			{
				//	Log this exception
				return null;
			}
		}

		async public Task<IEnumerable<TDto>> FetchList<TDto>(string targetRelativeUri) where TDto : class
		{
			Uri target = GetTarget(targetRelativeUri);
			try
			{
				var response = await _HttpClient.GetAsync(target);
				response.EnsureSuccessStatusCode();

				var str = await response.Content.ReadAsStringAsync();
				var result = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<TDto>>(str, SerialzationOptions);
				return result;
			}
			catch (Exception ex)
			{
				//	Log this exception
				return null;
			}
		}

		async public Task<bool> Update<TDto>(string targetRelativeUri, TDto targetData) where TDto : class
		{
			Uri target = GetTarget(targetRelativeUri);
			try
			{
				HttpContent content = JsonContent.Create<TDto>(targetData, null, SerialzationOptions);
				var response = await _HttpClient.PatchAsync(target, content);

				response.EnsureSuccessStatusCode();
				return true;
			}
			catch (Exception ex)
			{
				//	Log this exception
				return false;
			}
		}

		async public Task<int> Insert<TDto>(string targetRelativeUri, TDto targetData) where TDto : class
		{
			Uri target = GetTarget(targetRelativeUri);
			try
			{
				HttpContent content = JsonContent.Create<TDto>(targetData, null, SerialzationOptions);
				var response = await _HttpClient.PutAsync(target, content);

				response.EnsureSuccessStatusCode();
				return int.Parse(await response.Content.ReadAsStringAsync());
			}
			catch (Exception ex)
			{
				//	Log this exception
				return -1;
			}
		}

		async public Task<bool> Delete(string targetRelativeUri)
		{
			Uri target = GetTarget(targetRelativeUri);
			try
			{
				var response = await _HttpClient.DeleteAsync(target);

				response.EnsureSuccessStatusCode();

				return true;
			}
			catch (Exception ex)
			{
				//	Log this exception
				return false;
			}
		}

		async public Task<bool> Upload<TDto>(string targetRelativeUri, TDto typeToUpload)
		{
			Uri target = GetTarget(targetRelativeUri);
			try
			{
				HttpContent content = JsonContent.Create<TDto>(typeToUpload, null, SerialzationOptions);
				var response = await _HttpClient.PostAsync(target, content);
				response.EnsureSuccessStatusCode();

				return true;
			}
			catch (Exception ex)
			{
				//	Log this exception
				return false;
			}
		}



	}
}
