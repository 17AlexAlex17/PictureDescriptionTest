using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Network;
using Picture;
using UnityEngine;

namespace Picture
{
    public static class PictureSaverInCache
    {
        public static void SavePicturesDataToCache(List<PictureData> _picturesData,
            Dictionary<byte[], string> _pictureBytes)
        {
            if (_picturesData.Count > 0)
            {
                SaveImages(_pictureBytes);
                RefreshReference(_picturesData);
                var pc = new PictureCatalog(_picturesData);
                var json = JsonUtility.ToJson(pc);
                FilesProcessor.SaveFile(json, "/PicturesData.desc");

            }
        }

        private static void RefreshReference(List<PictureData> _picturesData)
        {
            for (int i = 0; i < _picturesData.Count; i++)
            {
                _picturesData[i].SetCacheRefence(FilesProcessor.GetImageReferenceInCache(_picturesData[i].name));
            }
        }

        private static void SaveImages(Dictionary<byte[], string> _pictureBytes)
        {
            foreach (var _pictureBytesItem in _pictureBytes)
            {
                FilesProcessor.SaveImage(_pictureBytesItem.Key, _pictureBytesItem.Value);
            }
        }
    }
}