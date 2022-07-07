using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject gameObjectGameModeView;

    [SerializeField]
    GameObject gameObjectGameHelpView;

    [SerializeField]
    GameObject gameObjectGameChangeSkinView;

    [SerializeField]
    TextMeshProUGUI textBestScore;

    private int bestScore = 0;
    private void Awake()
    {
        bestScore = SaveData.instance.bestScore;
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
        gameObjectToShow.SetActive(true);

        gameObjectToShow.transform.localScale = new Vector3(0, 0, 0);
        gameObjectToShow.transform.LeanScale(new Vector3(1f, 1f, 1f), .5f).setEaseOutQuart();
    }

    public void HiddeGameObject(GameObject gameObjectToHide)
    {
        gameObjectToHide.transform.LeanScale(new Vector3(0f, 0f, 0f), .5f).setEaseOutQuart();
        gameObjectToHide.SetActive(false);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
