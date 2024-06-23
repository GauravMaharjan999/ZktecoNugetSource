using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AttendanceFetch.Models
{
    public class BulkUserWithDeviceInfoViewModel
    {
        public AttendanceDeviceVMForBiometricDevice AttendanceDevice { get; set; }
        public List<UserInfo> UserInfoList { get; set; }



    }
    public class AttendanceDeviceVMForBiometricDevice
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int AttendanceDeviceTypeId { get; set; }

        [Required]
        [StringLength(10)]
        public string ModelNo { get; set; }

        [Required]
        public string DeviceMachineNo { get; set; }

        [Required]
        [StringLength(50)]
        public string IPAddress { get; set; }

        [Required]
        public int Port { get; set; }

    }
}
