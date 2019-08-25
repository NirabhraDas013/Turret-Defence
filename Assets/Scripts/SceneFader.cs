using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
	public Image image;
	public AnimationCurve fadeCurve;

	void Start()
	{
		StartCoroutine (FadeIn ());
	}

	public void FadeTo(string scene)
	{
		StartCoroutine (FadeOut (scene));
	}

	IEnumerator FadeIn()
	{
		float fadeTime = 1f;

		while (fadeTime > 0f)
		{
			fadeTime -= Time.deltaTime;
			float alpha = fadeCurve.Evaluate (fadeTime);
			image.color = new Color (0f, 0f, 0f, alpha);
			yield return 0;
		}
	}

	IEnumerator FadeOut(string scene)
	{
		float fadeTime = 0f;

		while (fadeTime < 1f)
		{
			fadeTime += Time.deltaTime;
			float alpha = fadeCurve.Evaluate (fadeTime);
			image.color = new Color (0f, 0f, 0f, alpha);
			yield return 0;
		}

		SceneManager.LoadScene (scene);
	}
}
