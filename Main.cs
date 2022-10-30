using DivoomControl.Models;
using DivoomControl.Models.Responses;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DivoomControl
{
    public static class Main
    {
        private static HttpClient client = new HttpClient();

        public static async Task<List<Device>> getAvailableDevices()
        {
            HttpResponseMessage response = await client.PostAsync("https://app.divoom-gz.com/Device/ReturnSameLANDevice", null);
            if (response.IsSuccessStatusCode)
            {
                var deviceResponse = await response.Content.ReadFromJsonAsync<FindDeviceResponse>();
                if (deviceResponse == null)
                {
                    return new List<Device>();
                }
                else if (deviceResponse.DeviceList == null)
                {
                    return new List<Device>();
                }
                var result = new List<Device>();

                deviceResponse.DeviceList.ForEach(information =>
                {
                    result.Add(new Device(information));
                });

                return result;
            }
            return new List<Device>();
        }
    }
}
