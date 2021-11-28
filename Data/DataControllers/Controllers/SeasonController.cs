using Exceptions;
using FutbolChallenge.Data.Dto;
using FutbolChallenge.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web.Resource;
using System.Threading.Tasks;

namespace DataControllers.Controllers
{
	//[Authorize]
	//[RequiredScope("access_as_admin")]
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

		[HttpGet("all-seasons")]
		public async Task<IActionResult> GetAllSeasons()
		{
			var ret = await _repositoryProvider.SeasonRepository.GetList("", null);
			return Ok(ret);
		}

		[HttpGet("get-all-games/{seasonId}")]
		public async Task<IActionResult> GetSeasonGames(int seasonId)
		{
			var where = "SeasonId = @seasonId";
			var ret = await _repositoryProvider.SeasonGameRepository.GetList(where, new { seasonId });
			return Ok(ret);
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

		[HttpGet("season-details/{seasonId}")]
		public async Task<IActionResult> GetSeasonDetails(int seasonId)
		{
			var ret = await _repositoryProvider.SeasonDetailRepository.Get($"id = {seasonId}", null);
			return Ok(ret);
		}


	}
}
