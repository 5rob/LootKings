using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Fade : MonoBehaviour
{
    RawImage img;
    public float timer;
    public float timerStart;
    public float fadeSpeed;

    private void Start()
    {
        img = GetComponentInChildren<RawImage>();
        timer = Random.Range(30f, 60f);
        fadeSpeed = 0.025f;
        timerStart = timer;

    }

    private void FixedUpdate()
    {
        timer -= fadeSpeed;
        Color temp = img.color;
        temp.a = timer / timerStart;
        img.color = temp;

        if(timer <=0f)
        {
            Destroy(this.gameObject);
        }
    }
}
