using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeartManager : MonoBehaviour
{

    [SerializeField]
    GameObject gameObjectHeartPrefab;

    [SerializeField]
    List<GameObject> childHeartGameObject;

    [SerializeField]
    List<GameObject> childHeartLeftGameObject;

    [SerializeField]
    List<GameObject> childHeartRightGameObject;

    void Start()
    {
        GameManager.instance.unityEventWrongIcon.AddListener(DestroyHeart);
    }

    private void DestroyHeart()
    {
        var whoClickedWrongIcon = GameManager.instance.leftOrRightClickedWrongIcon;

        if (GameMode.instance.GetGameModeType() == GameModeType.OneVSOne)
        {
            if (whoClickedWrongIcon == "LeftCircle")
            {
                Destroy(childHeartLeftGameObject.First());
                childHeartLeftGameObject.RemoveAt(0);

            }
            else if (whoClickedWrongIcon == "RightCircle")
            {
                Destroy(childHeartRightGameObject.First());
                childHeartRightGameObject.RemoveAt(0);
            }
        }
        else
        {

            Destroy(childHeartGameObject.First());
            childHeartGameObject.RemoveAt(0);

            Instantiate(gameObjectHeartPrefab);

        }
    }


}
