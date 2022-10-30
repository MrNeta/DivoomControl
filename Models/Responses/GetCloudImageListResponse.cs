using System.Collections.Generic;

namespace DivoomControl.Models.Responses
{
    public  class GetCloudImageListResponse
    {
        public int ReturnCode { get; set; }
        public string ReturnMessage { get; set; }
        public List<CloudImage> ImgList { get; set; }
        public int DeviceId { get; set; }
    }
}
