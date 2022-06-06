using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FontCreator : MonoBehaviour
{
    string path, filepath;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        StartCoroutine(changeFontWithDelay());
    }

    IEnumerator changeFontWithDelay()
    {
        //string[] fontPaths = Font.GetPathsToOSFonts();
        //for (int i = 0; i < fontPaths.Length; i++)
        //{
        //    yield return new WaitForSeconds(5);
        //    Font osFont = new Font(fontPaths[i]);
        //    Debug.Log("Path: " + fontPaths[i]);
        //    Debug.Log("Font name: " + osFont);
        //    TMP_FontAsset fontAsset = TMP_FontAsset.CreateFontAsset(osFont);
        //    //Debug.Log(fontAsset.material);
        //    GetComponent<TMP_Text>().font = fontAsset;
        //}
        yield return new WaitForSeconds(0.2f);
        path = Application.dataPath;
        if (Application.platform == RuntimePlatform.OSXPlayer)
        {
            path += "/../../";
        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            path += "/../";
        }
        filepath = path + "\\Font\\FONT.TTF";
        Font osFont = new Font(filepath);
        TMP_FontAsset fontAsset = TMP_FontAsset.CreateFontAsset(osFont);
        GetComponent<TMP_Text>().font = fontAsset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
