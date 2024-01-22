using CRS.CLUB.APPLICATION.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRS.CLUB.APPLICATION.Models.Home
{
    public class LoginRequestModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "Required")]
        [Display(Name = "Username")]
        public string LoginId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "Required")]
        [Display(Name = "Password")]
        [MaxLength(16, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "Maximum_length_is_16_characters")]
        public string Password { get; set; }
        public string SessionId { get; set; }
    }
    public class LoginResponseModel
    {
        public string Username { get; set; }
        public string AgentId { get; set; }
        public string UserId { get; set; }
        public string EmailAddress { get; set; }
        public string Logo { get; set; }
        public string ClubName { get; set; }
        public string ClubNameJp { get; set; }
        public string SessionId { get; set; }
        public bool ChangePassword { get; set; }
        public List<MenuModel> Menus { get; set; }
        public List<string> Functions { get; set; }
        public List<Notification> Notifications { get; set; }
    }
    public class MenuModel
    {
        public string MenuName { get; set; }
        public string MenuUrl { get; set; }
        public string MenuGroup { get; set; }
        public string ParentGroup { get; set; }
        public string CssClass { get; set; }
        public string GroupOrderPosition { get; set; }
        public string MenuOrderPosition { get; set; }
    }
    public class Notification
    {
        public string NotificationId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string ImageUrl { get; set; }
        public string TotalCount { get; set; }
        public string Time { get; set; }
    }
}