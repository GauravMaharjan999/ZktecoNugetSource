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

using Attendance_ZKTeco_Service.Interfaces;
using Attendance_ZKTeco_Service.Models;
using AttendanceFetch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ZkSoftwareEU;

namespace Attendance_ZKTeco_Service.Logics
{
    public class ZKTecoAttendance_DataFetchBL : IZKTecoAttendance_DataFetchBL
    {
        public ZKTecoAttendance_DataFetchBL()
        {

        }
        public async Task<DataResult<List<MachineInfo>>> GetData_Zkteco(AttendanceDevice model)
        {
            DeviceManipulator manipulator = new DeviceManipulator();
            try
            {
                //Validation
                if (model.IPAddress == string.Empty || model.Port <= 0)
                {
                    return new DataResult<List<MachineInfo>> { ResultType = ResultType.Failed, Message = "The Device IP Address or Port is mandotory !!" };
                }

                bool Isconnected = manipulator.Connect_device(model.IPAddress, model.Port);
                if (!Isconnected)
                {
                    return new DataResult<List<MachineInfo>> { ResultType = ResultType.Failed, Message = "Device not Connected!!" };
                }
                try
                {
                    List<MachineInfo> lstEnrollData = new List<MachineInfo>();

                    CZKEUEMNetClass machine1 = new CZKEUEMNetClass();

                    List<MachineInfo> data = manipulator.GetLogData(model.DeviceMachineNo, model.IPAddress, model.Port);
                    if (data.Count > 0)
                    {
                        #region Push  Attendance Log Data to HR Server
                        //
                        //DataResult result = PushData(data, model.Id, model.StatusChgUserId, model.ClientAlias);
                        //if (result.ResultType == ResultType.Success)
                        //{
                        //    //manipulator.ClearGLog(model.DeviceMachineNo, model.IPAddress);
                        //    return new DataResult { ResultType = ResultType.Success, Message = "Data Pull and Push Success!!", dataRow = data };
                        //}
                        //else
                        //{
                        //    manipulator.Enable_Device(model.DeviceMachineNo);
                        //    manipulator.Disconnect();
                        //    return new DataResult { ResultType = ResultType.Failed, Message = "Data Push Failed on Application!!" };
                        //}
                        #endregion

                        return new DataResult<List<MachineInfo>> { ResultType = ResultType.Success, Message = "Data Retrived from Attendance Device Successfully!", Data = data.ToList() };
                    }
                    else
                    {
                        //manipulator.ClearGLog(model.DeviceMachineNo, model.IPAddress);
                        return new DataResult<List<MachineInfo>> { ResultType = ResultType.Failed, Message = "No any data found to pull from Attendance Device!!" };
                    }
                }
                catch (Exception ex)
                {
                    return new DataResult<List<MachineInfo>> { ResultType = ResultType.Failed, Message = $"Data pull failed from attendance device!! {ex.Message}" };
                };
            }
            catch (Exception ex)
            {
                return new DataResult<List<MachineInfo>> { ResultType = ResultType.Failed, Message = $"Device not connected!! {ex.Message}." };
            };
        }


        public async Task<DataResult> DeleteData_Zkteco(AttendanceDevice model)
        {
            DeviceManipulator manipulator = new DeviceManipulator();
            try
            {
                //Validation
                if (model.IPAddress == string.Empty || model.Port <= 0)
                {
                    return new DataResult { ResultType = ResultType.Failed, Message = "The Device IP Address or Port is mandotory !!" };
                }

                bool Isconnected = manipulator.Connect_device(model.IPAddress, model.Port);
                if (!Isconnected)
                {
                    return new DataResult { ResultType = ResultType.Failed, Message = "Device not Connected!!" };
                }
                try
                {

                    CZKEUEMNetClass machine1 = new CZKEUEMNetClass();

                    bool result = manipulator.ClearLogData(model.DeviceMachineNo, model.IPAddress);
                    if (result == true)
                    {
                        return new DataResult { ResultType = ResultType.Success, Message = "Data Deleted from Attendance Device Successfully!" };
                    }
                    else
                    {
                        //manipulator.ClearGLog(model.DeviceMachineNo, model.IPAddress);
                        return new DataResult { ResultType = ResultType.Failed, Message = "No any data found to delete from Attendance Device!!" };
                    }
                }
                catch (Exception ex)
                {
                    return new DataResult { ResultType = ResultType.Exception, Message = $"Data delete failed from attendance device!! {ex.Message}" };
                };
            }
            catch (Exception ex)
            {
                return new DataResult { ResultType = ResultType.Failed, Message = $"Device not connected!! {ex.Message}." };
            };
        }


        public async Task<DataResult> CheckDeviceConnectionStatus(string IPAddress, int Port = 4370)
        {
            DeviceManipulator manipulator = new DeviceManipulator();
            try
            {
                //Validation
                if (IPAddress == string.Empty || Port <= 0)
                {
                    return new DataResult { ResultType = ResultType.Failed, Message = "The Device IP Address or Port is mandotory !!" };
                }
                return manipulator.CheckDeviceConnectionStatus(IPAddress, Port);


            }
            catch (Exception ex)
            {
                return new DataResult { ResultType = ResultType.Failed, Message = $"Device not connected!! {ex.Message}." };
            };
        }
        public async Task<DataResult> PingDevice(string IPAddress, int Port = 4370)
        {
            DeviceManipulator manipulator = new DeviceManipulator();
            try
            {
                //Validation
                if (IPAddress == string.Empty || Port <= 0)
                {
                    return new DataResult { ResultType = ResultType.Failed, Message = "The Device IP Address or Port is mandotory !!" };
                }
                return manipulator.PingDevice(IPAddress, Port);


            }
            catch (Exception ex)
            {
                return new DataResult { ResultType = ResultType.Failed, Message = $"Device ping operation failed connected!! {ex.Message}." };
            };
        }




    }
}

