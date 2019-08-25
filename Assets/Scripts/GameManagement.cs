using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
	public static bool isEndGame;
	public GameObject gameOverUI;
	public GameObject winLevelUI;

	void Start()
	{
		Time.timeScale = 1f;
		isEndGame = false;
	}

	// Update is called once per frame
	void Update ()
	{
		if (isEndGame)
			return;

		if(Input.GetKeyDown("e"))
		{
			EndGame ();
		}

		if (PlayerStats.Lives <= 0)
		{
			EndGame ();
		}
	}

	void EndGame ()
	{
		isEndGame = true;
		gameOverUI.SetActive (true);
	}

	public void WinLevel()
	{
		isEndGame = true;
		winLevelUI.SetActive (true);
	}
}
