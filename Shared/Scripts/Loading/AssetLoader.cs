using System.Collections;
using System.IO;
using TimiShared.Debug;
using TimiShared.Service;
using TimiShared.Utils;
using UnityEngine;
using UnityEngine.Networking;

namespace TimiShared.Loading {
    // TODO: AssetLoader is sort of a weird name for what this class is doing
    // TODO: Change this to not be a MonoBehaviour; right now the only reason this is a MonoBehaviour is for this.StartCoroutine
    // TODO: Add templated loaders
    public class AssetLoader : MonoBehaviour, IService {

        public static AssetLoader Instance {
            get {
                return ServiceLocator.Service<AssetLoader>();
            }
        }

        #region Public API
        public void GetStreamFromStreamingAssets(string filePathInStreamingAssets, System.Action<Stream> onLoadedCallback) {
            string fullPath = Path.Combine(Application.streamingAssetsPath, filePathInStreamingAssets);
            if (string.IsNullOrEmpty(fullPath)) {
                TimiDebug.LogErrorColor("No such file", LogColor.red);
                return;
            }

            if (!fullPath.Contains("://")) {
                // Regular File i/o should work here
                Stream stream = FileUtils.OpenFileStream(fullPath, FileMode.Open, FileAccess.Read);
                onLoadedCallback.Invoke(stream);

            } else {
                // Paths with "file://" are to be treated like urls and handled with UnityWebRequest
                this.StartCoroutine(this.DownloadStream(fullPath, onLoadedCallback));
            }
        }

        public void CloseStream(Stream stream) {
            if (stream != null) {
                stream.Close();
            }
        }
        #endregion

        private IEnumerator DownloadStream(string fullPath, System.Action<Stream> onLoadedCallback) {
            UnityWebRequest www = UnityWebRequest.Get(fullPath);
            if (www == null) {
                TimiDebug.LogErrorColor("www object is null for file path: " + fullPath, LogColor.red);
                yield break;
            }
            yield return www.SendWebRequest();

            if(www.isNetworkError || www.isHttpError) {
                TimiDebug.LogErrorColor("Error loading " + fullPath + ": " + www.error, LogColor.red);
                yield break;
            }
            MemoryStream stream = new MemoryStream(www.downloadHandler.data);
            if (stream == null) {
                TimiDebug.LogErrorColor("MemoryStream is null for file path: " + fullPath, LogColor.red);
                yield break;
            }

            onLoadedCallback.Invoke(stream);
        }
    }
}
