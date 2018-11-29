USE [BetterTeams]
GO

/****** Object:  Table [dbo].[Messages]    Script Date: 29/11/2018 7:47:51 μμ ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Messages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Sender] [nvarchar](50) NOT NULL,
	[Receiver] [nvarchar](50) NOT NULL,
	[Message] [varchar](250) NOT NULL,
	[DateTime] [date] NOT NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_Users] FOREIGN KEY([Sender])
REFERENCES [dbo].[Users] ([Email])
GO

ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_Users]
GO

