using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{

    private TextMeshProUGUI textTimer;

    [SerializeField]
    private float timeleft = 99;

    // Start is called before the first frame update
    void Start()
    {
        textTimer = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((int)timeleft > 0)
        {
            timeleft -= Time.deltaTime * 1;
            textTimer.text = "" + (int)timeleft;
        }
        else
        {
            GameManager.instance.GameOverOneVsOneWithTimer();
        }
    }
}
