USE [CRS]

/****** Object:  StoredProcedure [dbo].[sproc_admin_payment_management]    Script Date: 11/23/2023 6:58:03 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROC [dbo].[sproc_club_payment_management] @Flag VARCHAR(20),
@Date DATETIME = '2023-11-18',
@ClubId VARCHAR(10) = NULL,
@SearchField VARCHAR(200) = NULL
AS
DECLARE @SQL VARCHAR(MAX) = '',
		@SQLParameter VARCHAR(MAX) = '',
		@SQLParameter2 VARCHAR(MAX) = '';
BEGIN
	IF ISNULL(@Flag, '') = 'gpld' --get payment logs detail
	BEGIN
		IF @Date IS NULL
		BEGIN
			SELECT 1 Code,
				   'Invalid date' Message;
			RETURN;
		END
		ELSE
		BEGIN
			SET @SQLParameter += ' AND FORMAT(a.ActionDate,''yyyy-MM-dd'') = ''' +  FORMAT(@Date,'yyyy-MM-dd') + ''''; 
		END

		IF NOT EXISTS
		(
			SELECT 'X'
			FROM tbl_club_details a WITH (NOLOCK)
			WHERE a.AgentId = @ClubId
		)
		BEGIN
			SELECT 1 Code,
				   'Invalid club details' Message;
			RETURN;
		END
		ELSE
		BEGIN
			SET @SQLParameter += ' AND a.ClubId=' +  @ClubId
		END

		IF @SearchField IS NOT NULL
		BEGIN
			SET @SQLParameter += ' AND c.NickName LIKE ''%' + @SearchField + '%''';
		END

		SET @SQL = 'select 0 Code, ''Success'' Message, CONCAT(c.FirstName, '' '', c.LastName) AS CustomerName,
				   c.NickName AS CustomerNickName,
				   c.ProfileImage AS CustomerImage,
				   b.PlanName,
				   a.NoOfPeople,
				   a.VisitDate,
				   a.VisitTime,
				   d.StaticDataLabel AS PaymentType,
				   0 AS Commission,
				   0 AS AdminPayment
			FROM tbl_reservation_detail a WITH (NOLOCK)
			INNER JOIN tbl_reservation_plan_detail b WITH (NOLOCK) ON b.ReservationId = a.ReservationId
			INNER JOIN tbl_customer c WITH (NOLOCK) ON c.AgentId = a.CustomerId 
			INNER JOIN tbl_static_data d WITH (NOLOCK) on d.StaticDataType = 10 AND d.StaticDataValue = a.PaymentType
			WHERE 1 = 1 ' + @SQLParameter;

		PRINT(@SQL);
		EXEC(@SQL);

		RETURN;
	END

	ELSE IF ISNULL(@Flag, '') = 'gpmo' --get payment management overview
	BEGIN
		SELECT  '202' AS TotalBookings,
				'103' AS ApprovedBookings,
				'99' AS PendingBookings,
				'12,426' AS PendingPayments
		RETURN;
	END
END
GO


