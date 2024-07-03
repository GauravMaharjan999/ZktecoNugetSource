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
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using ZkSoftwareEU;

namespace Attendance_ZKTeco_Service.Models
{
    public class DeviceManipulator
    {
        private readonly CZKEUEMNetClass machine;
        public DeviceManipulator()
        {
            machine = new CZKEUEMNetClass();
        }
        public bool Connect_device(string IPaddress, int Port)
        {
            try
            {
                //CZKEUEMNetClass machine = new CZKEUEMNetClass();
                string ipAddress = IPaddress;
                int portNumber = Port; //4370;
                                       //string port = Port.ToString();

                //int portNumber = Port; //4370;
                //if (!int.TryParse(port, out portNumber))
                //    throw new Exception("Not a valid port number");

                bool isValidIpA = UniversalStatic.ValidateIP(ipAddress);
                if (!isValidIpA)
                    throw new Exception("The Device IP is invalid !!");

                isValidIpA = UniversalStatic.PingTheDevice(ipAddress);
                if (!isValidIpA)
                    throw new Exception("The device at " + ipAddress + ":" + portNumber + " did not respond!! Ping failed.");

                //machine = new ZkemClient(RaiseDeviceEvent);
                bool connectNet = machine.Connect_Net(ipAddress, portNumber);
                if (!connectNet)
                    return false;
                //throw new Exception("Device not connected!!.");

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
            // return false;
        }

        public DataResult CheckDeviceConnectionStatus(string IPaddress, int Port)
        {
            try
            {
                string ipAddress = IPaddress;
                int portNumber = Port; //4370;
                                       //string port = Port.ToString();

                

                bool isValidIpA = UniversalStatic.ValidateIP(ipAddress);
                if (!isValidIpA)
                    return new DataResult { ResultType = ResultType.Failed, Message = "The Device IP is invalid !!" };


                isValidIpA = UniversalStatic.PingTheDevice(ipAddress);
                if (!isValidIpA)
                    return new DataResult { ResultType = ResultType.Failed, Message = "The device at " + ipAddress + ":" + portNumber + " did not respond!! Ping failed." };

                bool connectNet = machine.Connect_Net(ipAddress, portNumber);
                if (!connectNet)
                    return new DataResult { ResultType = ResultType.Failed, Message = "The Device not Connected" };

                return new DataResult { ResultType = ResultType.Success, Message = "The Device Connected Successfully" };

            }
            catch (Exception ex)
            {

                return new DataResult { ResultType = ResultType.Exception, Message = ex.Message.ToString() };

            }
        }

        public DataResult PingDevice(string IPaddress, int Port)
        {
            try
            {
                string ipAddress = IPaddress;
                int portNumber = Port; //4370;
                                       //string port = Port.ToString();



                bool isValidIpA = UniversalStatic.ValidateIP(ipAddress);
                if (!isValidIpA)
                    return new DataResult { ResultType = ResultType.Failed, Message = "The Device IP is invalid !!" };


                isValidIpA = UniversalStatic.PingTheDevice(ipAddress);
                if (!isValidIpA)
                    return new DataResult { ResultType = ResultType.Failed, Message = "The device at " + ipAddress + ":" + portNumber + " did not respond!! Ping failed." };

               

                return new DataResult { ResultType = ResultType.Success, Message = "The Ping Operation Successfully" };

            }
            catch (Exception ex)
            {

                return new DataResult { ResultType = ResultType.Exception, Message = ex.Message.ToString() };

            }
        }
        //public bool Check_DeviceTime(string IPaddress, int Port)
        //{
        //    int dwYear = int.Parse(System.DateTime.Now.Year.ToString());
        //    int dwMonth = System.DateTime.Now.Month;
        //    int dwDay = System.DateTime.Now.Day;
        //    int dwHour = System.DateTime.Now.Hour;
        //    int dwMinute = System.DateTime.Now.Minute;
        //    int dwSecond = System.DateTime.Now.Second;


        //    Connect_device(IPaddress, Port);
        //    try
        //    {
        //        return machine.GetDeviceTime(1, ref dwYear, ref dwMonth, ref dwDay, ref dwHour, ref dwMinute, ref dwSecond);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public DataResult<string> GetDeviceTime(string IPaddress, int Port)
        {
            int dwYear = 0, dwMonth = 0, dwDay = 0, dwHour = 0, dwMinute = 0, dwSecond = 0;

            Connect_device(IPaddress, Port);

            try
            {
                if (machine.GetDeviceTime(1, ref dwYear, ref dwMonth, ref dwDay, ref dwHour, ref dwMinute, ref dwSecond))
                {
                    var date = new DateTime(dwYear, dwMonth, dwDay, dwHour, dwMinute, dwSecond);
                    return new DataResult<string> { Data= date.ToString(), Message = "Succesfully Data Retrieved", ResultType = ResultType.Success};
                }
                else
                {
                    return new DataResult<string> { Data = null, Message = "Failed to retrieved Date and Time", ResultType = ResultType.Failed };

                }
                // If GetDeviceTime fails, you can handle it here
                // Maybe throw an exception or return DateTime.MinValue or null as appropriate
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MachineInfo> GetLogData(int machineNumber, string IPaddress,int port)
        {
            try
            {
                List<MachineInfo> lstEnrollData = new List<MachineInfo>();

                //CZKEUEMNetClass machine1 = new CZKEUEMNetClass();
                bool isConected = machine.Connect_Net(IPaddress, port);
                if (isConected)
                {

                    int iMachineNumber = 1; //the device number
                    string idwEnrollNumber = "";
                    int idwVerifyMode = 0;
                    int idwInOutMode = 0;
                    int idwYear = 0;
                    int idwMonth = 0;
                    int idwDay = 0;
                    int idwHour = 0;
                    int idwMinute = 0;
                    int idwSecond = 0;
                    int idwWorkCode = 0;

                    string userId = ""; 
                    string userName = "";
                    string userCardNo = "";
                    string userPassword = "";
                    int abcdef = 1;
                    bool userEnabled = false;

                    machine.ReadAllGLogData(iMachineNumber); //read all the attendance records to the memory       \
                    
                    //bool fr = machine.ReadAllUserID(iMachineNumber);
                    //bool ab = machine.ReadAllTemplate(iMachineNumber);
                    while (machine.SSR_GetGeneralLogData(iMachineNumber, ref idwEnrollNumber, ref idwVerifyMode, ref idwInOutMode,
                        ref idwYear, ref idwMonth, ref idwDay, ref idwHour, ref idwMinute, ref idwSecond, ref idwWorkCode)) //get attendance data one by one from memory
                    {
                        
                        string inputDate = new DateTime(idwYear, idwMonth, idwDay, idwHour, idwMinute, idwSecond).ToString("yyyy-MM-dd HH:mm:ss");
                        MachineInfo objInfo = new MachineInfo();
                        objInfo.MachineNumber = iMachineNumber;
                        objInfo.IndRegID = int.Parse(idwEnrollNumber);
                        objInfo.Mode = idwVerifyMode.ToString();
                        objInfo.DateTimeRecord = inputDate;
                        objInfo.DeviceIP = IPaddress;
                        objInfo.PunchInOutMode = idwInOutMode;
                        machine.SSR_GetUserInfo(1, Convert.ToString(objInfo.IndRegID), ref userName, ref userPassword, ref abcdef, ref userEnabled);
                        objInfo.Username = userName;

                        lstEnrollData.Add(objInfo);
                        //disable
                    }

                    var dadsadad = lstEnrollData;
                    
                    return lstEnrollData;


                }
                else
                {
                    return lstEnrollData;
                }



  
            }
            catch (Exception ex)
            {
                List<MachineInfo> lst = new List<MachineInfo>();
                return lst;
            }
        }
        public bool ClearGLog(int machineNumber, string IPaddress)
        {
            bool IsClear = machine.ClearGLog(machineNumber);
            bool enable = machine.EnableDevice(machineNumber, true);
            machine.Disconnect();
            return IsClear;
        }

        public bool ClearLogData(int machineNumber, string IPaddress)
        {
            bool IsClear = machine.ClearLogData(machineNumber);
            bool enable = machine.EnableDevice(machineNumber, true);
            //machine.Disconnect();
            return IsClear;
        }
        //public bool ClearTimeLogData(int machineNumber, string IPaddress)
        //{
        //    string deviceIP = "YourDeviceIP"; // Replace with the actual device IP address
        //    int devicePort = 4370; // Replace with the actual device port
        //    DateTime startTime = new DateTime(2023, 1, 1, 0, 0, 0);
        //    DateTime endTime = new DateTime(2023, 12, 31, 23, 59, 59);

        //    bool isConnected = machine.Connect_Net(deviceIP, devicePort);

        //    if (isConnected)
        //    {
        //        // Fetch all attendance logs
        //        while (machine.ReadGeneralLogData(1))
        //        {
        //            // Check if the log timestamp falls within your desired time range
        //            DateTime logTime = DateTime.ParseExact(machine.AttendanceTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

        //            if (logTime >= startTime && logTime <= endTime)
        //            {
        //                // Delete the log entry
        //                machine.SSR_DelLogByTime_Spa();
        //            }
        //        }

        //        // Disconnect from the device
        //        machine.Disconnect();
        //    }
        //    else
        //    {
        //        Console.WriteLine("Connection to the device failed.");
        //    }
        //}

       

        public void Disconnect()
        {
            machine.Disconnect();
        }

        public bool Restart(int machineNumber)
        {
            return machine.RestartDevice(machineNumber);
        }
        #region UserPart
        public bool SetUserInfo(UserInfo userInfo)
        {
            try
            {
                if (userInfo == null)
                {
                    return false;
                }
                else
                {
                    if (Connect_device(userInfo.IPAddress, userInfo.Port))
                    {
                        int DwEnrollNumberInString = int.Parse(userInfo.DwEnrollNumber);
                        var userinfolist = GetUserInfoById(DwEnrollNumberInString, userInfo.IPAddress, userInfo.Port);
                        if (String.IsNullOrEmpty(userinfolist.Name))
                        {
                           
                            var result = machine.SSR_SetUserInfo(userInfo.DwMachineNumber, userInfo.DwEnrollNumber, userInfo.Name, userInfo.Password, 0, true); // User 

                            return result;
                        }
                    }
                    return false;

                }
            }
            catch (Exception ex)
            {

                return false;
            }

        }

        public List<UserInfo> GetAllUserInfo(string IPaddress, int Port)
        {
            List<UserInfo> userInfoList = new List<UserInfo>();
            try
            {

                bool isConected = machine.Connect_Net(IPaddress, Port);
          
                if (isConected)
                {
                    int machineNumber = 1; // Default value is 1
                    int dwEnrollNumber = 0;
                    string dwEnrollNumberString = "";
                    string Name = "";
                    string Password = "";
                    int dwVerifyMode = 0;
                    int dwInOutMode = 0;
                    int dwYear = 0;
                    int dwMonth = 0;
                    int dwDay = 0;
                    int dwHour = 0;
                    int dwMinute = 0;
                    int dwSecond = 0;
                    int dwWorkCode = 0;
                    int Previlege = 0;
                    bool Enabled = true;
                    int iPrivilege = 0, iTmpLength = 0, iFlag = 0, idwFingerIndex;
                    string sdwEnrollNumber = string.Empty, sName = string.Empty,
                             sPassword = string.Empty, sTmpData = string.Empty;

                    //machine.EnableDevice(machineNumber, false); // Disable device while processing data

                    bool abc = machine.ReadAllUserID(machineNumber); // Read all user data from the device

                    while (machine.SSR_GetAllUserInfo(machineNumber, ref dwEnrollNumberString,ref Name,ref Password,ref Previlege,ref Enabled))
                    {
                       

                        var userInfo = new UserInfo();
                        userInfo.Name = Name;
                        userInfo.DwEnrollNumber = dwEnrollNumberString;
                        userInfo.DwMachineNumber = machineNumber;
                        userInfo.IPAddress = IPaddress;
                        userInfo.Password = Password;
                        userInfo.Port = Port;

                        //if (machine.GetUserTmpExStr(machineNumber, dwEnrollNumberString, idwFingerIndex, out iFlag, out sTmpData, out iTmpLength))
                        //{

                        //}



                            userInfoList.Add(userInfo);
                    }

                    //machine.EnableDevice(machineNumber, true); // Enable device again

                    //machine.Disconnect(); // Disconnect from the device
                    return userInfoList;
                }
                else
                {
                    Console.WriteLine("Connection failed");
                    return userInfoList;
                }

            }
            catch (Exception ex)
            {

                return userInfoList;
            }

        }

        public UserInfo GetUserInfoById(int EnrollmentNumber, string IPaddress, int Port)
        {
            try
            {
                bool isConected = machine.Connect_Net(IPaddress, Port);
                UserInfo userInfo = new UserInfo();
                if (isConected)
                {
                    int machineNumber = 1; // Default value is 1
                    string dwEnrollNumberString = EnrollmentNumber.ToString();
                    string Name = "";
                    string Password = "";
                    int Previlege = 0;
                    bool Enabled = true;
                    bool getInfo = machine.SSR_GetUserInfo(machineNumber, dwEnrollNumberString, ref Name, ref Password, ref Previlege, ref Enabled);
                  
                        userInfo.Name = Name;
                        userInfo.Password = Password;
                        userInfo.DwMachineNumber = machineNumber;
                        userInfo.DwEnrollNumber = dwEnrollNumberString;
                  
                 
                    return userInfo;
                }
               return userInfo;

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public bool DeleteUserInfo(UserInfo userInfo)
        {
            try
            {
                if (userInfo == null)
                {
                    return false;
                }
                else
                {
                    //var result = machine.SSR_SetUserInfo(userInfo.DwMachineNumber, userInfo.DwEnrollNumber, userInfo.Name, userInfo.Password, 0, true); // User Set
                    int enrollmentNumberInInt = int.Parse( userInfo.DwEnrollNumber);
                    var result = machine.SSR_DeleteEnrollData(userInfo.DwMachineNumber,userInfo.DwEnrollNumber,12);
               
                    return result; 
                }
            }
            catch (Exception ex)
            {

                return false;
            }

        }



        public bool SetMultipleUserInfo(List<UserInfo> userInfoList)
        {
            try
            {
                if (userInfoList == null)
                {
                    return false;
                }
                else
                {

                    try
                    {
                        foreach (var userInfo in userInfoList)
                        {
                            var duplicateUserInfo = GetUserInfoById(int.Parse(userInfo.DwEnrollNumber), userInfo.IPAddress, userInfo.Port);
                            if (String.IsNullOrEmpty(duplicateUserInfo.Name))
                            {

                                var result = machine. SSR_SetUserInfo(userInfo.DwMachineNumber, userInfo.DwEnrollNumber, userInfo.Name, userInfo.Password, 0, true); // User 
                            }
                            else
                            {
                                return false;

                            }
                        }


                        return true;
                    }
                    catch (Exception)
                    {

                        return false;
                    }
                  
                }
            }
            catch (Exception ex)
            {

                return false;
            }

        }
        #endregion

        public DataResult SetDeviceTime(string IpAddress , int port,DateTime dateTime)
        {
            try
            {
                // Get current system time
                DateTime currentTime = dateTime== DateTime.MinValue ? DateTime.Now : dateTime;

                machine.Connect_Net(IpAddress, port);
                // Set the device time
                bool setTimeSuccess = machine.SetDeviceTime2(1, currentTime.Year, currentTime.Month, currentTime.Day,
                                                         currentTime.Hour, currentTime.Minute, currentTime.Second);

                if (setTimeSuccess)
                {
                    return new DataResult { ResultType = ResultType.Success, Message = "Set Time Successfully" };

                }
                else
                {
                    return new DataResult { ResultType = ResultType.Success, Message = "Failed to set device time." };

                } 
            }
            catch (Exception ex)
            {

                throw;
            }
            

        }
    }
}

