using FutbolChallenge.Data.Repository.Dto;
using FutbolChallenge.Data.Repository.Model;
using FutbolChallenge.Data.Repository.Repository;
using Helpers.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FutbolChallengeUI
{

	public interface IFutbolChallengeServiceClient
	{
		Task<Participant> FetchParticipant(int Id);
		Task<IEnumerable<Participant>> FetchAllParticipants();

		Task<bool> DeleteParticipant(int Id);

		Task<bool> UpdateParticipant(Participant data);

		Task<int> InsertParticipant(Participant data);

		Task<IEnumerable<ScheduledGame>> UploadScheduledGames(int seasonId, System.IO.Stream scheduledGames);

		Task<IEnumerable<ScheduledGame>> GetSeasonGames(int seasonId);
	
		Task<IEnumerable<Season>> FetchAllSeasons();

	}

	public class FutbolChallengeServiceClient : ServiceClientBase, IFutbolChallengeServiceClient
	{
		private readonly IDataRepositoryProvider _DataRepositoryProvider;

		public FutbolChallengeServiceClient(HelperConfiguration clientConfiguration, IDataRepositoryProvider dataRepositoryProvider) : base()
		{
			_DataRepositoryProvider = dataRepositoryProvider;

			this.BaseAddress = new Uri(clientConfiguration.GetValue("DataEndpointUrl"));
			this.MaxResponseContentBufferSize = Convert.ToInt64(clientConfiguration.GetValue("MaxResponseContentBufferSize"));
			this.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(clientConfiguration.GetValue("TimeoutInSeconds")));
			//		this.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(DefaultRequestHeaders.Authorization.Scheme);

		}

		async public Task<Participant> FetchParticipant(int Id)
		{
			var targetRelativeUri = $"participant/fetch/{Id}";
			var participant = await Fetch<ParticipantDto>(targetRelativeUri);
			return Participant.FromDataModel(participant);
		}

		async public Task<IEnumerable<Participant>> FetchAllParticipants()
		{
			var targetRelativeUri = $"participant/all-participants";
			var participants = await FetchList<ParticipantDto>(targetRelativeUri);
			return participants?.Select(p => Participant.FromDataModel(p)) ?? Enumerable.Empty<Participant>();
		}

		async public Task<bool> DeleteParticipant(int Id)
		{
			var targetRelativeUri = $"participant/delete/{Id}";
			var result = await Delete(targetRelativeUri);
			return result;
		}

		async public Task<bool> UpdateParticipant(Participant data)
		{
			var targetRelativeUri = $"participant/update/{data.Id}";
			var result = await Update(targetRelativeUri, data.ToDataModel());
			return result;
		}

		async public Task<int> InsertParticipant(Participant data)
		{
			var targetRelativeUri = $"participant/insert";

			var result = await Insert(targetRelativeUri, data.ToDataModel());
			return result;
		}


		async public Task<IEnumerable<Season>> FetchAllSeasons()
		{
			var targetRelativeUri = $"schedule/all-seasons";
			var seasons = await FetchList<SeasonDto>(targetRelativeUri);
			return seasons?.Select(p => Season.FromDataModel(p)) ?? Enumerable.Empty<Season>();
		}

		async public Task<IEnumerable<ScheduledGame>> UploadScheduledGames(int seasonId, System.IO.Stream scheduledGames)
		{
			var targetRelativeUri = $"schedule/upload-schedule/{seasonId}";
			var result = await UploadStream(targetRelativeUri, scheduledGames);
			if(result == true)
			{
				var fetchResult = await GetSeasonGames(seasonId);
				return fetchResult;
			}
			return Enumerable.Empty<ScheduledGame>();
		}

		async public Task<IEnumerable<ScheduledGame>> GetSeasonGames(int seasonId)
		{
			var targetRelativeUri = $"schedule/season-schedule/{seasonId}";
			var result = await FetchList<ScheduledGameDto>(targetRelativeUri);
			return result?.Select(g => ScheduledGame.FromDataModel(g));
		}

	}

}
