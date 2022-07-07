using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameObjectGameModeView;

    [SerializeField]
    private GameObject gameObjectGameHelpView;

    [SerializeField]
    private GameObject gameObjectGameChangeSkinView;

    [SerializeField]
    private TextMeshProUGUI textBestScore;

    private int bestScore = 0;
    private void Awake()
    {
        bestScore = SaveData.Instance.BestScore;
        SetText(textBestScore, "BEST SCORE: " + bestScore);
    }

    private void SetText(TextMeshProUGUI textMeshProUGUI, string text)
    {
        textMeshProUGUI.text = text;
    }

    private void Start()
    {
        SetText(textBestScore, "BEST SCORE: " + bestScore);
    }

    public void ShowView(GameObject gameObjectToShow)
    {
        EnableViewGameObject(gameObjectToShow, true, new Vector3(1f, 1f, 1f));
    }

    public void HiddeGameObject(GameObject gameObjectToHide)
    {
        EnableViewGameObject(gameObjectToHide, false, new Vector3(0f, 0f, 0f));
    }

    private static void EnableViewGameObject(GameObject gameObject, bool activate, Vector3 vector3)
    {
        gameObject.SetActive(activate);

        gameObject.transform.localScale = new Vector3(0, 0, 0);
        gameObject.transform.LeanScale(vector3, .5f).setEaseOutQuart();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
