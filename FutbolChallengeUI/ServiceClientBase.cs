using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace FutbolChallengeUI
{
	public class ServiceClientBase : HttpClient
	{

		public ServiceClientBase() : base() { }

		JsonSerializerOptions SerialzationOptions =>
			new JsonSerializerOptions() {
				PropertyNameCaseInsensitive = true
			};

		private Uri GetTarget(string relative)
		{
			return new Uri(this.BaseAddress, relative);
		}

		async public Task<TDto> Fetch<TDto>(string targetRelativeUri) where TDto : class
		{
			Uri target = GetTarget(targetRelativeUri);
			try
			{
				var response = await this.GetAsync(target);

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
				var response = await this.GetAsync(target);
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
				var response = await this.PatchAsync(target, content);

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
				var response = await this.PutAsync(target, content);

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
				var response = await this.DeleteAsync(target);

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
				var response = await this.PostAsync(target, content);
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
