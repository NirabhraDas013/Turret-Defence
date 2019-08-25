using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyController))]
public class EnemyMovement : MonoBehaviour
{

	private Transform target;
	private int waypointindex = 0;

	private EnemyController enemy;

	void Start()
	{
		enemy = GetComponent<EnemyController> ();

		target = Waypoints.waypoints [0];
	}

	void Update()
	{
		Vector3 dir = target.position - transform.position;
		transform.Translate (dir.normalized * enemy.speed * Time.deltaTime, Space.World);

		if (Vector3.Distance(transform.position, target.position) <= 0.4f)
		{
			GetNextWayPoint ();
		}

		enemy.speed = enemy.startSpeed;
	}

	void GetNextWayPoint()
	{
		if (waypointindex >= Waypoints.waypoints.Length - 1)
		{
			EndPath ();
			return;
		}

		waypointindex++;
		target = Waypoints.waypoints [waypointindex];
	}


	void EndPath()
	{
		PlayerStats.Lives--;
		WaveSpawner.enemiesAlive--;
		Destroy (gameObject);
	}
}
