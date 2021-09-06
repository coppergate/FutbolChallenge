﻿using FutbolChallenge.Data.Dto;
using FutbolChallenge.Data.Repository;
using FutbolChallengeDataRepository.Composites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataControllers.Controllers
{

	[ApiController]
	[Route("[controller]")]
	public class ScheduleController : ControllerBase
	{
		private readonly ILogger<ScheduleController> _logger;
		private readonly IDataRepositoryProvider _repositoryProvider;

		public ScheduleController(ILogger<ScheduleController> logger, IDataRepositoryProvider repoProvider)
		{
			_logger = logger;
			_repositoryProvider = repoProvider;
		}

		[HttpGet("full-schedule")]
		public async Task<IActionResult> GetFullSchedule()
		{
			var ret = await _repositoryProvider.SeasonScheduleRepository.GetList("", null);
			return Ok(ret);
		}

		[HttpGet("all-seasons")]
		public async Task<IActionResult> GetAllSeasons()
		{
			var ret = await _repositoryProvider.SeasonRepository.GetList("", null);
			return Ok(ret);
		}

		[HttpPost("upload-schedule/{seasonId}")]
		public async Task<IActionResult> UploadSchedule(int seasonId, [FromBody] ScheduleComposite scheduleData)
		{
			SeasonDto seasonDto = new() {
				Id = seasonId,
				Name = scheduleData.SeasonName,
				StartDate = scheduleData.SeasonStart,
				EndDate = scheduleData.SeasonEnd,
			};

			var seasonUpdateRet = await _repositoryProvider.SeasonRepository.MergeWithKeep(seasonDto);
			if (seasonUpdateRet != 0)
			{
				//	This is a problem... the merge with keep should return 0 if the merge updates
				//	if it didn't that means we just inserted a new season which shouldn't happen when uploading a 
				//	schedule for a season.
			}

			foreach (var group in scheduleData.SeasonGroups)
			{
				MatchGroupDto matchGroupDto = new() {
					MatchGroupSequence = group.Sequence,
					SeasonId = seasonId,
					MatchGroupTitle = group.Name,
					StartDate = group.GroupStart,
					EndDate = group.GroupEnd,
				};
				var matchGroup = await _repositoryProvider.MatchGroupRepository.Insert(matchGroupDto);

				Dictionary<string, int> teamMap = new Dictionary<string, int>();
				foreach (var match in group.Games)
				{
					int homeTeamId;
					if (!teamMap.TryGetValue(match.HomeTeam, out homeTeamId))
					{
						var homeTeamDto = await _repositoryProvider.TeamRepository.Get($"[Name] = @TeamName", new { TeamName = match.HomeTeam })
							?? new();

						if (homeTeamDto.Id == 0)
						{
							homeTeamDto.Name = match.HomeTeam;
							homeTeamDto.Id = await _repositoryProvider.TeamRepository.Insert(homeTeamDto);
						}
						homeTeamId= homeTeamDto.Id;
						teamMap.Add(match.HomeTeam, homeTeamId);
					}

					int awayTeamId;
					if (!teamMap.TryGetValue(match.AwayTeam, out awayTeamId))
					{
						var awayTeamDto = await _repositoryProvider.TeamRepository.Get($"[Name] = @TeamName", new { TeamName = match.AwayTeam })
							?? new();

						if (awayTeamDto.Id == 0)
						{
							awayTeamDto.Name = match.AwayTeam;
							awayTeamDto.Id = await _repositoryProvider.TeamRepository.Insert(awayTeamDto);
						}
						awayTeamId = awayTeamDto.Id;
						teamMap.Add(match.AwayTeam, awayTeamId);
					}

					ScheduledGameDto scheduledGame = new() {
						HomeTeamId = homeTeamId,
						VisitingTeamId = awayTeamId,
						MatchDate = match.GameDate,
						MatchGroupId = matchGroup,
					};
					var game = await _repositoryProvider.ScheduledGameRepository.Insert(scheduledGame);
				}

			}

			var ret = await _repositoryProvider.SeasonScheduleRepository.GetList($"SeasonId = @seasonId", new { seasonId } );
			return Ok(ret);
		}

		[HttpGet("season-details/{seasonId}")]
		public async Task<IActionResult> GetSeasonDetails(int seasonId)
		{
			var ret = await _repositoryProvider.SeasonDetailRepository.Get($"id = {seasonId}", null);
			return Ok(ret);
		}



	}
}
