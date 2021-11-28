using FutbolChallenge.Data.Dto;
using FutbolChallenge.Data.Model;
using SecureClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FutbolChallengeUI.ServiceClient
{

	public interface IFutbolChallengeTeamServiceClient
	{
		Task<IEnumerable<Team>> FetchAllTeams();
	}

	public class FutbolChallengeTeamServiceClient : ServiceClientBase, IFutbolChallengeTeamServiceClient
	{
		public FutbolChallengeTeamServiceClient() : base("team")
		{
		}

		async public Task<IEnumerable<Team>> FetchAllTeams()
		{
			var targetRelativeUri = "all-teams";
			var seasons = await FetchList<TeamDto>(targetRelativeUri);
			return seasons?.Select(p => Team.FromDataModel(p)) ?? Enumerable.Empty<Team>();
		}


	}
}
