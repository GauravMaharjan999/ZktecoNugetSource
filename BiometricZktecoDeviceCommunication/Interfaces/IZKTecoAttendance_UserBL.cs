using AttendanceFetch.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Attendance_ZKTeco_Service.Interfaces
{
    public interface IZKTecoAttendance_UserBL
    {
        Task<DataResult> SetUser(UserInfo model);
        Task<DataResult> DeleteUser(UserInfo model);
        DataResult<List<UserInfo>> GetAllUserInfo(string IPaddress, int Port);
        DataResult<UserInfo> GetUserInfoById(int enrollmentNumber, string IPaddress, int Port);
        Task<DataResult> SetBulkUsers(List<UserInfo> model);
        Task<DataResult> SetUserWithoutDuplicateCheck(UserInfo model);
        Task<BulkUserCreationViewModel> SetBulkUsers(BulkUserWithDeviceInfoViewModel model);
        Task<DataResult> SetDeviceTime(string IpAddress, int port,DateTime dateTime);
        DataResult<string> GetDeviceTime(string IpAddress, int port);
        DataResult<string> Restart(AttendanceDevice attendanceDevice);
    }
}
