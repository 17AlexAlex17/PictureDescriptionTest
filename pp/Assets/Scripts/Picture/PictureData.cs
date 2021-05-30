using System;
using UnityEngine;
using Utils;

namespace Picture
{
    [Serializable]
    public class PictureData
    {
        public string author;
        public string name;
        public string reference;
        public Vector2 size;
        public string description;

        public PictureData(string author, string name, string reference, string size, string description)
        {
            this.author = author;
            this.name = name;
            this.reference = reference;
            this.size = StringToVector.GetVector2(size);
            this.description = description;
        }

        public void SetCacheRefence(string reference)
        {
            this.reference = reference;
        }
    }
}