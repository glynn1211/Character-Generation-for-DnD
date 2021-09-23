using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SP.AssetRequestor
{
    public class AssetProvider
    {
        private static AssetProvider _providerInstance;

        public static AssetProvider Providers {
            get
            {
                if (_providerInstance == null) 
                {
                    _providerInstance = new AssetProvider();
                }
                return _providerInstance; 
            }
        }

        public VideoProvider videoProvider { get; private set; }
        public PdfProvider pdfProvider { get; private set; }
        public SpriteProvider spriteProvider { get; private set; }
        public JsonProvider jsonProvider { get; private set; }

        public AssetProvider() 
        {
            if (_providerInstance == null)
            {
                _providerInstance = this;
            }

            videoProvider = new VideoProvider();
            pdfProvider = new PdfProvider();
            spriteProvider = new SpriteProvider();
            jsonProvider = new JsonProvider();
        }

        public void AssignImage(int _id, string _title, Sprite _sprite) 
        {
            spriteProvider.AddAsset(_id, _title, _sprite);
        }

        public void AssingVideo(int _id, string _title, string _path) 
        {
            videoProvider.AddAsset(_id, _title, _path);
        }

        public void AssignJson(int _id, string _title, string _data)
        {
            jsonProvider.AddAsset(_id, _title, _data);
        }

        public void AssignTexture(int _id, string _title, Texture2D _texture)
        {
        }

        public void AssignPDF(int _id, string _title, Sprite _sprite)
        {
            pdfProvider.NewAsset(_id, _title, _sprite);
        }
    }

    public class VideoProvider
    {
        Dictionary<int, string> videos = new Dictionary<int, string>();
        Dictionary<string, int> refrence = new Dictionary<string, int>();

        public void AddAsset(int _id, string _title, string _path)
        {
            if (!videos.ContainsKey(_id)) 
            {
                videos.Add(_id, _path);
                refrence.Add(_title, _id);
            }
        }

        public string GetAsset(string _title)
        {
            if (refrence.ContainsKey(_title))
            {
                return videos[refrence[_title]];
            }
            return null;
        }

        public string GetAsset(int _id)
        {
            if (videos.ContainsKey(_id))
            {
                return videos[_id];
            }
            return null;
        }
    }

    public class PdfProvider
    {
        Dictionary<int, List<Sprite>> pdfs = new Dictionary<int, List<Sprite>>();
        Dictionary<string, int> refrence = new Dictionary<string, int>();

        public void NewAsset(int _id, string _title, Sprite _page) 
        {
            if (pdfs.ContainsKey(_id))
            {
                pdfs[_id].Add(_page);
            }
            else 
            {
                List<Sprite> data = new List<Sprite>();
                data.Add(_page);
                pdfs.Add(_id, data);
                refrence.Add(_title, _id);
            }
        }

        public Sprite[] GetAsset(string _title) 
        {
            if (refrence.ContainsKey(_title)) 
            {
                return pdfs[refrence[_title]].ToArray();
            }
            return null;
        }

        public Sprite[] GetAsset(int _id)
        {
            if (pdfs.ContainsKey(_id)) 
            {
                return pdfs[_id].ToArray();
            }
            return null;
        }
    }

    public class SpriteProvider
    {
        Dictionary<int, Sprite> spries = new Dictionary<int, Sprite>();
        Dictionary<string, int> refrence = new Dictionary<string, int>();

        public void AddAsset(int _id, string _title, Sprite _sprite) 
        {
            if (!spries.ContainsKey(_id)) 
            {
                spries.Add(_id, _sprite);
                refrence.Add(_title, _id);
            }
        }

        public Sprite GetAsset(string _title)
        {
            if (refrence.ContainsKey(_title))
            {
                return spries[refrence[_title]];
            }
            return null;
        }

        public Sprite GetAsset(int _id)
        {
            if (spries.ContainsKey(_id))
            {
                return spries[_id];
            }
            return null;
        }
    }

    public class JsonProvider
    {
        Dictionary<int, string> json = new Dictionary<int, string>();
        Dictionary<string, int> refrence = new Dictionary<string, int>();
        public void AddAsset(int _id, string _title, string _data)
        {
            if (!json.ContainsKey(_id))
            {
                json.Add(_id, _data);
                refrence.Add(_title, _id);
            }
        }

        public string GetAsset(string _title)
        {
            if (refrence.ContainsKey(_title))
            {
                return json[refrence[_title]];
            }
            return null;
        }

        public string GetAsset(int _id)
        {
            if (json.ContainsKey(_id))
            {
                return json[_id];
            }
            return null;
        }
    }
}