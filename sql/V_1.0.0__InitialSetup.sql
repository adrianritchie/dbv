If Not Exists (Select *
From sys.schemas
Where name = 'dbv')
Begin
  Exec('Create Schema [dbv] Authorization [dbo]')
End
Go


If Object_Id('[dbv].[Version]') Is Null
Begin

  Create Table [dbv].[Version]
  (
    [Id] [bigint] IDENTITY(1,1) NOT NULL,
    [Guid] [uniqueidentifier] NOT NULL,
    [Schema] [nvarchar](500) NOT NULL,
    [Version] [nvarchar](50) NOT NULL,
    [Script] [nvarchar](500) NOT NULL,
    [DateExecutedUTC] [datetime2](7) NOT NULL,
    [Results] [nvarchar](max) NULL,
    [IsRolledBack] [bit] NOT NULL,
    [HasErrors] [bit] NOT NULL,
    CONSTRAINT [PK_Version] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
  ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

  ALTER TABLE [dbv].[Version] ADD  CONSTRAINT [DF_Version_Guid]  DEFAULT (newid()) FOR [Guid]

  ALTER TABLE [dbv].[Version] ADD  CONSTRAINT [DF_Version_IsRolledBack]  DEFAULT ((0)) FOR [IsRolledBack]

  ALTER TABLE [dbv].[Version] ADD  CONSTRAINT [DF_Version_HasErrors]  DEFAULT ((0)) FOR [HasErrors]
End
Go