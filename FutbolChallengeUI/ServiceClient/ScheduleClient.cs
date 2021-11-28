using FutbolChallenge.Data.Model;
using FutbolChallengeDataRepository.Composites;
using SecureClient;
using System.Threading.Tasks;

namespace FutbolChallengeUI.ServiceClient
{
	public interface IFutbolChallengeScheduleServiceClient
	{
		Task<bool> UploadScheduledGames(int seasonId, ScheduleComposite scheduledGames);

		Task<bool> DeleteMatch(int id);

		Task<bool> UpdateMatch(ScheduledGame match);

		Task<int> InsertMatch(ScheduledGame match);

	}

	public class FutbolChallengeScheduleServiceClient : ServiceClientBase, IFutbolChallengeScheduleServiceClient
	{
		public FutbolChallengeScheduleServiceClient() : base("schedule")
		{
		}

		async public Task<bool> UploadScheduledGames(int seasonId, ScheduleComposite seasonSchedule)
		{
			var targetRelativeUri = $"upload-schedule/{seasonId}";
			var result = await Upload(targetRelativeUri, seasonSchedule);
			return result;
		}

		async public Task<bool> DeleteMatch(int id)
		{
			var targetRelativeUri = $"delete-game/{id}";
			var result = await Delete(targetRelativeUri);
			return result;
		}

		async public Task<bool> UpdateMatch(ScheduledGame match)
		{
			var targetRelativeUri = $"update-game/{match.Id}";
			var result = await Update(targetRelativeUri, match.ToDataModel());
			return result;
		}

		async public Task<int> InsertMatch(ScheduledGame match)
		{
			var targetRelativeUri = "insert-game";

			var result = await Insert(targetRelativeUri, match.ToDataModel());
			return result;
		}

	}
}