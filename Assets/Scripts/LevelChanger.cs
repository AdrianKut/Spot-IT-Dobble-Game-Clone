using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
	private Animator animator;
	private int levelToLoad;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	public void FadeToNextLevel(int levelToLoad)
	{
		FadeToLevel(levelToLoad);
	}

	public void FadeToLevel(int levelIndex)
	{
		levelToLoad = levelIndex;
		animator.SetTrigger("FadeOut");
	}

	public void OnFadeComplete()
	{
		StartCoroutine(LoadYourAsyncScene());
	}

	IEnumerator LoadYourAsyncScene()
	{
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelToLoad);

		while (!asyncLoad.isDone)
		{
			yield return null;
		}
	}
}
