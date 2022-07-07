using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VibrationManager : MonoBehaviour
{
    
    [SerializeField]
    private Sprite vibrationOnSprite;

    [SerializeField]
    private Sprite vibrationOffSprite;

    private Button button;

    [SerializeField]
    private bool isVibration = true;
    
    [SerializeField]
    private int vibration = 1; // 1 - On | 0 - Off
    public bool GetVibration() => isVibration == true ? true : false;

    private VibrationManager() { }
    public static VibrationManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    void Start()
    {
        vibration = SaveData.instance.vibration;
        button = GetComponent<Button>();
        button.onClick.AddListener(ChangeVibration);
        
        SetVibration();
    }

    private void SetVibration()
    {
        if (vibration == 0)
        {
            SetButtonSprite(true,vibrationOnSprite);
        }
        else if (vibration == 1)
        {
            SetButtonSprite(false, vibrationOffSprite);
        }
    }

    private void SetButtonSprite(bool enabled, Sprite sprite)
    {
        isVibration = enabled;
        button.image.sprite = sprite;
    }


    private void ChangeVibration()
    {
        if (isVibration == false)
        {
            SetButtonSprite(true, vibrationOnSprite);
            vibration = 0;
            SaveData.instance.vibration = vibration;
            SaveData.instance.Save();
        }
        else if (isVibration == true)
        {
            SetButtonSprite(false, vibrationOffSprite);
            vibration = 1;
            SaveData.instance.vibration = vibration;
            SaveData.instance.Save();
        }
    }
}
