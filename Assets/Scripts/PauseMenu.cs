using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	public GameObject ui;
	public string MenuToLoad = "MainMenu";
	public SceneFader sceneFader;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
		{
			Toggle ();
		}
	}

	void Toggle()
	{
		ui.SetActive (!ui.activeSelf);

		if (ui.activeSelf)
		{
			Time.timeScale = 0f;
		}
		else
		{
			Time.timeScale = 1;
		}	
	}

	public void Resume()
	{
		ui.SetActive (false);

		Time.timeScale = 1;
	}

	public void Restart()
	{
		Toggle ();
		sceneFader.FadeTo (SceneManager.GetActiveScene ().name);
	}

	public void Menu()
	{
		Toggle ();
		sceneFader.FadeTo (MenuToLoad);
	}

	public void ToLevelSelect()
	{
		Toggle ();
		sceneFader.FadeTo ("LevelSelector");
	}
}
