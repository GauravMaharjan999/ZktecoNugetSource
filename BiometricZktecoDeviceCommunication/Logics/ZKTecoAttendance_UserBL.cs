using Attendance_ZKTeco_Service.Interfaces;
using Attendance_ZKTeco_Service.Models;
using AttendanceFetch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZkSoftwareEU;

namespace Attendance_ZKTeco_Service.Logics
{
    public class ZKTecoAttendance_UserBL : IZKTecoAttendance_UserBL
    {

        public bool isDuplicateUserFound(UserInfo model)
        {

            var result = GetUserInfoById(int.Parse(model.DwEnrollNumber), model.IPAddress, model.Port);
            if (string.IsNullOrEmpty(result.Data.Name))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public async Task<DataResult> SetUser(UserInfo model)
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
                    //var userlist = GetAllUserInfo(model.IPAddress,model.Port);
                    //var isDuplicate = userlist.Data.Any(x => x.DwEnrollNumber == model.DwEnrollNumber);
                    var isDuplicate = isDuplicateUserFound(model);

                    if (isDuplicate)//check duplicate datat 
                    {
                        return new DataResult { ResultType = ResultType.Failed, Message = $" Failed to Create. Duplicate Emp Code found in device of IP: !! {model.IPAddress}" };

                    }
                    else
                    {
                        var dr = manipulator.SetUserInfo(model);
                        if (dr == true)
                        {

                            return new DataResult { ResultType = ResultType.Success, Message = $" User Created  successfully in device of IP: !! {model.IPAddress}" };
                        }
                        else
                        {
                            return new DataResult { ResultType = ResultType.Failed, Message = $" Failed to Create  in device of IP: !! {model.IPAddress}" };

                        }

                    }


                }
                catch (Exception ex)
                {
                    return new DataResult { ResultType = ResultType.Failed, Message = $"Data pull failed from attendance device!! {ex.Message}" };
                };
            }
            catch (Exception ex)
            {
                return new DataResult { ResultType = ResultType.Failed, Message = $"Device not connected!! {ex.Message}." };
            };
        }


        public async Task<DataResult> SetBulkUsers(List<UserInfo> model)
        {
            try
            {
                DeviceManipulator manipulator = new DeviceManipulator();
                var setStatus = manipulator.SetMultipleUserInfo(model);

                if (setStatus == true)
                {
                    return new DataResult
                    {
                        Message = "Successfully Set Bulk User",
                        ResultType = ResultType.Success,

                    };
                }
                else
                {
                    return new DataResult
                    {
                        Message = "Failed to Set Bulk User",
                        ResultType = ResultType.Failed,

                    };
                }

            }
            catch (Exception ex)
            {
                return new DataResult
                {
                    Message = ex.Message,
                    ResultType = ResultType.Exception,
                };
            }

        }

        public async Task<DataResult> DeleteUser(UserInfo model)
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
                    var dr = manipulator.DeleteUserInfo(model);
                    if (dr == true)
                    {

                        return new DataResult { ResultType = ResultType.Success, Message = $" User Deleted  successfully in device of IP: !! {model.IPAddress}" };
                    }
                    else
                    {
                        return new DataResult { ResultType = ResultType.Failed, Message = $" Failed to Delete User in device of IP: !! {model.IPAddress}" };

                    }

                }
                catch (Exception ex)
                {
                    return new DataResult { ResultType = ResultType.Failed, Message = $"Data pull failed from attendance device!! {ex.Message}" };
                };
            }
            catch (Exception ex)
            {
                return new DataResult { ResultType = ResultType.Failed, Message = $"Device not connected!! {ex.Message}." };
            };
        }

        public DataResult<List<UserInfo>> GetAllUserInfo(string IPaddress, int Port)
        {
            DataResult<List<UserInfo>> dataResult = new DataResult<List<UserInfo>>();
            DeviceManipulator manipulator = new DeviceManipulator();
            var result = manipulator.GetAllUserInfo(IPaddress, Port);
            dataResult.Data = result;
            return dataResult;


        }

        public DataResult<UserInfo> GetUserInfoById(int enrollmentNumber, string IPaddress, int Port)
        {
            DataResult<UserInfo> dataResult = new DataResult<UserInfo>();
            DeviceManipulator manipulator = new DeviceManipulator();
            var result = manipulator.GetUserInfoById(enrollmentNumber, IPaddress, Port);
            dataResult.Data = result;
            return dataResult;


        }

        public async Task<DataResult> SetUserWithoutDuplicateCheck(UserInfo model)
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
                    return new DataResult { ResultType = ResultType.Failed, Message = $"Device not Connected!! IP Address: {model.IPAddress}, Port: {model.Port}" };
                }

                var dr = manipulator.SetUserInfo(model);
                if (dr == true)
                {

                    return new DataResult { ResultType = ResultType.Success, Message = $"User Created successfully !! IP Address: {model.IPAddress}, Port: {model.Port}" };
                }
                else
                {
                    return new DataResult { ResultType = ResultType.Failed, Message = $"Failed to Create !! IP Address: {model.IPAddress}, Port: {model.Port}" };

                }

            }
            catch (Exception ex)
            {
                return new DataResult { ResultType = ResultType.Exception, Message = ex.Message };
            };
        }

        public async Task<BulkUserCreationViewModel> SetBulkUsers(BulkUserWithDeviceInfoViewModel model)
        {
            try
            {
                //var SuccessCount = 0;
                //var FailedCount = 0;
                BulkUserCreationViewModel userCreationViewModel = new BulkUserCreationViewModel();
                List<UserInfo> sslist = new List<UserInfo>();
                List<UserInfo> fflist = new List<UserInfo>();
                //List<UserInfo> finalListToCreate = new List<UserInfo>();
                //foreach (var userInfo in userInfoList)
                //{
                //    var existingList = _zKTecoAttendance_UserBL.GetAllUserInfo(userInfo.IPAddress, userInfo.Port).Data.ToList();

                //    string[] existingEnrollmentIdList = existingList.Select(x => x.DwEnrollNumber).ToArray();
                //    var filterList = userInfoList.Where(x =>
                //           !existingEnrollmentIdList.Contains(x.DwEnrollNumber)).ToList();

                //    finalListToCreate.AddRange(filterList); 
                //}

                var existingList = GetAllUserInfo(model.AttendanceDevice.IPAddress, model.AttendanceDevice.Port).Data.ToList();

                string[] existingEnrollmentIdList = existingList.Select(x => x.DwEnrollNumber).ToArray();
                var filterList = model.UserInfoList.Where(x =>
                       !existingEnrollmentIdList.Contains(x.DwEnrollNumber)).ToList();



                foreach (var userInfo in filterList)
                {
                    if (userInfo.AttendanceDeviceTypeId > 0)
                    {
                        if (userInfo.DeviceTypeName.ToLower() == "zkteco")
                        {
                            var resultData = await SetUserWithoutDuplicateCheck(userInfo);

                            if (resultData.ResultType == ResultType.Success)
                            {
                                sslist.Add(userInfo);
                            }
                            else
                            {
                                fflist.Add(userInfo);
                            }
                        }
                        // add with new device type 
                        //else
                        //{
                        //    result = new DataResult { ResultType = ResultType.Failed, Message = "Attendance Device Type Invalid !!" };
                        //}
                        else
                        {
                            fflist.Add(userInfo);
                        }
                    }
                    else
                    {
                        fflist.Add(userInfo);

                    }

                }

                userCreationViewModel.SuccessList = sslist;
                userCreationViewModel.FailedList = fflist;
                userCreationViewModel.SuccessListCount = userCreationViewModel.SuccessList.Count;
                userCreationViewModel.FailedListCount = userCreationViewModel.FailedList.Count;

                return (userCreationViewModel);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public async Task<DataResult> SetDeviceTime(string IpAddress, int port, DateTime dateTime)
        {
            DeviceManipulator manipulator = new DeviceManipulator();

            return manipulator .SetDeviceTime(IpAddress, port, dateTime);

        }

        public DataResult<string> GetDeviceTime(string IpAddress, int port)
        {
            DeviceManipulator manipulator = new DeviceManipulator();

            //return manipulator.GetDeviceTime(IpAddress, port);
            return manipulator.GetDeviceTime(IpAddress, port);


        }
        public DataResult<string> Restart(AttendanceDevice attendanceDevice)
        {
            DeviceManipulator manipulator = new DeviceManipulator();

            manipulator.Connect_device(attendanceDevice.IPAddress, attendanceDevice.Port);
            //return manipulator.GetDeviceTime(IpAddress, port);
            var result = manipulator.Restart(attendanceDevice.DeviceMachineNo);
            if (result == true)
            {
                return new DataResult<string> { Message = "Sucess", ResultType = ResultType.Success };
            }
            else
            {
                return new DataResult<string> { Message = "Failed", ResultType = ResultType.Failed };

            }


        }
    }

}
