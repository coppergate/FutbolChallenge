using FutbolChallenge.Data.Dto;
using FutbolChallenge.Data.Model;
using Helpers.Core;
using SecureClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FutbolChallengeUI.ServiceClient
{

	public interface IFutbolChallengeParticipantServiceClient
	{
		Task<Participant> FetchParticipant(int Id);
		Task<IEnumerable<Participant>> FetchAllParticipants();

		Task<bool> DeleteParticipant(int Id);

		Task<bool> UpdateParticipant(Participant data);

		Task<int> InsertParticipant(Participant data);
	}

	public class FutbolChallengeParticipantServiceClient : ServiceClientBase, IFutbolChallengeParticipantServiceClient
	{
		public FutbolChallengeParticipantServiceClient(HelperConfiguration clientConfiguration) 
			: base("participant")
		{
			MaxResponseContentBufferSize = Convert.ToInt64(clientConfiguration.GetValue("MaxResponseContentBufferSize"));
			Timeout = TimeSpan.FromSeconds(Convert.ToDouble(clientConfiguration.GetValue("TimeoutInSeconds")));
		}

		async public Task<Participant> FetchParticipant(int Id)
		{
			var targetRelativeUri = $"/fetch/{Id}";
			var participant = await Fetch<ParticipantDto>(targetRelativeUri);
			return Participant.FromDataModel(participant);
		}

		async public Task<IEnumerable<Participant>> FetchAllParticipants()
		{
			var targetRelativeUri = "/all-participants";
			var participants = await FetchList<ParticipantDto>(targetRelativeUri);
			return participants?.Select(p => Participant.FromDataModel(p)) ?? Enumerable.Empty<Participant>();
		}

		async public Task<bool> DeleteParticipant(int Id)
		{
			var targetRelativeUri = $"/delete/{Id}";
			var result = await Delete(targetRelativeUri);
			return result;
		}

		async public Task<bool> UpdateParticipant(Participant data)
		{
			var targetRelativeUri = $"/update/{data.Id}";
			var result = await Update(targetRelativeUri, data.ToDataModel());
			return result;
		}

		async public Task<int> InsertParticipant(Participant data)
		{
			var targetRelativeUri = "/insert";

			var result = await Insert(targetRelativeUri, data.ToDataModel());
			return result;
		}
	}
}
