namespace CRS.CLUB.SHARED.ClubManagement
{
    public class ClubManagementCommon
    {
        public string Title { get; set; }
        public string AgentID { get; set; }
        public string Image { get; set; }
        public string Status { get; set; }
        public string Sno { get; set; }
        public string LoginId { get; set; }
        public string ImagePath { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public string Action { get; set; }
    }
    public class AddClubImageCommon : Common
    {
        public string Email { get; set; }
        public string ImageTitle { get; set; }
        public string ImagePath { get; set; }
        public string Sno { get; set; }
        public string Title { get; set; }
    }
}
