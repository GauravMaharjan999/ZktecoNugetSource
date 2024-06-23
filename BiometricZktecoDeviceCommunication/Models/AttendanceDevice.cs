using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AttendanceFetch.Models
{
    public class AttendanceDevice
    {
        public int Id { get; set; }
        [Required]
        public int AttendanceDeviceTypeId { get; set; }
        [Required]
        public string DeviceTypeName { get; set; }
        [Required]
        public string ModelNo { get; set; }
        [Required]
        public int DeviceMachineNo { get; set; }
        [Required]
        public string IPAddress { get; set; }
        [Required]
        public int Port { get; set; }
        public int StatusChgUserId { get; set; }

        public string ClientAlias { get; set; }
    }
}
