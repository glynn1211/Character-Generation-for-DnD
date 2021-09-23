using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SP.AssetRequestor
{
    public class AssetProcessor
    {
        Action compleatCallback;

        AssetProvider provider;

        string readLocation;

        public AssetProcessor(Action _compleatCallback, AssetProvider _provider)
        {
            compleatCallback = _compleatCallback;
            provider = _provider;
        }

        public void SetReadLocation(string _readLocation) 
        {
            readLocation = _readLocation;
        }

        public void ProcessPDF(LoadedAsset asset, string key, int id)
        {
            if (provider != null)
            {
                provider.AssignPDF(id, key, GetSpirite(asset));
            }

            if (compleatCallback != null)
            {
                compleatCallback();
            }
        }

        public void ProcessJson(LoadedAsset asset, string key, int id)
        {
            string jsonText = System.Text.Encoding.UTF8.GetString(asset.AssetBytes);
            if (provider != null)
            {
                provider.AssignJson(id, key, jsonText);
            }

            if (compleatCallback != null)
            {
                compleatCallback();
            }
        }

        public void ProcessImage(LoadedAsset asset, string key, int id)
        {
            if (provider != null)
            {
                provider.AssignImage(id, key, GetSpirite(asset));
            }

            if (compleatCallback != null)
            {
                compleatCallback();
            }
        }

        public void ProcessVideo(string filePath, string key, int id)
        {
            if (provider != null)
            {
                provider.AssingVideo(id, key, readLocation + "/" + filePath);
            }

            if (compleatCallback != null)
            {
                compleatCallback();
            }
        }

        private Sprite GetSpirite(LoadedAsset asset) 
        {
#if UNITY_IOS
            Texture2D texture = new Texture2D(1, 1, TextureFormat.PVRTC_RGBA4, false);
#else
            Texture2D texture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
#endif
            texture.LoadImage(asset.AssetBytes);
            texture.Apply();

            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
    }
}