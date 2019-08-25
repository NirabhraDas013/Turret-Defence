using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
	[HideInInspector]
	public float speed;

	public float startSpeed = 10f;

	public float startHealth = 100;
	private float health;
	public int deathReward = 5;
	public GameObject deathEffect;

	[Header("UnityStuff")]
	public Image healthBar;

	private bool isDead = false;

	void Start()
	{
		speed = startSpeed;
		health = startHealth;
	}

	public void takeDamage(float dmgAmount)
	{
		health -= dmgAmount;

		healthBar.fillAmount = health / startHealth;

		if (health <= 0 && !isDead)
		{
			Die ();
		}
	}

	public void Slow(float slowPercentage)
	{
		speed = startSpeed * (1f - slowPercentage);
	}

	void Die()
	{
		isDead = true;

		Destroy (gameObject);
		GameObject deathEff = (GameObject)Instantiate (deathEffect, transform.position, Quaternion.identity);
		Destroy (deathEff, 2.0f);

		WaveSpawner.enemiesAlive--;

		PlayerStats.Money += deathReward;
	}


}
