using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DivoomControl.Models.Requests
{
    public class GetLikedImageListRequest
    {
        [JsonPropertyName("DeviceId")]
        public int DeviceId { get; set; }
        [JsonPropertyName("DeviceMac")]
        public string DeviceMac { get; set; }
        [JsonPropertyName("Page")]
        public int Page { get; set; }

        public GetLikedImageListRequest(DeviceInformation device, int Page = 1)
        {
            this.DeviceId = device.DeviceId;
            this.DeviceMac = device.DeviceMac;
            this.Page = Page;
        }
    }
}
