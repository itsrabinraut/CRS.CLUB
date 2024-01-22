using CRS.CLUB.SHARED.ReservationValidationManagement;
using CRS.CLUB.SHARED;
using System.Collections.Generic;

namespace CRS.CLUB.BUSINESS.ReservationValidationManagement
{
    public interface IReservationValidationManagementBusiness
    {
        ReservationDetailViaOTPCommon GetReservationDetailViaOTP(string OTPCode, string ActionUser, string ReservationId = "");

        List<ReservationHostDetailCommon> GetReservationHostDetail(string ReservationId);

        CommonDbResponse ManageReservationOTPStatus(ManageReservationOTPStatusCommon Request);
    }
}
