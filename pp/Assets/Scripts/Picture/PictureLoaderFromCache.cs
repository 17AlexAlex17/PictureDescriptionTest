using System.Collections;
using System.Collections.Generic;
using Description;
using Network;
using Picture;
using UnityEngine;
using Vuforia;

namespace Picture
{
    public class PictureLoaderFromCache
    {
        private List<PictureData> _picturesData;
        private DescriptionItemGenerator _descriptionItemGenerator;

        public PictureLoaderFromCache(MonoBehaviour coroutineProcessor,
            DescriptionItemGenerator _descriptionItemGenerator)
        {
            this._descriptionItemGenerator = _descriptionItemGenerator;
            LoadPicturesDataFromCache();
            coroutineProcessor.StartCoroutine(GenerateImageFromCache());
        }

        private void LoadPicturesDataFromCache()
        {
            var path = FilesProcessor.GetPath() + "/PicturesData.desc";
            _picturesData = new List<PictureData>();
            var catalog = JsonUtility.FromJson<PictureCatalog>(FilesProcessor.LoadFile(path));
            _picturesData.AddRange(catalog.PictureDatas);
        }

        private IEnumerator GenerateImageFromCache()
        {
            while (VuforiaRuntime.Instance.InitializationState != VuforiaRuntime.InitState.INITIALIZED)
            {
                yield return null;
            }

            for (int i = 0; i < _picturesData.Count; i++)
            {
                var pictTransform = PictureGenerator.GetImageTargetFromSideloadedTexture(_picturesData[i]);
                CreateDescription(_picturesData[i], pictTransform);
            }
        }

        private void CreateDescription(PictureData data, Transform parent)
        {
            _descriptionItemGenerator.CreateItem(data, parent);
        }
    }
}