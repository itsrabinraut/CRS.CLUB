using CRS.CLUB.BUSINESS.BookingRequest;
using CRS.CLUB.BUSINESS.ClubManagement;
using CRS.CLUB.BUSINESS.CommonManagement;
using CRS.CLUB.BUSINESS.Home;
using CRS.CLUB.BUSINESS.LogManagement.APILogManagement;
using CRS.CLUB.BUSINESS.LogManagement.EmailLogManagement;
using CRS.CLUB.BUSINESS.LogManagement.ErrorLogManagement;
using CRS.CLUB.BUSINESS.NotificationManagement;
using CRS.CLUB.BUSINESS.PaymentManagement;
using CRS.CLUB.BUSINESS.ProfileManagement;
using CRS.CLUB.BUSINESS.RecommendationManagement;
using CRS.CLUB.BUSINESS.ReservationLedger;
using CRS.CLUB.BUSINESS.ReservationValidationManagement;
using CRS.CLUB.BUSINESS.ReviewManagement;
using CRS.CLUB.BUSINESS.ScheduleManagement;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;

namespace CRS.CLUB.APPLICATION
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<IHomeBusiness, HomeBusiness>();
            container.RegisterType<IAPILogManagementBusiness, APILogManagementBusiness>();
            container.RegisterType<IEmailLogManagementBusiness, EmailLogManagementBusiness>();
            container.RegisterType<IErrorLogManagementBusiness, ErrorLogManagementBusiness>();
            container.RegisterType<IProfileManagementBusiness, ProfileManagementBusiness>();
            container.RegisterType<ICommonManagementBusiness, CommonManagementBusiness>();
            container.RegisterType<INotificationManagementBusiness, NotificationManagementBusiness>();
            container.RegisterType<IBookingRequestBusiness, BookingRequestBusiness>();
            container.RegisterType<IClubManagementBussiness, ClubManagementBusiness>();
            container.RegisterType<IReservationValidationManagementBusiness, ReservationValidationManagementBusiness>();
            container.RegisterType<IReservationLedgerBusiness, ReservationLedgerBusiness>();
            container.RegisterType<IPaymentManagementBusiness, PaymentManagementBusiness>();
            container.RegisterType<IScheduleManagementBusiness, ScheduleManagementBusiness>();
            container.RegisterType<IRecommendationManagementBusiness, RecommendationManagementBusiness>();
            container.RegisterType<IReviewManagementBusiness, ReviewManagementBusiness>();
            return container;
        }
    }
}