
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Attendance_ZKTeco_Service.Models
{
    public class BaseViewModel:RootViewModel
    {
        [Display(Name = "Status")]
        public RawStatus Status { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public string StatusChgDate { get; set; }

        [Display(Name = "User ID", Order = 103)]
        public int StatusChgUserId { get; set; }

        [Display(Name = "User Name")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string StatusChgUserName { get; set; }
        public BaseStagingInfoViewModel BaseStagingInfo { get; set; }
        [Display(Name = "Delete Cause")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
        public string DeleteCause { get; set; }

        [Display(Name = "Rejected Cause")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
        public string RejectCause { get; set; }
    }
    public class BaseStagingInfoViewModel
    {
        public RawStatus StagingStatus { get; set; }
        public string Cause { get; set; }
    }
}
