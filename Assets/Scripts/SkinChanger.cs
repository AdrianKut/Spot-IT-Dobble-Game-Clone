using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinChanger : MonoBehaviour
{
    public void ChangeSkin(string skinType)
    {
        SaveData.instance.skinType = skinType;
        SaveData.instance.Save();

        transform.parent.gameObject.SetActive(false);
    }
}
