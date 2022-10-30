using DivoomControl.Enums;
using DivoomControl.Models.Requests;
using DivoomControl.Models.Responses;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace DivoomControl.Models
{
    public class Device
    {
        private DeviceInformation Information { get; set; }
        private HttpClient client = new HttpClient();

        public Device(DeviceInformation information)
        {
            this.Information = information;
        }

        public async Task setImageFromCloudAsync(string fileId)
        {
            var url = $"http://{Information.DevicePrivateIp}:80/post";
            await client.PostAsync(url, new StringContent("{\"Command\":\"Draw/SendRemote\",\"FileId\":\"" + fileId + "\"}", Encoding.UTF8, "application/json"));
        }

        public async Task<List<CloudImage>> getLikedImagesAsync(int page = 1)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync<GetCloudImageListRequest>("https://app.divoom-gz.com/Device/GetImgLikeList", new GetCloudImageListRequest(Information, page));
            if (response.IsSuccessStatusCode)
            {
                var likedImageResponse = await response.Content.ReadFromJsonAsync<GetCloudImageListResponse>();
                if (likedImageResponse == null)
                {
                    return new List<CloudImage>();
                }
                else if (likedImageResponse.ImgList == null)
                {
                    return new List<CloudImage>();
                }
                return likedImageResponse.ImgList;
            }
            return new List<CloudImage>();
        }

        public async Task<List<CloudImage>> getUploadedImagesAsync(int page = 1)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync<GetCloudImageListRequest>("https://app.divoom-gz.com/Device/GetImgUploadList", new GetCloudImageListRequest(Information, page));
            if (response.IsSuccessStatusCode)
            {
                var likedImageResponse = await response.Content.ReadFromJsonAsync<GetCloudImageListResponse>();
                if (likedImageResponse == null)
                {
                    return new List<CloudImage>();
                }
                else if (likedImageResponse.ImgList == null)
                {
                    return new List<CloudImage>();
                }
                return likedImageResponse.ImgList;
            }
            return new List<CloudImage>();
        }

        public async Task setDisplayChannel(DisplayChannel channel)
        {
            var url = $"http://{Information.DevicePrivateIp}:80/post";
            await client.PostAsync(url, new StringContent("{\"Command\":\"Channel/SetIndex\",\"SelectIndex\": " + (int)channel + "}", Encoding.UTF8, "application/json"));
        }

        public async Task setDisplayStatus(DisplayStatus status)
        {
            var url = $"http://{Information.DevicePrivateIp}:80/post";
            await client.PostAsync(url, new StringContent("{\"Command\":\"Channel/OnOffScreen\",\"OnOff\": " + (status == DisplayStatus.On ? 1 : 0) + "}", Encoding.UTF8, "application/json"));
        }

        public async Task<DisplayStatus> getDisplayStatus()
        {
            var settings = await getDeviceSettings();
            if (settings == null)
            {
                return DisplayStatus.Unknown;
            }
            return settings.LightSwitch == 1 ? DisplayStatus.On : DisplayStatus.Off;
        }

        public async Task setBrightness(int brightness)
        {
            if (brightness < 0)
            {
                brightness = 0;
            } else if (brightness > 100)
            {
                brightness = 100;
            }
            var url = $"http://{Information.DevicePrivateIp}:80/post";
            await client.PostAsync(url, new StringContent("{\"Command\":\"Channel/SetBrightness\",\"Brightness\": " + brightness + "}", Encoding.UTF8, "application/json"));
        }

        public async Task<int> getBrightness()
        {
            var settings = await getDeviceSettings();
            if (settings == null)
            {
                return -1;
            }
            return settings.Brightness;
        }

        public async Task<SettingsResponse?> getDeviceSettings()
        {
            var url = $"http://{Information.DevicePrivateIp}:80/post";
            HttpResponseMessage response = await client.PostAsync(url, new StringContent("{\"Command\":\"Channel/GetAllConf\"}", Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var settingsResponse = await response.Content.ReadFromJsonAsync<SettingsResponse>();
                return settingsResponse;
            }
            return null;
        }

        public async Task enableNoiseTool(bool enable)
        {
            var url = $"http://{Information.DevicePrivateIp}:80/post";
            await client.PostAsync(url, new StringContent("{\"Command\":\"Tools/SetNoiseStatus\",\"NoiseStatus\": " + (enable ? 1 : 0) + "}", Encoding.UTF8, "application/json"));
        }

        public async Task setScoreBoard(int blueScore, int redScore)
        {
            var url = $"http://{Information.DevicePrivateIp}:80/post";
            await client.PostAsync(url, new StringContent("{\"Command\":\"Tools/SetScoreBoard\",\"BlueScore\": " + blueScore + ",\"RedScore\": " + redScore + "}", Encoding.UTF8, "application/json"));
        }

        public async Task setStopWatch(StopWatchCommand command)
        {
            var url = $"http://{Information.DevicePrivateIp}:80/post";
            await client.PostAsync(url, new StringContent("{\"Command\":\"Tools/SetStopWatch\",\"Status\": " + (int)command + "}", Encoding.UTF8, "application/json"));
        }

        public async Task setTimer(bool active, int minutes = 0, int seconds = 0)
        {
            var url = $"http://{Information.DevicePrivateIp}:80/post";
            await client.PostAsync(url, new StringContent("{\"Command\":\"Tools/SetTimer\",\"Minute\": " + minutes + ",\"Second\": " + seconds + ", \"Statzs\": " + (active ? 1 : 0) + "}", Encoding.UTF8, "application/json"));
        }

        public async Task playGifFromUrl(string gifUrl)
        {
            var url = $"http://{Information.DevicePrivateIp}:80/post";
            await client.PostAsync(url, new StringContent("{\"Command\":\"Device/PlayTFGif\",\"FileType\": 2, \"FileName\": \"" + gifUrl + "\"}", Encoding.UTF8, "application/json"));
        }

        public string getLocalIp()
        {
            return Information.DevicePrivateIp;
        }

        public string getMac()
        {
            return Information.DeviceMac;
        }

        public int getId()
        {
            return Information.DeviceId;
        }

        public string getName()
        {
            return Information.DeviceName;
        }

        public override string ToString()
        {
            return $"{Information.DeviceName} ({Information.DevicePrivateIp})";
        }
    }
}
