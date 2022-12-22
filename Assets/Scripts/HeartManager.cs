using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeartManager : MonoBehaviour
{

    [SerializeField]
    private GameObject gameObjectHeartPrefab;

    [SerializeField]
    private List<GameObject> childHeartGameObject;

    [SerializeField]
    private List<GameObject> childHeartLeftGameObject;

    [SerializeField]
    private List<GameObject> childHeartRightGameObject;

    void Start()
    {
        GameManager.Instance.UnityEventWrongIcon.AddListener(DestroyHeart);
    }

    private void DestroyHeart()
    {
        var whoClickedWrongIcon = GameManager.Instance.LeftOrRightClickedWrongIcon;

        if (GameMode.Instance.GameModeType == GameModeType.OneVSOne)
        {
            if (whoClickedWrongIcon == "LeftCircle")
            {
                DestroyFirstGameObjectFromList(childHeartLeftGameObject);
            }
            else if (whoClickedWrongIcon == "RightCircle")
            {
                DestroyFirstGameObjectFromList(childHeartRightGameObject);
            }
        }
        else
        {
            DestroyFirstGameObjectFromList(childHeartGameObject);
            Instantiate(gameObjectHeartPrefab);

        }
    }

    private void DestroyFirstGameObjectFromList(List<GameObject> gameObjectList)
    {
        Destroy(gameObjectList.First());
        gameObjectList.RemoveAt(0);
    }


}