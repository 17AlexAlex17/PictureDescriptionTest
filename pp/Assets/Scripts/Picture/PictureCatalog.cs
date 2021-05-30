using System;
using System.Collections.Generic;

namespace Picture
{
    [Serializable]
    public struct PictureCatalog
    {
        public List<PictureData> PictureDatas;

        public PictureCatalog(List<PictureData> pictureDatas)
        {
            PictureDatas = pictureDatas;
        }
    }
}