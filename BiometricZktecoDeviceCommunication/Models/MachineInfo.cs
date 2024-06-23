using AttendanceFetch.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance_ZKTeco_Service.Models
{
    public class MachineInfo
    {
        public int MachineNumber { get; set; }
        public int IndRegID { get; set; }
        public string DateTimeRecord { get; set; }
        public string DeviceIP { get; set; }
        public string Mode { get; set; }
        public DateTime DateOnlyRecord
        {
            get { return DateTime.Parse(DateTime.Parse(DateTimeRecord).ToString("yyyy-MM-dd")); }
        }
        public DateTime TimeOnlyRecord
        {
            get { return DateTime.Parse(DateTime.Parse(DateTimeRecord).ToString("hh:mm:ss tt")); }
        }
        public string Username { get; set; }
        public int? AttendanceDeviceId { get; set; }
        public int PunchInOutMode { get; set; }
    }


    public class MachineInfoViewModel
    {
        public List<MachineInfo> machineInfoList { get; set; }
        public int AttendanceDeviceId { get; set; }
        public string ClientAlias { get; set; }
        public int FetchType { get; set; }
    }
}
