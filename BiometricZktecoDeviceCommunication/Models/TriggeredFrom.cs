using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AttendanceFetch.Models
{
    public enum TriggeredFrom
    {
        [Display(Name = "ScheduleAsyncAutoPushDataToMainServer")]
        ScheduleAsyncAutoPushDataToMainServer = 1,
        [Display(Name = "ScheduleAsyncAutoPushDataToMainServerAndDeleteAttLogMorning")]
        ScheduleAsyncAutoPushDataToMainServerAndDeleteAttLogMorning = 2,
        [Display(Name = "ScheduleAsyncAutoPushDataToMainServerAndDeleteAttLogEvening")]
        ScheduleAsyncAutoPushDataToMainServerAndDeleteAttLogEvening = 3

    }
}
