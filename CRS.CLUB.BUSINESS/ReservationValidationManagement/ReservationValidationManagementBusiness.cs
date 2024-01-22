using CRS.CLUB.REPOSITORY.ReservationValidationManagement;
using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.ReservationValidationManagement;
using System.Collections.Generic;

namespace CRS.CLUB.BUSINESS.ReservationValidationManagement
{
    public class ReservationValidationManagementBusiness : IReservationValidationManagementBusiness
    {
        private readonly IReservationValidationManagementRepository _repo;
        public ReservationValidationManagementBusiness(ReservationValidationManagementRepository repo) => _repo = repo;

        public ReservationDetailViaOTPCommon GetReservationDetailViaOTP(string OTPCode, string ActionUser, string ReservationId = "")
        {
            return _repo.GetReservationDetailViaOTP(OTPCode, ActionUser, ReservationId);
        }

        public List<ReservationHostDetailCommon> GetReservationHostDetail(string ReservationId)
        {
            return _repo.GetReservationHostDetail(ReservationId);
        }

        public CommonDbResponse ManageReservationOTPStatus(ManageReservationOTPStatusCommon Request)
        {
            return _repo.ManageReservationOTPStatus(Request);
        }
    }
}
