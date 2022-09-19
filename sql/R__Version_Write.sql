Create Or Alter Procedure [dbv].[Version_Write]
  (
  @Schema nVarChar(500),
  @Version nVarChar(50),
  @Script nVarChar(500)
)
As
Begin

  Insert Into
			[dbv].[Version]
    (
    [Schema],
    [Version],
    [Script],
    [DateExecutedUTC]
    )
  Values
    (
      @Schema,
      @Version,
      @Script,
      GetUTCDate()
			)
End
Go