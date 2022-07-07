using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlockImage : MonoBehaviour
{
    [SerializeField]
    private float timeleft = 3f;

    TextMeshProUGUI textMeshProUGUI;
    void Start()
    {
        textMeshProUGUI = this.gameObject.GetComponent<TextMeshProUGUI>();
        GameManager.instance.unityEventCorrectIcon.AddListener(DestroyOnDemand);
    }

    void DestroyOnDemand() => Destroy(this.gameObject);

    void FixedUpdate()
    {
        timeleft -= 1 * Time.deltaTime;
        if (timeleft <= 0f)
        {
            Destroy(this.gameObject);
        }
        textMeshProUGUI.text = "" + (int)timeleft;
    }
}
