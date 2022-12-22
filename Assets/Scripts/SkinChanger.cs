using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinChanger : MonoBehaviour
{
    public void ChangeSkin(string skinType)
    {
        SaveData.Instance.SkinType = skinType;
        SaveData.Instance.Save();

        transform.parent.gameObject.SetActive(false);
    }
}
