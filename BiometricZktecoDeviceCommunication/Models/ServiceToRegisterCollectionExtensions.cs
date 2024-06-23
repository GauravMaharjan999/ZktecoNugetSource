using Attendance_ZKTeco_Service.Interfaces;
using Attendance_ZKTeco_Service.Logics;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AttendanceFetch.Models
{
    public class ServiceToRegisterCollectionExtensions
    {
        public IServiceCollection AddMyServices(IServiceCollection services)
        {
            services.AddTransient<IZKTecoAttendance_DataFetchBL, ZKTecoAttendance_DataFetchBL>();
            // Add other services here
            return services;
        }
    }
}
