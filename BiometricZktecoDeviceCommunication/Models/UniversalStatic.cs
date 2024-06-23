using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace AttendanceFetch.Models
{
    public class UniversalStatic
    {
        public const string acx_Disconnect = "Disconnected";
        public const string acx_Connect = "Conncected";

        public static bool ValidateIP(string addrString)
        {
            IPAddress address;
            if (IPAddress.TryParse(addrString, out address))
                return true;
            else
                return false;
        }


        public static bool PingTheDevice(string ipAdd)
        {
            try
            {
                IPAddress ipAddress = IPAddress.Parse(ipAdd);

                Ping pingSender = new Ping();
                PingOptions options = new PingOptions();
                options.DontFragment = true;

                // Create a buffer of 32 bytes of data to be transmitted. 
                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 120;
                PingReply reply = pingSender.Send(ipAddress, timeout, buffer, options);

                if (reply.Status == IPStatus.Success)
                    return true;
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string SplitPascal(string text)
        {
            Regex r = new Regex("([A-Z]+[a-z]+)");
            string result = r.Replace(text, m => (m.Value.Length > 3 ? m.Value : m.Value.ToLower()) + " ");
            return result;
        }


        public static string IntegerValidation(char[] enteredString, string actualString)
        {
            foreach (char c in enteredString.AsEnumerable())
            {

                if (Char.IsDigit(c))
                { actualString = actualString + c; }
                else
                {
                    actualString.Replace(c, ' ');
                    actualString.Trim();
                }
            }
            return actualString;
        }


    }
}
