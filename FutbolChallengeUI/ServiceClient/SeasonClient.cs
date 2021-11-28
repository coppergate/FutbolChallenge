using FutbolChallenge.Data.Dto;
using FutbolChallenge.Data.Model;
using SecureClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FutbolChallengeUI.ServiceClient
{
	public interface IFutbolChallengeSeasonServiceClient
	{
		Task<IEnumerable<SeasonGame>> GetSeasonGames(int seasonId);

		Task<IEnumerable<Season>> FetchAllSeasons();

		Task<SeasonDetail> FetchSeasonDetails(int seasionId);

		Task<bool> UpdateSeason(Season season);

	}

	public class FutbolChallengeSeasonServiceClient : ServiceClientBase, IFutbolChallengeSeasonServiceClient
	{
		public FutbolChallengeSeasonServiceClient() : base("season")
		{
		}

		async public Task<IEnumerable<Season>> FetchAllSeasons()
		{
			var targetRelativeUri = "all-seasons";
			var seasons = await FetchList<SeasonDto>(targetRelativeUri);
			return seasons?.Select(p => Season.FromDataModel(p)) ?? Enumerable.Empty<Season>();
		}


		async public Task<IEnumerable<SeasonGame>> GetSeasonGames(int seasonId)
		{
			var targetRelativeUri = $"get-all-games/{seasonId}";
			var result = await FetchList<SeasonGameDto>(targetRelativeUri);
			return result?.Select(g => SeasonGame.FromDataModel(g)) ?? Enumerable.Empty<SeasonGame>();
		}


		async public Task<SeasonDetail> FetchSeasonDetails(int seasonId)
		{
			var targetRelativeUri = $"season-details/{seasonId}";
			var result = await Fetch<SeasonDetailDto>(targetRelativeUri);
			return SeasonDetail.FromDataModel(result);
		}

		async public Task<bool> UpdateSeason(Season season)
		{
			var targetRelativeUri = $"update/{season.Id}";
			var result = await Update(targetRelativeUri, season.ToDataModel());
			return result;
		}
	}
}
