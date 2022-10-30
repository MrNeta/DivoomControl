using System.Collections.Generic;

namespace DivoomControl.Models.Responses
{
    public  class GetLikedImageListResponse
    {
        public int ReturnCode { get; set; }
        public string ReturnMessage { get; set; }
        public List<LikedImage> ImgList { get; set; }
        public int DeviceId { get; set; }
    }
}
