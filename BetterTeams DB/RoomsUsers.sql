USE [BetterTeams]
GO

/****** Object:  Table [dbo].[RoomsUsers]    Script Date: 5/12/2018 19:14:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RoomsUsers](
	[RoomId] [int] NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_RoomsUsers] PRIMARY KEY CLUSTERED 
(
	[RoomId] ASC,
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[RoomsUsers]  WITH CHECK ADD  CONSTRAINT [FK_RoomsUsers_Rooms] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Rooms] ([Id])
GO

ALTER TABLE [dbo].[RoomsUsers] CHECK CONSTRAINT [FK_RoomsUsers_Rooms]
GO

ALTER TABLE [dbo].[RoomsUsers]  WITH CHECK ADD  CONSTRAINT [FK_RoomsUsers_Users] FOREIGN KEY([Email])
REFERENCES [dbo].[Users] ([Email])
GO

ALTER TABLE [dbo].[RoomsUsers] CHECK CONSTRAINT [FK_RoomsUsers_Users]
GO

