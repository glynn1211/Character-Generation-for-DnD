using SP.AssetRequestor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AssetProcessorConfig : MonoBehaviour
{
    AssetProvider provider;
    AssetProcessor processor;
    GenorateComplete genorateComplete;

    private const string completeFile = "/complete.json";
    bool isInitalised = false;
    Assets assetsData;
    Action compleatedAssetProcessing;
    int totalAssetsToProcess = 0;
    int currentAsset = 0;
    string readLocation;
    public void Inintalise() 
    {
        if (!isInitalised) 
        {
            isInitalised = !isInitalised;
            provider = AssetProvider.Providers;
            processor = new AssetProcessor(AssetProcessed, provider);
            genorateComplete = new GenorateComplete();
        }
    }

    public void ProcessAssets(string _readLocation, Action _compleatedAssetLoading, string _assetFolder, Assets _complete = null) 
    {
        Debug.Log("Processing Assets");
        //if the complete is not passed to this methoud the app looks for it in the read location
        //it then stores it in its local data
        Inintalise();
        compleatedAssetProcessing = _compleatedAssetLoading;
        readLocation = _readLocation + "/" + _assetFolder;

        //set the read location of the assets to process
        processor.SetReadLocation(readLocation);

        if (_complete == null)
        {
            if (File.Exists(readLocation + completeFile))
            {
                _complete = GetAssetData(readLocation);
            }
            else {
                Debug.Log("No complete, generating");
                _complete = genorateComplete.Genorate(readLocation, false);
            }
        }

        assetsData = _complete;

        //write the updated compleate to the write location
        WriteComplete(readLocation + completeFile);

        if (assetsData != null)
        {
            Process();
        }
    }

    private void Process() 
    {
        totalAssetsToProcess = GetTotalAssets();

        for (int i = 0; i < assetsData.assets.Length; i++)
        {
            Asset asset = assetsData.assets[i];
            switch (assetsData.assets[i].assetType)
            {
                case AssetTypes.Image:
                    processor.ProcessImage(GetFileBytes(asset.filePath), asset.title, asset.id);
                    break;
                case AssetTypes.PDF:
                    //Maybe do a for eahc asset in folder location ?
                    for (int j = 0; j < asset.pages; j++)
                    {
                        //TODO need to do a check on the extention type
                        processor.ProcessPDF(GetFileBytes(asset.filePath + "/" + i + ".png"), asset.title, asset.id);
                    }
                    break;
                case AssetTypes.Json:
                    processor.ProcessJson(GetFileBytes(asset.filePath), asset.title, asset.id);
                    break;
                case AssetTypes.Texture:
                    //processor.ProcessTexture(GetFileBytes(asset.filePath), asset.title, asset.id);
                    break;
                case AssetTypes.Video:
                    //ajust the file path here ?
                    //also need to get its file extention
                    processor.ProcessVideo(asset.filePath, asset.title, asset.id);
                    break;
            }
        }
    }

    private int GetTotalAssets() 
    {
        int assets = 0;

        for (int i = 0; i < assetsData.assets.Length; i++)
        {
            if (assetsData.assets[i].assetType == AssetTypes.PDF)
            {
                assets += assetsData.assets[i].pages;
            }
            else {
                assets++;
            }
        }

        return assets;
    }
    private void AssetProcessed() 
    {
        currentAsset++;
        Debug.Log($"Processd asset: {currentAsset}/{totalAssetsToProcess}");

        if (currentAsset == totalAssetsToProcess) 
        {
            if (compleatedAssetProcessing != null) 
            {
                compleatedAssetProcessing();
            }
        }
    }

    private LoadedAsset GetFileBytes(string _fileLocation) 
    {
        return new LoadedAsset(Path.GetFileName(readLocation + "/" + _fileLocation) ,File.ReadAllBytes(readLocation + "/" +_fileLocation));
    }

    //get data from local complete
    private Assets GetAssetData(string _readLocation) 
    {
        return JsonUtility.FromJson<Assets>(File.ReadAllText(_readLocation + completeFile));
    }
    //write local complete
    private void WriteComplete(string _filePath) 
    {
        File.WriteAllText(_filePath, JsonUtility.ToJson(assetsData, true));
    }
}
