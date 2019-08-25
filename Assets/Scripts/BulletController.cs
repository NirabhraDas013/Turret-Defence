using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	private Transform target;

	public float speed = 70.0f;
	public GameObject ImpactEffect;

	public float explosionRadius = 0f;
	public int DamageAmount = 20;
	public void Seek(Transform _target)
	{
		target = _target;
	}
	
	
	// Update is called once per frame
	void Update ()
	{
		if (target == null)
		{
			Destroy (gameObject);
			return;
		}

		Vector3 dirToTarget = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;

		if (dirToTarget.magnitude <= distanceThisFrame)
		{
			HitTarget ();
			return;
		}

		transform.Translate (dirToTarget.normalized * distanceThisFrame, Space.World);
		transform.LookAt(target);
	}

	void HitTarget()
	{
		GameObject ImpactEffectIns = (GameObject)Instantiate (ImpactEffect, transform.position, transform.rotation);
		Destroy (ImpactEffectIns, 5.0f);

		if (explosionRadius >= 0f)
		{
			Explode ();
		}
		else
		{
			Damage (target);
		}

		Destroy (gameObject);
	}

	void Damage(Transform enemy)
	{
		EnemyController enemyScript = enemy.GetComponent<EnemyController> ();

		if(enemyScript != null)
		{
			enemyScript.takeDamage (DamageAmount);
		}
	}

	void Explode()
	{
		Collider[] colliders = Physics.OverlapSphere (transform.position, explosionRadius);
		foreach (Collider collider in colliders)
		{
			if (collider.tag == "Enemy")
			{
				Damage (collider.transform);
			}
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, explosionRadius);
	}
}
