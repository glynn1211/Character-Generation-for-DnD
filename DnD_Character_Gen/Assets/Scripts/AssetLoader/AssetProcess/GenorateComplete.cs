using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GenorateComplete
{

    readonly string[] imageFileExtentions = {".png",".jpg"};
    readonly string[] videoFileExtentions = {".mp4"};
    readonly string[] textFileExtention = {".json", ".txt"}; //this could be a generic text file

    readonly string[] folders = 
    {
        "/pdf", 
        "/image",
        "/texture",
        "/video",
        "/json"
    };


    int assetIndex = 0;
    List<Asset> assetData = new List<Asset>();
    public Assets Genorate(string _readLocation, bool _useOrganisedFolder) 
    {
        //chose if it folders or not
        if (_useOrganisedFolder)
        {
            HandlePDF(_readLocation + folders[0]);
        }
        else 
        {
            HandlePDF(_readLocation);
            HandleAssets(_readLocation);
        }

        Assets a = new Assets();
        a.assets = assetData.ToArray();

        return a;
    }

    void HandleAssets(string _path)
    {
        foreach (string file in Directory.GetFiles(_path)) 
        {
            string title = Path.GetFileNameWithoutExtension(file);
            string fileName = Path.GetFileName(file);
            if (CheckExtention(file, videoFileExtentions)) 
            {
                AddAsset(new Asset(assetIndex, fileName, title, AssetTypes.Video));
            }

            if (CheckExtention(file, textFileExtention)) 
            {
                AddAsset(new Asset(assetIndex, fileName, title, AssetTypes.Json));
            }

            if (CheckExtention(file, imageFileExtentions))
            {
                AddAsset(new Asset(assetIndex, fileName, title, AssetTypes.Image));
            }
        }
    }

    bool CheckExtention(string _path, string[] _extentionTypes) 
    {
        string e = Path.GetExtension(_path);
        foreach (string extention in _extentionTypes)
        {
            if (e == extention) 
            {
                return true;
            }
        }
        return false;
    }

    void HandlePDF(string _readLoaction)
    {
        foreach (string dir in Directory.GetDirectories(_readLoaction))
        {
            AddAsset(PdfData(dir));
        }
    }

    void AddAsset(Asset asset) 
    {
        if (asset != null)
        {
            assetIndex++;
            assetData.Add(asset);
        }
    }

    Asset PdfData(string _path)
    {
        int pageCount = 0;
        string title = "";
        string fileName = "";
        foreach (string file in Directory.GetFiles(_path))
        {
            if (String.IsNullOrEmpty(title)) 
            {
                fileName = Path.GetFileName(Path.GetDirectoryName(file));
                title = Path.GetFileNameWithoutExtension(Path.GetDirectoryName(file));
            }

            if (CheckFileExtention(file))
            {
                pageCount++;
            }
            else 
            {
                Debug.LogWarning($"File found in inccorect location, removing:{file}");
                File.Delete(file);
            }
        }

        if (pageCount == 0) 
        {
            return null;
        }

        return new Asset(assetIndex, title, title, AssetTypes.PDF ,pageCount);
    }

    bool CheckFileExtention(string _path) 
    {
        string e = Path.GetExtension(_path);
        for (int i = 0; i < imageFileExtentions.Length; i++)
        {
            if (imageFileExtentions[i] == e) 
            {
                return true;
            }
        }
        return false;
    }
}
