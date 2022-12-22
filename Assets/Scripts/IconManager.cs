using UnityEngine;
using UnityEngine.UI;

public class IconManager : MonoBehaviour
{
    public bool IsDoubleIcon { get; private set; } = false;

    private Button thisButton;
    private void Awake()
    {
        thisButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        thisButton.onClick.AddListener(Click);
    }

    private void OnDisable()
    {
        thisButton.onClick.RemoveListener(Click);
    }

    public void Click()
    {
        if (IsDoubleIcon)
        {
            if (GameMode.Instance.GameModeType == GameModeType.OneVSOne)
            {
                var currentCircle = this.gameObject.transform.parent.gameObject.tag;
                GameManager.Instance.CorrectIcon(currentCircle);
            }
            else
            {
                GameManager.Instance.CorrectIcon();
            }
        }
        else
        {
            if (GameMode.Instance.GameModeType == GameModeType.OneVSOne)
            {
                var currentCircle = this.gameObject.transform.parent.gameObject.tag;
                GameManager.Instance.WrongIcon(currentCircle);
            }
            else
            {
                GameManager.Instance.WrongIcon();
            }
        }
    }

    public void SetIsDoubleIcon(bool value)
    {
        IsDoubleIcon = value;
    }
}
