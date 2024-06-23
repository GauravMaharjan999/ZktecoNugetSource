using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceFetch.Models
{
    public  class BaseDeviceModel
    {
        public int AttendanceDeviceTypeId { get; set; }
        public string DeviceTypeName { get; set; }
        public string IPAddress { get; set; }
        public int Port { get; set; }
    }
}
