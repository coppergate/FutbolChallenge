CREATE VIEW dbo.ParticipantGamePredictions
AS
SELECT        dbo.ParticipatingInSeason.SeasonId, dbo.ParticipantPrediction.ParticipantId, dbo.Participant.FirstName, dbo.Participant.LastName, dbo.MatchGroup.MatchGroupSequence, dbo.MatchGroup.EndDate, dbo.MatchGroup.StartDate, 
                         dbo.ParticipantPrediction.ScheduledGameId, HomeTeam.Name AS HomeTeamName, dbo.ParticipantPrediction.HomeTeamScore AS HomeTeamPredictedResult, dbo.ScheduledGame.HomeTeamScore AS HomeTeamActualResult,
                          VistingTeam.Name AS VisitingTeamName, dbo.ParticipantPrediction.VisitingTeamScore AS VisitingTeamPredictedResult, dbo.ScheduledGame.VisitingTeamScore AS VisitingTeamActualResult
FROM            dbo.Season INNER JOIN
                         dbo.MatchGroup ON dbo.MatchGroup.SeasonId = dbo.Season.Id INNER JOIN
                         dbo.ParticipatingInSeason ON dbo.Season.Id = dbo.ParticipatingInSeason.SeasonId INNER JOIN
                         dbo.Participant ON dbo.ParticipatingInSeason.ParticipantId = dbo.Participant.Id INNER JOIN
                         dbo.ParticipantPrediction ON dbo.Participant.Id = dbo.ParticipantPrediction.ParticipantId INNER JOIN
                         dbo.ScheduledGame ON dbo.ParticipantPrediction.ScheduledGameId = dbo.ScheduledGame.Id AND dbo.MatchGroup.Id = dbo.ScheduledGame.MatchGroupId INNER JOIN
                         dbo.Team AS HomeTeam ON dbo.ScheduledGame.HomeTeamId = HomeTeam.Id INNER JOIN
                         dbo.Team AS VistingTeam ON dbo.ScheduledGame.VisitingTeamId = VistingTeam.Id
WHERE        (dbo.ParticipatingInSeason.Removed IS NULL)
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ParticipantGamePredictions';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'           End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "MatchGroup"
            Begin Extent = 
               Top = 151
               Left = 1143
               Bottom = 313
               Right = 1350
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 10
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 3015
         Alias = 1755
         Table = 2355
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ParticipantGamePredictions';




GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[10] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Season"
            Begin Extent = 
               Top = 0
               Left = 1587
               Bottom = 155
               Right = 1757
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ParticipatingInSeason"
            Begin Extent = 
               Top = 368
               Left = 1394
               Bottom = 571
               Right = 1602
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Participant"
            Begin Extent = 
               Top = 461
               Left = 911
               Bottom = 629
               Right = 1081
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ParticipantPrediction"
            Begin Extent = 
               Top = 233
               Left = 350
               Bottom = 419
               Right = 536
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ScheduledGame"
            Begin Extent = 
               Top = 27
               Left = 769
               Bottom = 230
               Right = 955
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "HomeTeam"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 119
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "VistingTeam"
            Begin Extent = 
               Top = 140
               Left = 51
               Bottom = 283
               Right = 221
 ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ParticipantGamePredictions';



