USE [BetterTeams]
GO

/****** Object:  Table [dbo].[Posts]    Script Date: 14/12/2018 11:32:05 πμ ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Posts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Sender] [nvarchar](50) NOT NULL,
	[PostText] [nvarchar](250) NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[Room] [nvarchar](50) NOT NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_Posts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Posts]  WITH CHECK ADD  CONSTRAINT [FK_Posts_Rooms] FOREIGN KEY([Room])
REFERENCES [dbo].[Rooms] ([Name])
GO

ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [FK_Posts_Rooms]
GO

ALTER TABLE [dbo].[Posts]  WITH CHECK ADD  CONSTRAINT [FK_Posts_Users] FOREIGN KEY([Sender])
REFERENCES [dbo].[Users] ([Email])
GO

ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [FK_Posts_Users]
GO

