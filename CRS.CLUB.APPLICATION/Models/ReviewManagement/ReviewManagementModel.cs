using CRS.CLUB.SHARED.PaginationManagement;
using System.Collections.Generic;

namespace CRS.CLUB.APPLICATION.Models.ReviewManagement
{
    public class ReviewManagementModel
    {
        public List<ReviewModel> ReviewsAndRatings { get; set; } = new List<ReviewModel>();
    }
    public class ReviewModel
    {
        public string ReviewId { get; set; }
        public string UserImage { get; set; }
        public string NickName { get; set; }
        public string FullName { get; set; }
        public string ClubNameEng { get; set; }
        public string ClubNameJap { get; set; }
        public string EnglishRemark { get; set; }
        public string JapaneseRemark { get; set; }
        public string RemarkType { get; set; }
        public string Rating { get; set; }
        public string ReviewedOn { get; set; }
        public int SNO { get; set; }
        public int TotalRecords { get; set; }
    }
    public class SearchFilterModel
    {
        public string SearchFilter { get; set; }
    }
}