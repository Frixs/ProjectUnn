using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeText : MonoBehaviour
{
    private float fadeTimer;
    private Color alpha;
    // Start is called before the first frame update
    void Start()
    {
        alpha.a = 0f;
        GetComponent<Text>().color = alpha;
        fadeTimer = 2f;
    }
    public void Show()
    {
        alpha.a = 1f;
        fadeTimer = 2f;
        GetComponent<Text>().color = alpha;
    }
    private void Update()
    {
        if (alpha.a > 0)
        {
            fadeTimer -= Time.deltaTime;
            if (fadeTimer < 0)
            {
                float fadeAmount = 2f;
                alpha.a -= fadeAmount * Time.deltaTime;
                GetComponent<Text>().color = alpha;
                
            }
        }
    }
}
