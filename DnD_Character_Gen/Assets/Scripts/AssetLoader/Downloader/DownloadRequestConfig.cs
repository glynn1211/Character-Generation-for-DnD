using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SP.AssetRequestor
{
    /*
     At the minuet this just downloads assets that are zip files
     need this to do soemthing other than zips
         */
    public class DownloadRequestConfig : MonoBehaviour
    {
        bool isInitalised = false;
        const string zipExtention = ".zip";
        public ZipManager zipManager { get; private set; }
        Action downloadingCompleate;

        bool loadCompleateFromServer = false;

        int downloadCount;
        int currentAsset = 0;
        string assetFolder;
        string writeLocation;

        public void Initalised()
        {
            if (!isInitalised)
            {
                isInitalised = !isInitalised;
                zipManager = new ZipManager(Application.persistentDataPath, UpdateCompleate, assetFolder);
            }
        }

        public void DownloadRequest(string[] _assetUrls, Action _downloadCompleate, string _assetFolder, string _writeLocation = null)
        {
            //make sure all objects are initalised
            if (_assetUrls.Length == 0 && _downloadCompleate != null) 
            {
                _downloadCompleate();
            }

            writeLocation = _writeLocation;
            assetFolder = _assetFolder;
            Initalised();
            downloadingCompleate = _downloadCompleate;
            downloadCount = _assetUrls.Length;

            if (writeLocation != null) 
            {
                zipManager.SetDownloadLocation(writeLocation);
            }


            for (int i = 0; i < _assetUrls.Length; i++)
            {
                string fileName = FileName(_assetUrls[i]);
                string extention = GetFileExtention(_assetUrls[i]);

                if (!string.IsNullOrEmpty(extention))
                {
                    //tell the dowload manager to download the zip from the url
                    //it will then pass the downloaded asset byte[] to the zim managaer

                    DownloadManager dm = gameObject.AddComponent<DownloadManager>();
                    if (extention == zipExtention)
                    {
                        dm.DownloadAsset(_assetUrls[i], zipManager.HandleDownloadedZip);
                    }
                    else
                    {
                        dm.DownloadAsset(_assetUrls[i], ManageDownloadAsset);
                    }
                    //do a switch based on the file extention if its a .zip then process the zip, if its not then just download it
                }
            }
        }

        private void ManageDownloadAsset(LoadedAsset _asset) 
        {
            if (!ByteArrayToFile(_asset.FileName, _asset.AssetBytes))
            {
                Debug.Log($"Failed to write bytes to file: {_asset.FileName}");
            }

            UpdateCompleate();
        }

        private bool ByteArrayToFile(string fileName, byte[] byteArray)
        {
            string path = $"{writeLocation}/{assetFolder}/{fileName}";
            try
            {
                using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(byteArray, 0, byteArray.Length);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in process: {0}", ex);
                return false;
            }
        }

        private string FileName(string _url) 
        {
            return Path.GetFileName(_url);
        }

        private string GetFileExtention(string _url) 
        {
            return Path.GetExtension(_url);
        }

        public void UpdateCompleate() 
        {
            currentAsset++;
            Debug.Log($"Asset Download Compleate: {currentAsset} / {downloadCount}");
            if (downloadCount == currentAsset) 
            {
                if (downloadingCompleate != null) 
                {
                    downloadingCompleate();
                }
            }
        }
    }
}