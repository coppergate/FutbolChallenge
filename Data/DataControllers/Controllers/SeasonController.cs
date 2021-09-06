using Exceptions;
using FutbolChallenge.Data.Dto;
using FutbolChallenge.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DataControllers.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class SeasonController : ControllerBase
	{
		private readonly ILogger<SeasonController> _logger;
		private readonly IDataRepositoryProvider _repositoryProvider;

		public SeasonController(ILogger<SeasonController> logger, IDataRepositoryProvider repoProvider)
		{
			_logger = logger;
			_repositoryProvider = repoProvider;
		}

		[HttpPatch("update/{id}")]
		public async Task<bool> UpdateSeason(int id, [FromBody] SeasonDto seasonDto)
		{
			if (id != seasonDto.Id)
			{
				throw new InvalidArgumentException($"REST path id disagrees with season id");
			}
			var ret = await _repositoryProvider.SeasonRepository.Update(seasonDto);
			return ret;
		}

	}
}
