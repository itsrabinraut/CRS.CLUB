using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRS.CLUB.APPLICATION.Models
{
    public class CommonModel
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string Action { get; set; }
        public string IpAddress { get; set; }
        public string BrowserInfo { get; set; }
        public string ActionUser { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedPlatform { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}