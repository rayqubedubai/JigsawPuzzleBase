using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class GamePlayHandler : MonoBehaviour
{
    public TMP_Text timerText;
    public TMP_Text gameEndText;
    public GameObject leaderBoard;
    public Slider timerSlider;
    public bool startTime = false;
    public RectTransform content;
    public GameObject statsPrefab;
    public List<GameObject> allStats;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startTime && SceneHandler.Instance.isGamePlay)
        {
            timer -= Time.deltaTime;
            timerText.text = ((int)timer).ToString();
            timerSlider.value = timer;
            if (timer <= 0)
            {
                //timerText.gameObject.SetActive(false);
                timerSlider.gameObject.SetActive(false);
                startTime = false;
                SceneHandler.Instance.timeUp();
            }
        }
    }

    public void startTimer(float time)
    {
        timer = time;
        startTime = true;
        timerSlider.minValue = 0;
        timerSlider.maxValue = time;
        timerSlider.value = timer;
        timerSlider.gameObject.SetActive(true);
    }

    public void putStats(Response[] allPlayers)
    {
        if (allPlayers == null)
            return;
        clearAllData();
        int count = 1;
        content.sizeDelta = new Vector2(1200, allPlayers.Length * 125); 
        foreach (Response r in allPlayers)
        {
            r.rank = count;
            GameObject temp;
            temp = Instantiate(statsPrefab, content.transform);
            DataBlock newData = temp.GetComponent<DataBlock>();
            newData.setStats(r.name, r.rank, r.score);
            allStats.Add(temp);
            count++;
        }
    }

    public void clearAllData()
    {
        foreach (GameObject g in allStats)
        {
            GameObject temp = g;
            Destroy(temp);
        }
        allStats.Clear();
    }
}
