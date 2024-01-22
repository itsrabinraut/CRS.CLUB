using CRS.CLUB.APPLICATION.Resources;
using System.ComponentModel.DataAnnotations;

namespace CRS.CLUB.APPLICATION.Models.Home
{
    public class SetNewPasswordModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "PasswordRequired")]
        [MinLength(8, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "Minimum_length_is_8_characters")]
        [MaxLength(16, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "Maximum_length_is_16_characters")]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "PasswordRequired")]
        [MinLength(8, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "Minimum_length_is_8_characters")]
        [MaxLength(16, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "Maximum_length_is_16_characters")]
        public string ConfirmPassword { get; set; }
    }
}