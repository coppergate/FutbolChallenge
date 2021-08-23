using Exceptions;
using FutbolChallenge.Data.Dto;
using FutbolChallenge.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataControllers.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ParticipantController : ControllerBase
	{


		private readonly ILogger<TeamController> _logger;
		private readonly IDataRepositoryProvider _repositoryProvider;

		public ParticipantController(ILogger<TeamController> logger, IDataRepositoryProvider repoProvider)
		{
			_logger = logger;
			_repositoryProvider = repoProvider;
		}

		[HttpGet("all-participants")]
		public async Task<IEnumerable<ParticipantDto>> GetAllParticipants()
		{
			var ret = await _repositoryProvider.ParticipantRepository.GetList("", null);
			return ret;
		}

		[HttpGet("fetch/{id}")]
		public async Task<ParticipantDto> GetParticipant(int id)
		{
			var ret = await _repositoryProvider.ParticipantRepository.Get(id);
			return ret;
		}

		[HttpDelete("delete/{id}")]
		public async Task<bool> DeleteParticipant(int id)
		{
			var ret = await _repositoryProvider.ParticipantRepository.Delete(new ParticipantDto() { Id = id });
			return ret;
		}

		[HttpPatch("update/{id}")]
		public async Task<bool> UpdateParticipant(int id, [FromBody] ParticipantDto participant)
		{
			if(id != participant.Id)
			{
				throw new InvalidArgumentException($"REST path id disagrees with participant id");
			}
			var ret = await _repositoryProvider.ParticipantRepository.Update(participant);
			return ret;
		}

		[HttpPut("insert")]
		public async Task<int> InsertParticipant([FromBody] ParticipantDto participant)
		{
			var ret = await _repositoryProvider.ParticipantRepository.Insert(participant);
			return ret;
		}
	}
}
