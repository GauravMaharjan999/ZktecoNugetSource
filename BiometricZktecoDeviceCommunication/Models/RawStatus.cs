using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Attendance_ZKTeco_Service.Models
{
    public enum RawStatus
    {
        [Display(Name ="Save as Draft")]
        SaveAsDraft = -1,
        New = 0,
        Approved = 1,
        Modified = 2,
        Deleted = 3,
        [Display(Name = "Approved Modified")]
        ApprovedModified = 4,
        [Display(Name = "Non Approval")]
        NonApproval = 5,
        [Display(Name = "Rejected")]
        Rejected = 6,
    }
}
