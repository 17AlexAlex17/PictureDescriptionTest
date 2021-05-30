using Picture;
using UnityEngine;

namespace Description
{
    public class DescriptionItemGenerator : MonoBehaviour
    {
        [SerializeField] private DescriptionItem _descriptionItem = default;
        
        public void CreateItem(PictureData data, Transform parent)
        {
            var di = Instantiate(_descriptionItem, parent);
            
            di.transform.localScale = new Vector3(data.size.x, data.size.y, 0.01f);
            di.transform.localPosition = new Vector3(data.size.x, 0, 0);
            di.Init(data);
        }
    }
}