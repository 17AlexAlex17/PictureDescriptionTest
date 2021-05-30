using System.Threading.Tasks;
using Description;
using Network;
using Picture;
using UnityEngine;

namespace DefaultNamespace
{
    public class AppInitializer : MonoBehaviour
    {
        private async void Awake()
        {
            if (!FilesProcessor.CheckCache())
            {
                await FirebaseProcessor.Init();
            }
            new PictureLoaderFromCache(this,GetComponent<DescriptionItemGenerator>());
        }

       
    }
}