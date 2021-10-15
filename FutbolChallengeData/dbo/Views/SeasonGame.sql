CREATE VIEW dbo.SeasonGame
AS
SELECT        dbo.Season.Id AS SeasonId, dbo.Season.Name AS SeasonName, dbo.MatchGroup.MatchGroupSequence, dbo.ScheduledGame.MatchDate, HomeTeam.Stadium, HomeTeam.Name AS HomeTeam, 
                         VisitingTeam.Name AS VistingTeam, dbo.ScheduledGame.HomeTeamScore, dbo.ScheduledGame.VisitingTeamScore, HomeTeam.Id AS HomeTeamId, VisitingTeam.Id AS VisitingTeamId, dbo.MatchGroup.Id AS MatchGroupId, 
                         dbo.MatchGroup.MatchGroupTitle, dbo.MatchGroup.StartDate AS MatchGroupStartDate, dbo.MatchGroup.EndDate AS MatchGroupEndDate, dbo.ScheduledGame.Id AS MatchId
FROM            dbo.ScheduledGame INNER JOIN
                         dbo.MatchGroup ON dbo.ScheduledGame.MatchGroupId = dbo.MatchGroup.Id INNER JOIN
                         dbo.Season ON dbo.MatchGroup.SeasonId = dbo.Season.Id INNER JOIN
                         dbo.Team AS HomeTeam ON dbo.ScheduledGame.HomeTeamId = HomeTeam.Id INNER JOIN
                         dbo.Team AS VisitingTeam ON dbo.ScheduledGame.VisitingTeamId = VisitingTeam.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'SeasonGame';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'  Alias = 2055
         Table = 1980
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'SeasonGame';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
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
         Begin Table = "ScheduledGame"
            Begin Extent = 
               Top = 186
               Left = 650
               Bottom = 395
               Right = 836
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "MatchGroup"
            Begin Extent = 
               Top = 253
               Left = 368
               Bottom = 475
               Right = 575
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Season"
            Begin Extent = 
               Top = 199
               Left = 54
               Bottom = 329
               Right = 224
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "HomeTeam"
            Begin Extent = 
               Top = 142
               Left = 1124
               Bottom = 343
               Right = 1294
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "VisitingTeam"
            Begin Extent = 
               Top = 354
               Left = 1287
               Bottom = 467
               Right = 1457
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
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 2325
         Width = 2985
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 2460
       ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'SeasonGame';

