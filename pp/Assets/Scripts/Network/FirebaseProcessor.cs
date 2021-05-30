using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using Firebase.Extensions;
using Firebase.Firestore;
using Firebase.Storage;

using Picture;

namespace Network
{
    public static class FirebaseProcessor
    {
        #region Static
        private static PictureData CreateDataStruct(Dictionary<string, object> dataPair)
        {
            var data = new PictureData(
                dataPair["author"].ToString(),
                dataPair["name"].ToString(),
                dataPair["reference"].ToString(),
                dataPair["size"].ToString(),
                dataPair["description"].ToString()
            );
            return data;
        }
        #endregion
        
      
        private static FirebaseFirestore _store;
        private static FirebaseStorage _storage;

        private static  List<PictureData> _picturesData = new List<PictureData>();
        private static Dictionary<byte[],string> _picturesBytes = new Dictionary<byte[], string>();

        public static async Task Init()
        {
            InitiailizeFireStore();
            InitializeStorage();
            await RetrieveData();
            await GetPictureBytes();
            PictureSaverInCache.SavePicturesDataToCache(_picturesData, _picturesBytes);
      
        }

        private static async Task GetPictureBytes()
        {
            for (int i = 0; i < _picturesData.Count; i++)
            {
                await DownloadPictureBytes(_picturesData[i]);
            }
        }
        private static void InitiailizeFireStore()
        {
            _store = FirebaseFirestore.DefaultInstance;
        }
        
        private static void InitializeStorage()
        {
            _storage = FirebaseStorage.GetInstance("gs://picturedescription-3f129.appspot.com/");
        }

        private static async Task RetrieveData()
        {
            var collectionReference = _store.Collection("pictures");
            var task = await collectionReference.GetSnapshotAsync().ContinueWithOnMainThread(t => t);
            {
                var result = task?.Result;
                if (task?.Result != null)
                {
                    await LoadAllDocs(result.Documents);
                }
            }
        }
        
        private static async Task LoadAllDocs(IEnumerable<DocumentSnapshot> docs)
        {
            foreach (var snapshot in docs)
            {
                var doc = snapshot.Reference;
                await doc.GetSnapshotAsync()
                    .ContinueWithOnMainThread(task =>
                {
                    _picturesData.Add(CreateDataStruct(task.Result.ToDictionary()));
                });
            }
        }

        private static async Task DownloadPictureBytes(PictureData data)
        {
            var avatarRef = _storage.GetReferenceFromUrl(data.reference);
            var downloadTask = avatarRef.GetBytesAsync(long.MaxValue);
            await downloadTask.ContinueWithOnMainThread(task =>
            {
                if (!task.IsFaulted && !task.IsCanceled)
                {
                    _picturesBytes.Add(downloadTask.Result, data.name);
                }
            });
        }
    }
}

