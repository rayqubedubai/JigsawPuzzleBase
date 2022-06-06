using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;
using UnityEngine.UI;

public class IntroHandler : MonoBehaviour
{

    public VideoClip introVideoLandscape;
    public VideoClip introVideoPortrait;
    public VideoPlayer introVideoPlayer;
    public TMP_Text tapToPlayText;
    public Image logo;
    public bool tapped = false;
    bool startTimer = false;
    float timer;
    string path;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    private void OnEnable()
    {
        //introVideoPlayer.clip = introVideoLandscape;
        //introVideoPlayer.url = "C:\\Users\\ibrah\\Downloads\\Video\\Landscape.mp4";
        path = Application.dataPath;
        if (Application.platform == RuntimePlatform.OSXPlayer)
        {
            path += "/../../";
        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            path += "/../";
        }
        if (SceneHandler.Instance.isPortrait)
        {
            
            introVideoPlayer.url = path + "\\Videos\\portait.mp4";
        }
        else
        {
            introVideoPlayer.url = path + "\\Videos\\landscape.mp4";
        }
        //introVideoPlayer.url = "C:\\Users\\97158\\Downloads\\Landscape.mp4";
        //if (Screen.orientation == ScreenOrientation.Landscape || Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight)
        //{
        //    introVideoPlayer.clip = introVideoLandscape;
        //}else if(Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown)
        //{
        //    introVideoPlayer.clip = introVideoPortrait;
        //}
        introVideoPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) /*&& Application.internetReachability != NetworkReachability.NotReachable*/)
        {
            if (!tapped)
            {
                tapped = true;
                tapToPlayText.GetComponent<textFadeInNOut>().fadeOut();
                logo.GetComponent<Animator>().SetTrigger("animate");
                timer = 0;
                startTimer = true;
                logo.GetComponent<TweenPosition>().enabled = true;
                logo.GetComponent<TweenScale>().enabled = true;
                logo.GetComponent<TweenPosition>().PlayForward();
                logo.GetComponent<TweenScale>().PlayForward();
            }
        }
        if (startTimer)
        {
            timer += Time.deltaTime;
            if (timer >= 1)
            {
                if (!SceneHandler.Instance.isPortrait)
                {
                    SceneHandler.Instance.menuManager.registrationHandler.showRegistrationPanel();
                }
                else
                {
                    SceneHandler.Instance.menuManagerPortrait.registrationHandler.showRegistrationPanel();
                }
                startTimer = false;
            }
        }
    }

    public void fadeIntroVideo()
    {
        StartCoroutine(FadeImageToZeroAlpha(2));
    }

    public IEnumerator FadeImageToZeroAlpha(float t)
    {
        RawImage i = introVideoPlayer.GetComponent<RawImage>();
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }

    public void unfadeIntroVideo()
    {
        StartCoroutine(unfadeImageToZeroAlpha(2));
    }

    public IEnumerator unfadeImageToZeroAlpha(float t)
    {
        RawImage i = introVideoPlayer.GetComponent<RawImage>();
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a < 1)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    public void Reset()
    {
        tapped = false;
        logo.GetComponent<TweenPosition>().enabled = false;
        logo.GetComponent<TweenScale>().enabled = false;
    }
}
