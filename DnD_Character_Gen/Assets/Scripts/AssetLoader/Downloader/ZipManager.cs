using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZipManager
{
    private string writeLocation;
    private Action completeCallback;
    private int zipsDownloaded;
    private string assetFolder;
    public ZipManager(string _writeLocation, Action _compeleteDownload, string _assetFolder) 
    {
        completeCallback = _compeleteDownload;
        writeLocation = _writeLocation;
        assetFolder = _assetFolder;
    }

    public void SetDownloadLocation(string _location) 
    {
        writeLocation = _location;
    }

    public void HandleDownloadedZip(LoadedAsset _zip) 
    {
        if (_zip == null)
        {
            Debug.Log("No asset to download");
        }
        else {
            UnzipFromBytes(_zip.AssetBytes, writeLocation + "/" + assetFolder, HandleZipUnpacked);
        }
    }

    private void HandleZipUnpacked(bool _success) 
    {
        if (!_success)
        {
            Debug.Log("Failed to unpack zip");
        }

        if (completeCallback != null) 
        {
            completeCallback();
        }
    }

    private void UnzipFromBytes(byte[] _zipBytes, string _unzipToPath, Action<bool> _completed = null)
    {
        try
        {
#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_WSA_10_0)
            lzip.setEncoding(1);
#endif
            int[] progress = new int[1];
            int[] progress2 = new int[1];

            int zipResult = lzip.decompress_File(null, _unzipToPath, progress, _zipBytes, progress2);

            bool success = zipResult > 0;
            if (!success)
            {
                Debug.Log("Zip failed to decompress");
            }

            if (_completed != null) _completed(success);
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to unzip file from bytes ==> " + e.ToString());
            if (_completed != null) _completed(false);
        }
    }
}
