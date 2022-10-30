﻿using DivoomControl.Models.Requests;
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

        public async Task<List<LikedImage>> getLikedImagesAsync(int page = 1)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync<GetLikedImageListRequest>("https://app.divoom-gz.com/Device/GetImgLikeList", new GetLikedImageListRequest(Information, page));
            if (response.IsSuccessStatusCode)
            {
                var likedImageResponse = await response.Content.ReadFromJsonAsync<GetLikedImageListResponse>();
                if (likedImageResponse == null)
                {
                    return new List<LikedImage>();
                }
                else if (likedImageResponse.ImgList == null)
                {
                    return new List<LikedImage>();
                }
                return likedImageResponse.ImgList;
            }
            return new List<LikedImage>();
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