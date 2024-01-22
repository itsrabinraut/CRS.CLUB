using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.ReviewManagement;
using System.Collections.Generic;

namespace CRS.CLUB.REPOSITORY.ReviewManagement
{
    public interface IReviewManagementRepository
    {
        List<ReviewManagementCommon> GetReviews(string clubId, string reviewId = "", string searchText = "");
        CommonDbResponse DeleteReview(string reviewId, string actionUser, string actionIp);
    }
}