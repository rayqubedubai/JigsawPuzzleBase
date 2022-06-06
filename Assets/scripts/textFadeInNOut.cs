using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class textFadeInNOut : MonoBehaviour
{
    public float time;
    void Update()
    {

    }

    public void fadeIn()
    {
        StartCoroutine(FadeTextToFullAlpha(time, this.GetComponent<TextMeshProUGUI>()));
    }

    public void fadeOut()
    {
        StartCoroutine(FadeTextToZeroAlpha(time, this.GetComponent<TextMeshProUGUI>()));
    }

    public IEnumerator FadeTextToFullAlpha(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
