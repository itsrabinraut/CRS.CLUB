using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.ReviewManagement;
using System.Collections.Generic;

namespace CRS.CLUB.BUSINESS.ReviewManagement
{
    public interface IReviewManagementBusiness
    {
        List<ReviewManagementCommon> GetReviews(string clubId, SearchFilterCommonModel dbRequest, string reviewId = "");
        CommonDbResponse DeleteReview(string reviewId, string actionUser, string actionIp);
    }
}