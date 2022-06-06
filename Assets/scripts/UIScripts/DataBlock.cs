using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DataBlock : MonoBehaviour
{
    public TMP_Text name, rank, score;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setStats(string n, int r, int s)
    {
        name.text = n;
        rank.text = r.ToString();
        score.text = s.ToString();
    }
}
