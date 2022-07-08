using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Main Objects")]
    [SerializeField]
    private GameObject[] leftCircle;

    [SerializeField]
    private GameObject[] rightCircle;

    private Animator leftCircleAnimator;
    private Animator rightCircleAnimator;

    [SerializeField]
    private short numberOfObjectToFind;

    public UnityEvent UnityEventWrongIcon;
    public UnityEvent UnityEventCorrectIcon;

    [Header("UI")]
    [SerializeField]
    private GameObject gameOverGameObjectUI;

    [Header("GAME MODES")]
    [SerializeField]
    private bool isGameOver = false;

    [SerializeField]
    private TextMeshProUGUI textScoreInGame;

    [SerializeField]
    private TextMeshProUGUI textScoreOnGameOver;

    [SerializeField]
    private int numberOfLifes;

    [SerializeField]
    private int score = 0;

    [Header("Unlimited Game Mode")]

    [Header("Limited Game Mode")]
    [SerializeField]
    private TextMeshProUGUI textTime;

    [SerializeField]
    private float timeleft = 61;

    [Header("One VS One Game Mode")]
    [SerializeField]
    private TextMeshProUGUI textScoreLeftPlayer;

    [SerializeField]
    private int scoreLeftPlayer;

    [SerializeField]
    private TextMeshProUGUI textScoreRightPlayer;

    [SerializeField]
    private int scoreRightPlayer;

    public string LeftOrRightPlayerScore = ""; // LeftCircle | RightCircle
    public string LeftOrRightClickedWrongIcon = ""; // LeftCircle | RightCircle

    [SerializeField]
    private int numberOfLifesLeftPlayer;

    [SerializeField]
    private int numberOfLifesRightPlayer;

    [SerializeField]
    private GameObject gameObjectBlockImage;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Application.targetFrameRate = 60;
            Input.multiTouchEnabled = false;
        }

    }

    private string iconPath = "";
    private List<Texture> allIcons;

    [SerializeField]
    private GameModeType gameModeType;
    void Start()
    {
        if (string.IsNullOrEmpty(SaveData.Instance.SkinType))
        {
            iconPath = "Icons/Devices";
        }
        else
        {
            iconPath = "Icons/" + SaveData.Instance.SkinType;
        }

        leftCircleAnimator = leftCircle[0].transform.parent.gameObject.GetComponent<Animator>();
        rightCircleAnimator = rightCircle[0].transform.parent.gameObject.GetComponent<Animator>();

        Draw();

        gameModeType = GameMode.Instance.GameModeType;
        switch (gameModeType)
        {
            case GameModeType.Practice:
                InitializeGameMode();
                break;
            case GameModeType.Normal:
                InitializeGameMode();
                break;
            case GameModeType.OneVSOne:
                InitializeOneVsOneGameMode();
                break;
            case GameModeType.Extreme:
                InitializeGameMode();
                SetBoolLeftAndRightAnimator("isRotate", leftCircleAnimator, rightCircleAnimator, true);
                break;
        }

        Draw();
    }

    public void Draw()
    {
        allIcons = Resources.LoadAll<Texture>(iconPath).ToList();
        numberOfObjectToFind = (short)Random.Range(0, 6);
        AsignImages(leftCircle);
        AsignImages(rightCircle);

        //The same object
        var currRandom = Random.Range(0, 6);

        leftCircle[numberOfObjectToFind].GetComponent<IconManager>().SetIsDoubleIcon(true);
        rightCircle[currRandom].GetComponent<IconManager>().SetIsDoubleIcon(true);
        rightCircle[currRandom].GetComponent<RawImage>().texture = leftCircle[numberOfObjectToFind].GetComponent<RawImage>().texture;
        rightCircle[currRandom].GetComponent<RawImage>().SetNativeSize();
    }

    private void AsignImages(GameObject[] circle)
    {
        int currRandom;

        for (int i = 0; i < circle.Length; i++)
        {
            currRandom = Random.Range(0, allIcons.Count - 1);
            circle[i].GetComponent<RawImage>().texture = allIcons[currRandom];

            var randomRotate = Random.Range(0, 180);
            circle[i].GetComponent<RectTransform>().Rotate(new Vector3(0, 0, randomRotate));
            allIcons.RemoveAt(currRandom);

            circle[i].GetComponent<RawImage>().SetNativeSize();
            circle[i].transform.localScale = new Vector3(1f, 1f, 1f);

            circle[i].GetComponent<IconManager>().SetIsDoubleIcon(false);
        }
    }

    private void InitializeGameMode(int score = 0, int numberOfLifes = 3, int timeleft = 61)
    {
        this.score = score;
        this.timeleft = timeleft;
        this.numberOfLifes = numberOfLifes;
        textScoreInGame.text = "" + 0;
    }

    [SerializeField]
    private GameObject gameObjectPlayerHealth;

    [SerializeField]
    private GameObject gameObjectOneVsOneTimerOn;

    private void InitializeOneVsOneGameMode()
    {
        SetPlayerHealth();
        SetPlayerTimer();

        scoreLeftPlayer = 0;
        scoreRightPlayer = 0;
        textScoreLeftPlayer.text = "" + 0;
        textScoreRightPlayer.text = "" + 0;
    }

    private void SetPlayerHealth()
    {
        if (GameMode.Instance.IsHealthOn)
        {
            gameObjectPlayerHealth.gameObject.SetActive(true);
            numberOfLifesLeftPlayer = 3;
            numberOfLifesRightPlayer = 3;
        }
        else
            gameObjectPlayerHealth.gameObject.SetActive(false);
    }

    private void SetPlayerTimer()
    {
        if (GameMode.Instance.IsTimerOn)
            gameObjectOneVsOneTimerOn.gameObject.SetActive(true);
        else
            gameObjectOneVsOneTimerOn.gameObject.SetActive(false);
    }


    private void SetBoolLeftAndRightAnimator(string name, Animator leftCircleAnimator, Animator rightCircleAnimator, bool boolValue)
    {
        leftCircleAnimator.SetBool(name, boolValue);
        rightCircleAnimator.SetBool(name, boolValue);
    }

    public IEnumerator IncreaseScore()
    {
        UnityEventCorrectIcon?.Invoke();
        if (gameModeType == GameModeType.OneVSOne)
        {
            if (LeftOrRightPlayerScore.Equals("LeftCircle"))
            {
                scoreLeftPlayer += 5;
                textScoreLeftPlayer.text = "" + scoreLeftPlayer;
            }
            else if (LeftOrRightPlayerScore.Equals("RightCircle"))
            {
                scoreRightPlayer += 5;
                textScoreRightPlayer.text = "" + scoreRightPlayer;
            }

            if (scoreLeftPlayer == GameMode.Instance.PointsToGet && scoreRightPlayer == GameMode.Instance.PointsToGet)
            {
                GameOver("DRAW!");
            }
            else if (scoreLeftPlayer == GameMode.Instance.PointsToGet)
            {
                GameOver("LEFT PLAYER WON");
            }
            else if (scoreRightPlayer == GameMode.Instance.PointsToGet)
            {
                GameOver("RIGHT PLAYER WON");
            }
            else
            {
                AudioManager.instance.PlayOnceClip(AudioClipType.correctIcon);
            }
        }
        else
        {
            AudioManager.instance.PlayOnceClip(AudioClipType.correctIcon);
            score += 5;
            textScoreInGame.text = "" + score;
        }

        if (gameModeType == GameModeType.Extreme)
        {
            SetBoolLeftAndRightAnimator("isRotate", leftCircleAnimator, rightCircleAnimator, false);
        }

        leftCircleAnimator.SetTrigger("correctIcon");
        rightCircleAnimator.SetTrigger("correctIcon");

        yield return new WaitForSeconds(0.25f);
        Draw();

        if (gameModeType == GameModeType.Extreme)
        {
            SetBoolLeftAndRightAnimator("isRotate", leftCircleAnimator, rightCircleAnimator, true);
        }

    }


    public void CorrectIcon(string leftOrRightPlayer = "")
    {
        LeftOrRightPlayerScore = leftOrRightPlayer;
        StartCoroutine(IncreaseScore());
    }

    public IEnumerator ShowWrongIconEffect()
    {
        if (gameModeType == GameModeType.Extreme)
        {
            SetBoolLeftAndRightAnimator("isRotate", leftCircleAnimator, rightCircleAnimator, false);
            yield return new WaitForSeconds(0.01f);
        }

        if (VibrationManager.Instance.IsVibration)
            Handheld.Vibrate();

        AudioManager.instance.PlayOnceClip(AudioClipType.wrongIcon);

        leftCircleAnimator.SetTrigger("wrongIcon");
        rightCircleAnimator.SetTrigger("wrongIcon");

        if (gameModeType == GameModeType.OneVSOne)
        {
            if (LeftOrRightClickedWrongIcon.Equals("LeftCircle"))
            {
                Instantiate(gameObjectBlockImage, leftCircle[0].transform.parent.gameObject.GetComponent<Transform>());
            }
            else if (LeftOrRightClickedWrongIcon.Equals("RightCircle"))
            {
                Instantiate(gameObjectBlockImage, rightCircle[0].transform.parent.gameObject.GetComponent<Transform>());
            }
        }

        yield return new WaitForSeconds(0.1f);

        if (gameModeType == GameModeType.Extreme)
        {
            SetBoolLeftAndRightAnimator("isRotate", leftCircleAnimator, rightCircleAnimator, true);
        }
    }

    public void WrongIcon(string leftOrRightPlayer = "")
    {
        LeftOrRightClickedWrongIcon = leftOrRightPlayer;
        if (LeftOrRightClickedWrongIcon == "LeftCircle")
        {
            numberOfLifesLeftPlayer--;

            if (numberOfLifesLeftPlayer == 0)
                GameOver("RIGHT PLAYER WON");
        }
        else if (LeftOrRightClickedWrongIcon == "RightCircle")
        {
            numberOfLifesRightPlayer--;

            if (numberOfLifesRightPlayer == 0)
                GameOver("LEFT PLAYER WON");

        }

        numberOfLifes--;

        StartCoroutine(ShowWrongIconEffect());
        UnityEventWrongIcon?.Invoke();


        if (IsGameOver())
        {
            GameOver();
        }
    }

    public void GameOverOneVsOneWithTimer()
    {
        WinnerOfOneVsOneGame();
    }

    private void WinnerOfOneVsOneGame()
    {
        if (scoreLeftPlayer == scoreRightPlayer)
        {
            GameOver("DRAW!");
        }
        else if (scoreLeftPlayer > scoreRightPlayer)
        {
            GameOver("LEFT PLAYER WON");
        }
        else
        {
            GameOver("RIGHT PLAYER WON");
        }
    }

    public void GameOver(string message = "")
    {
        if (isGameOver == false)
        {
            isGameOver = true;
            gameOverGameObjectUI.SetActive(true);

            SaveScore(message);

            if (GooglePlayServicesManager.Instance.IsConnectedToGooglePlayServices && gameModeType == GameModeType.OneVSOne)
            {
                Social.ReportProgress(GPGSIds.achievement_play_with_friends, 100.0f, null);
            }

            GooglePlayServicesManager.Instance.SendScoreToLeadership(score, gameModeType);
            GooglePlayServicesManager.Instance.SendAchivementProgress(score, gameModeType);

            AudioManager.instance.PlayOnceClip(AudioClipType.gameOver);
            AdsManager.ShowIntersitialAd();
        }
    }



    private void SaveScore(string message)
    {
        if ((SaveData.Instance.BestScore < score) && gameModeType == GameModeType.Normal)
        {
            textScoreOnGameOver.text = "NEW HIGHSCORE!\nSCORE: " + score;
            SaveData.Instance.BestScore = score;
            SaveData.Instance.Save();
        }
        else if (gameModeType == GameModeType.OneVSOne)
        {
            textScoreOnGameOver.text = message;
        }
        else
        {
            textScoreOnGameOver.text = "SCORE: " + score;
        }
    }

    private bool IsGameOver()
    {
        if (numberOfLifes == 0)
        {
            return true;
        }
        return false;
    }

    void FixedUpdate()
    {
        if ((gameModeType == GameModeType.Normal || gameModeType == GameModeType.Extreme) && (int)timeleft > 0 && IsGameOver() == false)
        {
            timeleft -= Time.deltaTime * 1;
            textTime.text = "" + (int)timeleft;
        }
        else if (gameModeType == GameModeType.Normal || gameModeType == GameModeType.Extreme)
        {
            GameOver();
        }
    }
}

