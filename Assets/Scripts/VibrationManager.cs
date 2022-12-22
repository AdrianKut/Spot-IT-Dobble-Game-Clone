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
    private int vibration = 1; // 1 - On | 0 - Off

    public bool IsVibration = true;
    public static VibrationManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        button = GetComponent<Button>();
    }


    void Start()
    {
        vibration = SaveData.Instance.Vibration;
        button.onClick.AddListener(ChangeVibration);

        SetVibration();
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(ChangeVibration);
    }

    private void SetVibration()
    {
        if (vibration == 0)
        {
            SetButtonSprite(true, vibrationOnSprite);
        }
        else if (vibration == 1)
        {
            SetButtonSprite(false, vibrationOffSprite);
        }
    }

    private void SetButtonSprite(bool enabled, Sprite sprite)
    {
        IsVibration = enabled;
        button.image.sprite = sprite;
    }


    private void ChangeVibration()
    {
        if (IsVibration == false)
        {
            SetButtonSprite(true, vibrationOnSprite);
            SaveVibrationOnOff(0, vibration);
        }
        else if (IsVibration == true)
        {
            SetButtonSprite(false, vibrationOffSprite);
            SaveVibrationOnOff(1, vibration);
        }
    }

    private void SaveVibrationOnOff(int value, int vibration)
    {
        this.vibration = value;
        SaveData.Instance.Vibration = vibration;
        SaveData.Instance.Save();
    }
}
