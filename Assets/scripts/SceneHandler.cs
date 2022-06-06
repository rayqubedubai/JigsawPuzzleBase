using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SceneHandler : Singleton<SceneHandler>
{
    public PuzzleBoardHandler boardLandScape;
    public PuzzleBoardHandler boardPortrait;
    public MenuManager menuManager;
    public MenuManager menuManagerPortrait;
    public MenuManager menuManagerLandscape;
    public bool isGamePlay = false;
    public PuzzleBoardHandler puzzleBoard;
    public string baseAssetsURL;
    public string baseAssetsURLPortrait;
    public Credentials playerData = new Credentials();
    public bool isPortrait = false;
    public int GameTime;
    public string consentPath = "C:\\Users\\ibrah\\Documents\\consent.txt";
    public string consentString;
    public GameObject warningPopup;
    public TMPro.TMP_Text warningMsg;
    public GameObject warningPopupPortrait;
    public TMPro.TMP_Text warningMsgPortrait;
    [SerializeField]
    public string landscapeVideoURL, portraitVideoURL;
    public GameObject[] confettiLandscape, confettiPortrait;
    //public bool settingsButtonShown = false;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Application.dataPath);
        portraitVideoURL = Application.dataPath + "\\Videos\\portait.mp4";
        landscapeVideoURL = Application.dataPath + "/Videos/landscape.mp4";
        //GameTime = PlayerPrefs.GetInt("Time", 0);
        switch (PlayerPrefs.GetInt("Time", 0))
        {
            case 0:
                GameTime = 50;
                break;
            case 1:
                GameTime = 75;
                break;
            case 2:
                GameTime = 100;
                break;
            case 3:
                GameTime = 150;
                break;
            case 4:
                GameTime = 200;
                break;
        }
        if (PlayerPrefs.GetInt("Orientation", 0) == 0)
        {
            isPortrait = false;
        }
        else
        {
            isPortrait = true;
        }
        //puzzleBoard = boardLandScape;
        puzzleBoard = boardPortrait;
        if (isPortrait)
        {
            Screen.orientation = ScreenOrientation.Portrait;
            menuManager = menuManagerPortrait;
            puzzleBoard = boardPortrait;
        }
        else
        {
            Screen.orientation = ScreenOrientation.Landscape;
            menuManager = menuManagerLandscape;
            puzzleBoard = boardLandScape;
        }
        menuManager.gameObject.SetActive(true);
        puzzleBoard.gameObject.SetActive(true);
        readConsentFromFile();
    }

    public void readConsentFromFile()
    {
        StreamReader reader = new StreamReader(consentPath);
        consentString = reader.ReadToEnd();
        menuManager.registrationHandler.consentText.text = consentString;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startGameWithDelay()
    {
        puzzleBoard.resetPuzzle();
        isGamePlay = true;
    }

    public void timeUp()
    {
        if (!isGamePlay)
            return;
        isGamePlay = false;
        if (puzzleBoard.score < puzzleBoard.allBoardBoxes.Length)
        {
            puzzleBoard.gameObject.SetActive(false);
            // Fail Situation
            menuManager.gamePlayHandler.gameEndText.text = "Time's up, Better luck next time, " + playerData.name;
            menuManager.gamePlayHandler.gameEndText.gameObject.SetActive(true);
            menuManager.gamePlayHandler.gameEndText.GetComponent<TweenPosition>().PlayForward();
            menuManager.gamePlayHandler.leaderBoard.SetActive(true);
            menuManager.gamePlayHandler.leaderBoard.GetComponent<TweenPosition>().PlayForward();
            APIHandler.Instance.getAllUsers();
            //menuManager.gamePlayHandler.putStats(APIHandler.Instance.root);
            StartCoroutine(backToIntro());
        }
    }

    public void showConfetti()
    {
        StartCoroutine(showConfettiWithDelay());
    }

    IEnumerator showConfettiWithDelay()
    {
        if (isPortrait)
        {
            foreach (GameObject c in confettiPortrait)
            {
                c.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject c in confettiLandscape)
            {
                c.SetActive(true);
            }
        }
        yield return new WaitForSeconds(3);
        if (isPortrait)
        {
            foreach (GameObject c in confettiPortrait)
            {
                c.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject c in confettiLandscape)
            {
                c.SetActive(false);
            }
        }
    }

    IEnumerator backToIntro()
    {
        yield return new WaitForSeconds(5);
        menuManager.gamePlayHandler.gameEndText.GetComponent<TweenPosition>().PlayReverse();
        menuManager.gamePlayHandler.leaderBoard.GetComponent<TweenPosition>().PlayReverse();
        yield return new WaitForSeconds(1);
        menuManager.gamePlayHandler.gameEndText.gameObject.SetActive(false);
        menuManager.gamePlayHandler.leaderBoard.SetActive(false);
        menuManager.introHandler.unfadeIntroVideo();
        menuManager.introHandler.logo.GetComponent<TweenPosition>().PlayReverse();
        menuManager.introHandler.logo.GetComponent<TweenScale>().PlayReverse();
        menuManager.introHandler.tapToPlayText.GetComponent<textFadeInNOut>().fadeIn();
        yield return new WaitForSeconds(3);
        menuManager.introHandler.Reset();
    }


}
