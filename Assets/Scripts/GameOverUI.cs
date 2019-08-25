using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour 
{

	public string MenuToLoad = "MainMenu";
	public SceneFader sceneFader;

	public void Restart()
	{
		sceneFader.FadeTo (SceneManager.GetActiveScene ().name);
	}

	public void Menu()
	{
		sceneFader.FadeTo (MenuToLoad);
	}
}
