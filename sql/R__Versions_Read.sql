Create Or Alter Procedure [dbv].[Versions_Read]
  (
  @Schema nVarChar(500)
)
As
Begin

  Select Distinct
    [Version]
  From
    [dbv].[Version]
  Where
			[Schema] = @Schema
    And IsRolledBack = 0
End
Go
