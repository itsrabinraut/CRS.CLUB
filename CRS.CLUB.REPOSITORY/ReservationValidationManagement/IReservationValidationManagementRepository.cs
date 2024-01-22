using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.ReservationValidationManagement;
using System.Collections.Generic;

namespace CRS.CLUB.REPOSITORY.ReservationValidationManagement
{
    public interface IReservationValidationManagementRepository
    {
        ReservationDetailViaOTPCommon GetReservationDetailViaOTP(string OTPCode, string ActionUser, string ReservationId = "");

        List<ReservationHostDetailCommon> GetReservationHostDetail(string ReservationId);

        CommonDbResponse ManageReservationOTPStatus(ManageReservationOTPStatusCommon Request);
    }
}
