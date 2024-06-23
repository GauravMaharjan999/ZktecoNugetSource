using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceFetch.Models
{

    public class DataResult<T> where T : class
    {
        public ResultType ResultType { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
    public class DataResult
    {
        public ResultType ResultType { get; set; }
        public string Message { get; set; }
        public int ReturnId { get; set; }

    }
}
