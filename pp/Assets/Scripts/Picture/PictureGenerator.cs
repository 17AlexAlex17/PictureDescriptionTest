using UnityEngine;
using Vuforia;

namespace Picture
{
    public static class PictureGenerator
    {
        public static Transform GetImageTargetFromSideloadedTexture(PictureData data)
        {
            var objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
            var runtimeImageSource = objectTracker.RuntimeImageSource;

            runtimeImageSource.SetFile(VuforiaUnity.StorageType.STORAGE_ABSOLUTE, data.reference, data.size.x,
                "myTargetName");
            var dataset = objectTracker.CreateDataSet();
        
            var trackableBehaviour = dataset.CreateTrackable(runtimeImageSource, "myTargetName");
            trackableBehaviour.gameObject.AddComponent<DefaultTrackableEventHandler>();
            trackableBehaviour.gameObject.AddComponent<TurnOffBehaviour>();
        
            var imgBehaviour = trackableBehaviour.GetComponent<ImageTargetBehaviour>();
            imgBehaviour.ImageTarget.SetSize(new Vector2(data.size.x, data.size.y));
            objectTracker.ActivateDataSet(dataset);
        
            return trackableBehaviour.transform;
        }
    }
}


