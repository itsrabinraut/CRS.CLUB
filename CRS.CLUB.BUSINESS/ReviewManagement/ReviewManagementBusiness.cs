using CRS.CLUB.REPOSITORY.ReviewManagement;
using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.ReviewManagement;
using System.Collections.Generic;

namespace CRS.CLUB.BUSINESS.ReviewManagement
{
    public class ReviewManagementBusiness : IReviewManagementBusiness
    {
        private readonly IReviewManagementRepository _repo;
        public ReviewManagementBusiness(ReviewManagementRepository repo) => _repo = repo;

        public CommonDbResponse DeleteReview(string reviewId, string actionUser, string actionIp)
        {
            return _repo.DeleteReview(reviewId, actionUser, actionIp);
        }

        public List<ReviewManagementCommon> GetReviews(string clubId, SearchFilterCommonModel dbRequest, string reviewId = "")
        {
            return _repo.GetReviews(clubId, dbRequest, reviewId);
        }
    }
}