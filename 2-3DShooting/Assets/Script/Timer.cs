using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public GameObject time_object;
    public GameObject delta_object;

    private float time;
    private float delta;

    void Update()
    {
        time = Time.time;
        delta += Time.deltaTime;
        Text timeText = time_object.GetComponent<Text>();
        timeText.text = "Time:" + time.ToString("0000");
        Text deltaText = delta_object.GetComponent<Text>();
        deltaText.text = "Time:" + delta.ToString("0000");
    }
}
