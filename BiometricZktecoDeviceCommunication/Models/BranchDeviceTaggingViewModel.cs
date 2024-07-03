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

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Attendance_ZKTeco_Service.Models
{
    public class BranchDeviceTaggingViewModel : BaseViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Branch")]
        //[Range(1, int.MaxValue, ErrorMessage = "Invalid {0}. {0} is required")]
        public int? BranchId { get; set; }

        [Required]
        [Display(Name = "Effect Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public string EffectDate { get; set; }

        [Required]
        [Display(Name = "Attendance Device Type")]
        //[Range(1, int.MaxValue, ErrorMessage = "Invalid {0}. {0} is required")]
        public int? AttendanceDeviceId { get; set; }

        [Display(Name = "Model No.")]
        public string ModelNo { get; set; }

        [Display(Name = "Attendance Device Type")]
        public string DeviceTypeName { get; set; }

        [Display(Name = "IPAddress")]
        public string IPAddress { get; set; }

        [Display(Name = "Device Machine No.")]
        public string DeviceMachineNo { get; set; }

        [Display(Name = "Port")]
        public int Port { get; set; }

        [Display(Name = "Branch")]
        public string Branchname { get; set; }

        [Display(Name = "Branch Code")]
        public string BranchCode { get; set; }

        public int AttendanceDeviceTypeId { get; set; }

        
        [Display(Name = "Valid Till Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public string ValidTillDate { get; set; }
    }

}

