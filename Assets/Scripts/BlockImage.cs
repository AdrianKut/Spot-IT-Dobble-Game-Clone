using TMPro;
using UnityEngine;

public class BlockImage : MonoBehaviour
{
    [SerializeField]
    private float timeleft = 3f;

    private TextMeshProUGUI textMeshProUGUI;

    private void Awake()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        GameManager.Instance.UnityEventCorrectIcon.AddListener(DestroyOnDemand);
    }

    private void OnDisable()
    {
        GameManager.Instance.UnityEventCorrectIcon.RemoveListener(DestroyOnDemand);
    }

    void DestroyOnDemand() => Destroy(this.gameObject);

    void FixedUpdate()
    {
        CoutdownToUnlockClickOnImage();
    }

    private void CoutdownToUnlockClickOnImage()
    {
        timeleft -= Time.deltaTime;
        if (timeleft <= 0f)
        {
            Destroy(this.gameObject);
        }
        textMeshProUGUI.text = "" + (int)timeleft;
    }
}
