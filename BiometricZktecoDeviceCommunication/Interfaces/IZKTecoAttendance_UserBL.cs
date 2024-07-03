/*
 * MIT License
 *
 * Copyright (c) 2024 Gaurav Maharjan
 * Email: gauravmaharjan000@gmail.com
 * Unique GUID: 7e408279-5263-447f-b7ab-631c93d64de0
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

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

