using SP.AssetRequestor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    string assetFolder = "Assets";
    DownloadRequestConfig donwloaderConfig;
    AssetProcessorConfig assetProcessorConfig;
    void Start()
    {

        donwloaderConfig = gameObject.AddComponent<DownloadRequestConfig>();
        assetProcessorConfig = gameObject.AddComponent<AssetProcessorConfig>();

        donwloaderConfig.DownloadRequest(new string[]
        {
            "http://ipv4.download.thinkbroadband.com:8080/10MB.zip",
            "http://ipv4.download.thinkbroadband.com:8080/20MB.zip",
            "https://test-videos.co.uk/vids/bigbuckbunny/mp4/h264/1080/Big_Buck_Bunny_1080_10s_1MB.mp4",
            "https://sample-videos.com/img/Sample-png-image-500kb.png"
        },
            FinishedDownload,
            assetFolder,
            Application.persistentDataPath);
    }

    void FinishedDownload() 
    {
        Debug.Log("All assets downloaded");
        assetProcessorConfig.ProcessAssets(Application.persistentDataPath, FinishedAssetLoading, assetFolder);
    }

    [SerializeField]
    Image img;
    void FinishedAssetLoading() 
    {
        Debug.Log("All assets processed");
        img.sprite = AssetProvider.Providers.spriteProvider.GetAsset("Sample-png-image-500kb");
    }
}
