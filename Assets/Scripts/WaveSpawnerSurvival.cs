using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawnerSurvival : MonoBehaviour
{
	public static int enemiesAlive;

	public Wave[] waves;

	public GameObject[] enemyPrefabs;

	public Transform spawnPoint;
	public float countdownTimer = 5.5f;
	public float enemyDistanceTimer;
	public Text WavecountdownText;

	public GameManagement gameManager;

	private float countdown = 2.0f;
	private int waveIndex = 0;

	void Start()
	{
		enemiesAlive = 0;
	}

	void Update()
	{
		if(enemiesAlive > 0)
		{
			return;
		}

		if (countdown <= 0)
		{
			StartCoroutine (Spawnwave());
			countdown = countdownTimer;
			return;
		}

		countdown -= Time.deltaTime;
		countdown = Mathf.Clamp (countdown, 0.0f, Mathf.Infinity);

		WavecountdownText.text = string.Format("{0:00.00}", countdown);
	}

	IEnumerator Spawnwave()
	{

		PlayerStats.Rounds++;

		Wave wave = waves [waveIndex];

		enemiesAlive = wave.count;

		enemyDistanceTimer = 1f / wave.spawnRate;

		for (int i = 0; i < wave.count; i++)
		{
			SpawnEnemy (wave.enemy);
			yield return new WaitForSeconds (enemyDistanceTimer);
		}

		waveIndex++;

		if (waveIndex == waves.Length)
		{
			gameManager.WinLevel ();
			this.enabled = false;
		}
	}

	void SpawnEnemy(GameObject enemy)
	{
		int i;

		i = Random.Range (1, 3);

		Instantiate (enemyPrefabs[i], spawnPoint.position, spawnPoint.rotation);
	}
}
