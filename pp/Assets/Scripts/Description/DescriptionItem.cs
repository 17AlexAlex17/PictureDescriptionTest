using Picture;
using TMPro;
using UnityEngine;

namespace Description
{
    public class DescriptionItem : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _nameItem = default;
        [SerializeField] private TextMeshPro _authorItem = default;
        [SerializeField] private TextMeshPro _descriptionItem = default;

        public void Init(PictureData data)
        {
            _nameItem.text = data.name;
            _authorItem.text = data.author;
            _descriptionItem.text = data.description;
        }
    }
}