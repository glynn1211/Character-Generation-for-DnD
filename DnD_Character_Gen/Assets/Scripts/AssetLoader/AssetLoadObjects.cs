using System;

[Serializable]
public class DBZipPaths
{
    public string url;
}

[Serializable]
public class Assets
{
    public Asset[] assets;
}

public enum AssetTypes
{
    Image,
    Video,
    Json,
    PDF,
    Texture
}
[Serializable]
public class Asset
{
    public int id;
    public string filePath;
    public string title;
    public AssetTypes assetType;
    public int pages;

    public Asset(int _id, string filePath, string title, AssetTypes type, int pages = 0)
    {
        this.id = _id;
        this.filePath = filePath;
        this.title = title;
        this.assetType = type;
        this.pages = pages;
    }
}

[Serializable]
public class ApiData
{
    public DBZipPaths[] zipData;
    public Assets complete;
    public string since;
}

public class LoadedAsset
{
    public string FileName { get; private set; }
    public byte[] AssetBytes { get; private set; }

    public LoadedAsset(string fileName, byte[] bytes)
    {
        FileName = fileName;
        AssetBytes = bytes;
    }
}
