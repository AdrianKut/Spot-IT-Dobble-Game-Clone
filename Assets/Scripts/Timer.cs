using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private float timeleft = 99;

    private TextMeshProUGUI textTimer;
    private void Awake()
    {
        textTimer = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        Countdown();
    }

    private void Countdown()
    {
        if ((int)timeleft > 0)
        {
            timeleft -= Time.deltaTime * 1;
            textTimer.text = "" + (int)timeleft;
        }
        else
        {
            GameManager.Instance.GameOverOneVsOneWithTimer();
        }
    }
}
