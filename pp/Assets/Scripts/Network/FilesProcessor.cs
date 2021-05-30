using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using UnityEngine;

namespace Network
{
    public static class FilesProcessor
    {
        public static void SaveFile(object data, string name)
        {
            var directory = string.Empty;
            SetPath(ref directory);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            
            var destination = directory + name;
            var file = File.Exists(destination) ? File.OpenWrite(destination) : File.Create(destination);
            
            var bf = new BinaryFormatter();
            bf.Serialize(file, data);
            file.Close();
        }

        public static string LoadFile(string destination) 
        {
            var file = File.OpenRead(destination);
            var bf = new BinaryFormatter();
            
            if (File.Exists(destination))
            {
                var data = (string) bf.Deserialize(file);
                file.Close();
                return data;
            }
     
            return string.Empty;
        }
       
        public static void SaveImage(byte[] bytes, string name)
        {
            var dirPath = GetPath() + "/Vuforia/";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            
            dirPath = dirPath + name + ".png";
            File.WriteAllBytes(dirPath, bytes);
        }
        
        public static bool CheckCache()
        {
            var path = GetPath() + "/PicturesData.desc";
            return File.Exists(path);
        }

        public static string GetImageReferenceInCache(string name)
        {
            return GetPath() + "/Vuforia/" + name + ".png";
        }
        
        private static void SetPath(ref string currentString)
        {
    #if UNITY_EDITOR || UNITY_STANDALONE
            currentString = Application.streamingAssetsPath;
    #endif
    #if UNITY_ANDROID || UNITY_IOS
            currentString = Application.persistentDataPath;
    #endif
        }
        
       public static string GetPath()
        {
            var currentString = string.Empty;
    #if UNITY_EDITOR || UNITY_STANDALONE
            currentString = Application.streamingAssetsPath;
    #endif
    #if UNITY_ANDROID || UNITY_IOS
            currentString = Application.persistentDataPath;
    #endif
            return currentString;
        }
    }
}
