using UnityEngine;
using UnityEngine.UI;

public class IconManager : MonoBehaviour
{

    public bool isDoubleDevice = false;

    private Button thisButton;
    void Awake()
    {
        thisButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        thisButton.onClick.AddListener(Click);
    }

    public void Click()
    {
        if (isDoubleDevice)
        {
            if (GameMode.instance.GetGameModeType() == GameModeType.OneVSOne)
            {
                var currentCircle = this.gameObject.transform.parent.gameObject.tag;
                GameManager.instance.CorrectIcon(currentCircle);
            }
            else
            {
                GameManager.instance.CorrectIcon();
            }
        }
        else
        {
            if (GameMode.instance.GetGameModeType() == GameModeType.OneVSOne)
            {
                var currentCircle = this.gameObject.transform.parent.gameObject.tag;
                GameManager.instance.WrongIcon(currentCircle);
            }
            else
            {
                GameManager.instance.WrongIcon();
            }
        }
    }




    public void Reset()
    {
        isDoubleDevice = false;
    }

}
