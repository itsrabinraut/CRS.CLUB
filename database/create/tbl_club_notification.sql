USE [CRS]
GO

/****** Object:  Table [dbo].[tbl_customer_notification]    Script Date: 11/4/2023 6:55:54 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tbl_club_notification](
	[Sno] [bigint] IDENTITY(1,2) PRIMARY KEY,
	[notificationId] [bigint] NULL,
	[ToAgentId] [bigint] NULL,
	[NotificationType] [varchar](200) NULL,
	[NotificationSubject] [varchar](512) NULL,
	[NotificationBody] [varchar](512) NULL,
	[NotificationImageURL] [varchar](512) NULL,
	[NotificationStatus] [char](1) NULL,
	[NotificationReadStatus] [char](1) NULL,
	[CreatedBy] [varchar](200) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [varchar](200) NULL,
	[UpdatedDate] [datetime] NULL,
)

