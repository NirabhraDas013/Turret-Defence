using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
	
	private Transform target;
	private EnemyController targetEnemy;

	[Header("Inside Requirements")]
	public Transform TurretPivot;
	public float turnspeed = 10.0f;
	public string enemyTag = "Enemy";
	public Transform FirePosition;

	[Header("general")]
	public float range = 15.0f;

	[Header("Bullets")]
	public float firerate = 1.0f;
	public GameObject bulletPrefab;
	private float fireCountdown =0.0f;

	[Header("Lasers")]
	public bool isUsingLaser = false;
	public LineRenderer linerenderer;
	public ParticleSystem impactEffect;
	public Light impactLight;
	public int damageOverTime = 30;
	public float slowPercentage = 0.5f;

	// Use this for initialization
	void Start ()
	{
		InvokeRepeating ("UpdateTarget", 0f, 0.5f);
	}

	void UpdateTarget()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag (enemyTag);
		float shortestdistance = Mathf.Infinity;
		GameObject nearestEnemy = null;

		foreach (GameObject enemy in enemies)
		{
			float distanceToEnemy = Vector3.Distance (transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestdistance)
			{
				shortestdistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null && shortestdistance <= range)
		{
			target = nearestEnemy.transform;
			targetEnemy = nearestEnemy.GetComponent<EnemyController> ();
		}
		else
		{
			target = null;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (target == null)
		{
			if (isUsingLaser)
			{
				if (linerenderer.enabled)
				{
					linerenderer.enabled = false;
					impactEffect.Stop ();
					impactLight.enabled = false;
				}
			}

			return;
		}

		LockOnTarget ();

		if (isUsingLaser)
		{
			Laser ();
		}
		else
		{
			if (fireCountdown <= 0)
			{
				Shoot ();
				fireCountdown = 1 / firerate;
			}

			fireCountdown -= Time.deltaTime;
		}
			
	}

	void LockOnTarget()
	{
		Vector3 dirToEnemy = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation (dirToEnemy);

		Vector3 rotation = Quaternion.Lerp (TurretPivot.rotation, lookRotation, Time.deltaTime * turnspeed).eulerAngles;
		TurretPivot.rotation = Quaternion.Euler (0.0f, rotation.y, 0.0f);
	}

	void Laser()
	{
		targetEnemy.takeDamage (damageOverTime * Time.deltaTime);
		targetEnemy.Slow (slowPercentage);

		if (!linerenderer.enabled)
		{
			linerenderer.enabled = true;
			impactEffect.Play();
			impactLight.enabled = true;
		}

		linerenderer.SetPosition (0, FirePosition.position);
		linerenderer.SetPosition (1, target.position);

		Vector3 dir = FirePosition.position - target.position;

		impactEffect.transform.position = target.position + dir.normalized;
		impactEffect.transform.rotation = Quaternion.LookRotation (dir);
	
	}

	void Shoot()
	{
		GameObject bulletGO = (GameObject)Instantiate (bulletPrefab, FirePosition.position, FirePosition.rotation);

		BulletController bullet = bulletGO.GetComponent<BulletController> ();

		if (bullet != null)
			bullet.Seek (target);
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, range);
	}

}
