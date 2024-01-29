using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.ReviewManagement;
using DocumentFormat.OpenXml.Office2016.Excel;
using System;
using System.Collections.Generic;
using System.Data;

namespace CRS.CLUB.REPOSITORY.ReviewManagement
{
    public class ReviewManagementRepository : IReviewManagementRepository
    {
        private readonly RepositoryDao _dao;
        public ReviewManagementRepository(RepositoryDao dao) => _dao = dao;

        public CommonDbResponse DeleteReview(string reviewId, string actionUser, string actionIp)
        {
            var sql = "sproc_review_and_ratings_admin_setup @Flag='dre'";
            sql += ",@ReviewId=" + _dao.FilterString(reviewId);
            sql += ", @ActionUser=" + _dao.FilterString(actionUser);
            sql += ", @ActionIP=" + _dao.FilterString(actionIp);
            return _dao.ParseCommonDbResponse(sql);
        }

        public List<ReviewManagementCommon> GetReviews(string clubId, SearchFilterCommonModel dbRequest, string reviewId = "")
        {
            var reviewsAndRatings = new List<ReviewManagementCommon>();
            var sql = "Exec sproc_review_and_ratings_admin_setup @Flag='srnr'";
            if (!string.IsNullOrWhiteSpace(clubId))
                sql += " ,@ClubId=" + _dao.FilterString(clubId);
            if (!string.IsNullOrWhiteSpace(reviewId))
                sql += " ,@ReviewId=" + _dao.FilterString(reviewId);
            if (!string.IsNullOrWhiteSpace(dbRequest.SearchFilter))
                sql += " ,@SearchText=N" + _dao.FilterString(dbRequest.SearchFilter);
            sql += ",@Skip=" + dbRequest.Skip;
            sql += ",@Take=" + dbRequest.Take;
            var dt = _dao.ExecuteDataTable(sql);
            if (null != dt)
            {
                foreach (DataRow item in dt.Rows)
                {
                    var review = new ReviewManagementCommon()
                    {
                        ReviewId = item["ReviewId"].ToString(),
                        UserImage = item["UserImage"].ToString(),
                        NickName = item["NickName"].ToString(),
                        FullName = item["FullName"].ToString(),
                        ClubNameEng = item["ClubNameEng"].ToString(),
                        ClubNameJap = item["ClubNameJap"].ToString(),
                        EnglishRemark = item["EnglishRemark"].ToString(),
                        JapaneseRemark = item["JapaneseRemark"].ToString(),
                        RemarkType = item["RemarkType"].ToString(),
                        Rating = item["Rating"].ToString(),
                        ReviewedOn = item["ReviewedOn"].ToString(),
                        TotalRecords = Convert.ToInt32(item["TotalRecords"].ToString()),
                        SNO = Convert.ToInt32(item["SNO"].ToString())
                    };
                    reviewsAndRatings.Add(review);
                }
            }
            return reviewsAndRatings;
        }
    }
}