using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance_ZKTeco_Service.Models
{
    public class MachineData
    {
        public int MachineNumber { get; set; }
        public int IndRegID { get; set; }
        public string DateTimeRecord { get; set; }
        public string DeviceIP { get; set; }
        public string Mode { get; set; }

        public virtual List<MachineData> ListData { get; set; }
    }
}
