using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class DownloadManager : MonoBehaviour
{
    public void DownloadAsset(string apiURl, Action<LoadedAsset> loadedCallback)
    {
        StartCoroutine(RequestAssets(apiURl, (bool successful, LoadedAsset asset) =>
        {
            if (successful)
            {
                if (loadedCallback != null)
                {
                    loadedCallback(asset);
                    RemoveDownloadManager();
                }
            }
            else
            {
                Debug.Log("No asset to download");
                if (loadedCallback != null)
                {
                    loadedCallback(null);
                    RemoveDownloadManager();
                }
            }
        }));
    }

    public IEnumerator RequestAssets(string apiURL, Action<bool, LoadedAsset> loadCompleteCallback)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(apiURL))
        {
            yield return www.SendWebRequest();

            if (www.isHttpError || www.isNetworkError)
            {
                if (loadCompleteCallback != null)
                {
                    loadCompleteCallback(false, null);
                }
            }
            else
            {
                if (loadCompleteCallback != null)
                {
                    LoadedAsset loadedAsset = new LoadedAsset(Path.GetFileName(apiURL), www.downloadHandler.data);
                    loadCompleteCallback(true, loadedAsset);
                }
            }
        }
    }

    private void RemoveDownloadManager() 
    {
        Destroy(this);
    }
}
