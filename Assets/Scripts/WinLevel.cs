using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLevel : MonoBehaviour
{
	public string MenuToLoad = "MainMenu";

	public string nextLevel = "Level02";
	public int nextLevelIndex = 2;

	public SceneFader sceneFader;

	public void Continue()
	{
		PlayerPrefs.SetInt ("levelReached", nextLevelIndex);
		sceneFader.FadeTo (nextLevel);
	}

	public void Menu()
	{
		sceneFader.FadeTo (MenuToLoad);
	}
}
