using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.ReviewManagement;
using System.Collections.Generic;
using System.Net;

namespace CRS.CLUB.REPOSITORY.ReviewManagement
{
    public interface IReviewManagementRepository
    {
        List<ReviewManagementCommon> GetReviews(string clubId, SearchFilterCommonModel dbRequest, string reviewId = "");
        CommonDbResponse DeleteReview(string reviewId, string actionUser, string actionIp);
    }
}