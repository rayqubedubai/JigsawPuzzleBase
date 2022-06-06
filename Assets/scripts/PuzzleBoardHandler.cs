using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PuzzleBoardHandler : MonoBehaviour
{
    public PieceHandler[] allBoardBoxes;
    public Sprite currentImage;
    public GameObject selectedPuzzleBlock;
    public float rMinX, rMaxX, lMinX, lMaxX, yMin, yMax;
    public float pMinX, pMaxX, pMinY, pMaxY;
    public float timer;
    public int score;
    public GameObject BGVideo;
    public string path;
    // Start is called before the first frame update
    void Start()
    {
        //BGVideo.GetComponent<VideoPlayer>().url = "C:\\Users\\ibrah\\Downloads\\Video\\Landscape.mp4";
        //BGVideo.GetComponent<VideoPlayer>().url = "C:\\Users\\97158\\Downloads\\Landscape.mp4";
        //resetPuzzle();
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

            BGVideo.GetComponent<VideoPlayer>().url = path + "\\Videos\\portait.mp4";
        }
        else
        {
            BGVideo.GetComponent<VideoPlayer>().url = path + "\\Videos\\landscape.mp4";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && SceneHandler.Instance.isGamePlay)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            Debug.Log("object clicked: " + hit.collider.name);
            if (hit.transform.CompareTag("PuzzleBlock"))
            {
                if (!hit.transform.GetComponent<PieceHandler>().isOnRightPosition && !hit.transform.GetComponent<PieceHandler>().replacing)
                {
                    selectedPuzzleBlock = hit.transform.gameObject;
                }
            }
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (selectedPuzzleBlock != null)
            {
                selectedPuzzleBlock.GetComponent<PieceHandler>().checkCurrentPosition();
                selectedPuzzleBlock = null;
            }
        }
    }

    private void FixedUpdate()
    {
        if (selectedPuzzleBlock != null)
        {
            Vector3 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selectedPuzzleBlock.transform.position = new Vector3(mPos.x, mPos.y, 0);
        }
    }

    public void changePuzzle()
    {
        foreach (PieceHandler p in allBoardBoxes)
        {
            p.gameObject.GetComponent<SpriteRenderer>().sprite = currentImage;
        }
    }

    public void onClickChangePic()
    {
        changePuzzle();
    }

    public void resetPuzzle()
    {
        score = 0;
        StartCoroutine(resetPuzzleWithDelay());
    }

    IEnumerator resetPuzzleWithDelay()
    {
        yield return new WaitForSeconds(3);
        int temp;
        
        if (!SceneHandler.Instance.isPortrait)
        {
            foreach (PieceHandler p in allBoardBoxes)
            {
                temp = Random.Range(0, 2);
                if (temp == 0)
                {
                    Vector3 newPosition = new Vector3(Random.Range(rMinX, rMaxX), Random.Range(yMin, yMax), 0);
                    p.replacePiece(newPosition);
                }
                else if (temp == 1)
                {
                    Vector3 newPosition = new Vector3(Random.Range(lMinX, lMaxX), Random.Range(yMin, yMax), 0);
                    p.replacePiece(newPosition);
                }
            }
            SceneHandler.Instance.menuManager.gamePlayHandler.gameObject.SetActive(true);
            SceneHandler.Instance.menuManager.gamePlayHandler.startTimer(SceneHandler.Instance.GameTime);
        }
        else
        {
            foreach (PieceHandler p in allBoardBoxes)
            {
                Vector3 newPosition = new Vector3(Random.Range(pMinX, pMaxX), Random.Range(pMinY, pMaxY), 0);
                p.replacePiece(newPosition);
            }
            SceneHandler.Instance.menuManagerPortrait.gamePlayHandler.gameObject.SetActive(true);
            SceneHandler.Instance.menuManagerPortrait.gamePlayHandler.startTimer(SceneHandler.Instance.GameTime);
        }
    }

    public void incrementScore()
    {
        score++;
        if (score >= allBoardBoxes.Length)
        {
            selectedPuzzleBlock = null;
            foreach (PieceHandler p in allBoardBoxes)
            {
                p.setToCorrectPosition();
            }
            //APIHandler.Instance.getAllUsers();
            SceneHandler.Instance.showConfetti();
            SceneHandler.Instance.isGamePlay = false;
            Debug.Log("You win");
            SceneHandler.Instance.menuManager.gamePlayHandler.gameEndText.text = "Congratulations You completed the puzzle, Your score is " + score;
            SceneHandler.Instance.playerData.score = score;
            SceneHandler.Instance.playerData.played_at = "2022-12-20 12:25:20";
            Response[] newArray = new Response[APIHandler.Instance.root.Length + 1];
            for (int i = 0; i < APIHandler.Instance.root.Length; i++)
            {
                newArray[i] = APIHandler.Instance.root[i];
            }
            newArray[newArray.Length - 1] = new Response();
            newArray[newArray.Length - 1].name = SceneHandler.Instance.playerData.name;
            newArray[newArray.Length - 1].score = SceneHandler.Instance.playerData.score;
            newArray[newArray.Length - 1].phone = SceneHandler.Instance.playerData.phone;
            newArray[newArray.Length - 1].email = SceneHandler.Instance.playerData.email;
            if (!APIHandler.Instance.checkInternet())
            {
                newArray[newArray.Length - 1].isOnServer = false;
            }
            else
            {
                newArray[newArray.Length - 1].isOnServer = true;
            }
            CSVEditor.Instance.writeOnFile(newArray);
            APIHandler.Instance.sendUserStats(SceneHandler.Instance.playerData);
            SceneHandler.Instance.menuManager.gamePlayHandler.gameEndText.gameObject.SetActive(true);
            SceneHandler.Instance.menuManager.gamePlayHandler.leaderBoard.SetActive(true);
            SceneHandler.Instance.menuManager.gamePlayHandler.leaderBoard.GetComponent<TweenPosition>().PlayForward();
            Debug.Log("Put stats");
            SceneHandler.Instance.menuManager.gamePlayHandler.putStats(APIHandler.Instance.root);
            this.gameObject.SetActive(false);
            //SceneHandler.Instance.playerData.score971525913255
        }
    }

    public void setToCorrectPosition()
    {
        score = 0;
        timer = 0;
        foreach (PieceHandler p in allBoardBoxes)
        {
            p.setToCorrectPosition();
        }
    }
}
