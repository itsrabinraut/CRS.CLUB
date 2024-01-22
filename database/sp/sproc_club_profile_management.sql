USE [CRS]
GO

/****** Object:  StoredProcedure [dbo].[sproc_customer_profile_management]    Script Date: 11/1/2023 10:35:18 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[sproc_club_profile_management] (
	@Flag CHAR(5) = NULL
	,@ClubNameEng NVARCHAR(400) = NULL
	,@ClubNameJap NVARCHAR(200) = NULL
	,@GroupName NVARCHAR(200) = NULL
	,@Username VARCHAR(20) = NULL
	,@RoleId VARCHAR(20) = NULL
	,@EmailAddress VARCHAR(512) = NULL
	,@MobileNumber VARCHAR(15) = NULL
	,@Bio VARCHAR(512) = NULL
	,@ProfilePicture VARCHAR(500) = NULL
	,@CoverPicture VARCHAR(500) = NULL
	,@Twitter VARCHAR(10) = NULL
	,@Instagram VARCHAR(200) = NULL
	,@TikTok VARCHAR(200) = NULL
	,@Website VARCHAR(200) = NULL
	,@GoogleMaps VARCHAR(200) = NULL
	,@FullName VARCHAR(150) = NULL
	,@Password VARCHAR(50) = NULL
	,@WorkingHoursFrom VARCHAR(150) = NULL
	,@WorkingHoursTo VARCHAR(150) = NULL
	,@Holiday VARCHAR(150) = NULL
	,@Zip VARCHAR(150) = NULL
	,@Prefecture VARCHAR(10) = NULL
	,@City VARCHAR(100) = NULL
	,@Street VARCHAR(100) = NULL
	,@BuildingAndRoomNo VARCHAR(200) = NULL
	,@Session VARCHAR(200) = NULL
	,@UserId VARCHAR(200) = NULL
	,@AgentId VARCHAR(200) = NULL
	,@ActionUser VARCHAR(200) = NULL
	,@ActionIp VARCHAR(50) = NULL
	,@ActionPlatform VARCHAR(20) = NULL
	)
AS
BEGIN
	SET NOCOUNT ON;

	--SELECT USER PROFILE DETAIL
	IF @Flag = 's'
	BEGIN
		SELECT ClubName1
			,ClubName2
			,Email
			,MobileNumber
			,GroupName
			,Description Bio
			,LocationURL
			,Longitude
			,Latitude
			,STATUS
			,Logo ProfilePhoto
			,CoverPhoto
			,ClubOpeningTime WorknigHoursFrom
			,ClubClosingTime WorkingHoursTO
		FROM tbl_club_details cd WITH (NOLOCK)
		WHERE cd.AgentId = @AgentId;

		RETURN;
	END;

	--UPDATE USER PROFILE DETAIL
	IF @Flag = 'u'
	BEGIN
		UPDATE tbl_club_details
		SET ClubName1 = ISNULL(@ClubNameEng, ClubName1)
			,ClubName2 = ISNULL(@ClubNameJap, ClubName2)
			,GroupName = ISNULL(@GroupName, GroupName)
			,Description = ISNULL(@Bio, Description)
			,Email = ISNULL(@EmailAddress, Email)
			,MobileNumber = ISNULL(@MobileNumber, MobileNumber)
			,Logo = ISNULL(@ProfilePicture, Logo)
			,CoverPhoto = ISNULL(@CoverPicture, CoverPhoto)
			,ClubOpeningTime = ISNULL(@WorkingHoursFrom, ClubOpeningTime)
			,ClubClosingTime = ISNULL(@WorkingHoursTo, ClubClosingTime)
		WHERE AgentId = @AgentId;

		SELECT '0' code
			,'Club detail updated successfully' message;

		RETURN;
	END;
END;
