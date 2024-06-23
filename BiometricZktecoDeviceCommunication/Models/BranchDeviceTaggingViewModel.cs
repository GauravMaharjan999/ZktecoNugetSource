using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Attendance_ZKTeco_Service.Models
{
    public class BranchDeviceTaggingViewModel : BaseViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Branch")]
        //[Range(1, int.MaxValue, ErrorMessage = "Invalid {0}. {0} is required")]
        public int? BranchId { get; set; }

        [Required]
        [Display(Name = "Effect Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public string EffectDate { get; set; }

        [Required]
        [Display(Name = "Attendance Device Type")]
        //[Range(1, int.MaxValue, ErrorMessage = "Invalid {0}. {0} is required")]
        public int? AttendanceDeviceId { get; set; }

        [Display(Name = "Model No.")]
        public string ModelNo { get; set; }

        [Display(Name = "Attendance Device Type")]
        public string DeviceTypeName { get; set; }

        [Display(Name = "IPAddress")]
        public string IPAddress { get; set; }

        [Display(Name = "Device Machine No.")]
        public string DeviceMachineNo { get; set; }

        [Display(Name = "Port")]
        public int Port { get; set; }

        [Display(Name = "Branch")]
        public string Branchname { get; set; }

        [Display(Name = "Branch Code")]
        public string BranchCode { get; set; }

        public int AttendanceDeviceTypeId { get; set; }

        
        [Display(Name = "Valid Till Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public string ValidTillDate { get; set; }
    }

}
