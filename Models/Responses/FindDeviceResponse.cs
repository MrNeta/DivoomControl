using System.Collections.Generic;

namespace DivoomControl.Models.Responses
{
    public class FindDeviceResponse
    {
        public int ReturnCode { get; set; }
        public string ReturnMessage { get; set; }
        public List<DeviceInformation> DeviceList { get; set; }
    }
}
