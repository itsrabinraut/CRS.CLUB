using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.ReservationValidationManagement;
using System.Collections.Generic;
using System.Linq;

namespace CRS.CLUB.REPOSITORY.ReservationValidationManagement
{
    public class ReservationValidationManagementRepository : IReservationValidationManagementRepository
    {
        private readonly RepositoryDao _dao;
        public ReservationValidationManagementRepository()
        {
            _dao = new RepositoryDao();
        }

        public ReservationDetailViaOTPCommon GetReservationDetailViaOTP(string OTPCode, string ActionUser, string ReservationId = "")
        {
            string SQL = "EXEC dbo.[sproc_reservation_validation_management] @Flag = 'grdvo'";
            SQL += ",@OTPCode=" + _dao.FilterString(OTPCode);
            SQL += ",@ActionUser=" + _dao.FilterString(ActionUser);
            SQL += ",@ReservationId=" + _dao.FilterString(ReservationId);
            var dbResponse = _dao.ExecuteDataRow(SQL);
            if (dbResponse != null)
            {
                var Code = _dao.ParseColumnValue(dbResponse, "Code").ToString();
                var Message = _dao.ParseColumnValue(dbResponse, "Message").ToString();
                if (!string.IsNullOrEmpty(Code) && Code == "0")
                {
                    return new ReservationDetailViaOTPCommon()
                    {
                        Code = ResponseCode.Success,
                        Message = Message ?? "Success",
                        ReservationId = _dao.ParseColumnValue(dbResponse, "ReservationId").ToString(),
                        CustomerName = _dao.ParseColumnValue(dbResponse, "CustomerName").ToString(),
                        InvoiceId = _dao.ParseColumnValue(dbResponse, "InvoiceId").ToString(),
                        LocationVerificationStatus = _dao.ParseColumnValue(dbResponse, "LocationVerificationStatus").ToString(),
                        OTPVerificationStatus = _dao.ParseColumnValue(dbResponse, "OTPVerificationStatus").ToString(),
                        PaymentType = _dao.ParseColumnValue(dbResponse, "PaymentType").ToString(),
                        ReservedDate = _dao.ParseColumnValue(dbResponse, "ReservedDate").ToString(),
                        VisitDateTime = _dao.ParseColumnValue(dbResponse, "VisitDateTime").ToString(),
                        TransactionStatus = _dao.ParseColumnValue(dbResponse, "TransactionStatus").ToString(),
                        LocationName = _dao.ParseColumnValue(dbResponse, "LocationName").ToString(),
                        StoreName = _dao.ParseColumnValue(dbResponse, "StoreName").ToString(),
                        PlanName = _dao.ParseColumnValue(dbResponse, "PlanName").ToString()
                    };
                }
                return new ReservationDetailViaOTPCommon()
                {
                    Code = ResponseCode.Failed,
                    Message = Message ?? "Failed"
                };
            }
            return new ReservationDetailViaOTPCommon()
            {
                Code = ResponseCode.Warning,
                Message = "Something went wrong. Try again later."
            };
        }

        public List<ReservationHostDetailCommon> GetReservationHostDetail(string ReservationId)
        {
            string SQL = "EXEC dbo.[sproc_reservation_validation_management] @Flag = 'ghvr'";
            SQL += ",@ReservationId=" + _dao.FilterString(ReservationId);
            var dbResponse = _dao.ExecuteDataTable(SQL);
            if (dbResponse != null && dbResponse.Rows.Count > 0) return _dao.DataTableToListObject<ReservationHostDetailCommon>(dbResponse).ToList();
            return new List<ReservationHostDetailCommon>();
        }

        public CommonDbResponse ManageReservationOTPStatus(ManageReservationOTPStatusCommon Request)
        {
            string SQL = "EXEC dbo.[sproc_reservation_validation_management] @Flag = 'mros'";
            SQL += ",@ReservationId=" + _dao.FilterString(Request.ReservationId);
            SQL += ",@OTPCode=" + _dao.FilterString(Request.OTPCode);
            SQL += ",@ActionUser=" + _dao.FilterString(Request.ActionUser);
            SQL += ",@ActionPlatform=" + _dao.FilterString(Request.ActionPlatform);
            SQL += ",@ActionIP=" + _dao.FilterString(Request.ActionIP);
            return _dao.ParseCommonDbResponse(SQL);
        }
    }
}
