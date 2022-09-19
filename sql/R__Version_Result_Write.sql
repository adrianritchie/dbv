Create Or Alter Procedure [dbv].[Version_Result_Write]
  (
  @Schema nVarChar(500),
  @Version nVarChar(50),
  @Script nVarChar(500),
  @Results nVarChar(Max),
  @HasErrors bit
)
As
Begin

  Update
			[dbv].[Version]
		Set
			Results = @Results,
			HasErrors = @HasErrors
		Where
			[Schema] = @Schema
    AND [Script] = @Script
    And [Version] = @Version
    And IsRolledBack = 0
End
Go