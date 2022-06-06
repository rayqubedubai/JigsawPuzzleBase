using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class LoadSprite : MonoBehaviour
{
    public string assetName;
    string path;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnEnable()
    {
        path = Application.dataPath;
        if (Application.platform == RuntimePlatform.OSXPlayer)
        {
            path += "/../../";
        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            path += "/../";
        }
        GetComponent<Image>().sprite = LoadImageAsSprite(path + "\\UI\\Landscape\\" + assetName );
        Debug.Log(path + "\\UI\\Landscape\\" + assetName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Texture2D LoadImage(string path)
    {
        if (File.Exists(path))
        {
            byte[] bytes = File.ReadAllBytes(path);
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(bytes);
            return tex;
        }
        else
        {
            return null;
        }
    }
    public Sprite LoadImageAsSprite(string path)
    {
        //string cPath = path + assetName;
        Sprite sprite = Sprite.Create(LoadImage(path), new Rect(0.0f, 0.0f, LoadImage(path).width,
        LoadImage(path).height), new Vector2(0.5f, 0.5f), 100.0f);
        return sprite;
    }
}
