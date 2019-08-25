using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
	public TurretBlueprint standardTurret;
	public TurretBlueprint rocketTurret;
	public TurretBlueprint laserTurret;

	BuildManager buildManager;

	void Start()
	{
		buildManager = BuildManager.instance;
	}

	public void selectStandardTurret()
	{
		Debug.Log ("Standard Turret purchased");
		buildManager.selectTurretToBuild (standardTurret);
	}

	public void selectRocketTurret()
	{
		Debug.Log ("Rocket Turret Purchased");
		buildManager.selectTurretToBuild (rocketTurret);
	}

	public void selectLaserTurret()
	{
		Debug.Log ("Laser Turret Purchased");
		buildManager.selectTurretToBuild (laserTurret);
	}
}
