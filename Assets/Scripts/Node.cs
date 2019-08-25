using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
	public Color hoverColor;
	public Color notEnoughMoneyColor;
	public Vector3 positionOffset;

	[HideInInspector]
	public GameObject turret;
	[HideInInspector]
	public TurretBlueprint turretBluePrint;
	[HideInInspector]
	public bool isUpgraded = false;

	private Renderer rend;
	private Color startColor;

	BuildManager buildManager;

	void Start()
	{
		rend = GetComponent<Renderer> ();
		startColor = rend.material.color;
		buildManager = BuildManager.instance;
	}

	public Vector3 getBuildPosition()
	{
		return transform.position + positionOffset;
	}

	void OnMouseDown()
	{
		if (EventSystem.current.IsPointerOverGameObject ())
			return;

		if (turret != null)
		{
			buildManager.SelectNode (this);
			return;
		}

		if (!buildManager.CanBuild)
			return;

		BuildTurret (buildManager.GetTurretToBuild());
	}

	void BuildTurret(TurretBlueprint blueprint)
	{
		if (PlayerStats.Money < blueprint.cost)
		{
			Debug.Log ("NOT ENOUGH MONEY");
			return;
		}

		PlayerStats.Money -= blueprint.cost;

		GameObject _turret = Instantiate (blueprint.prefab, getBuildPosition(), Quaternion.identity);
		turret = _turret;

		turretBluePrint = blueprint;

		GameObject effect = (GameObject)Instantiate (buildManager.buildTurretEffect, getBuildPosition (), Quaternion.identity);
		Destroy (effect, 1.0f);
	}

	public void UpgradeTurret()
	{
		if (PlayerStats.Money < turretBluePrint.upgradeCost)
		{
			Debug.Log ("NOT ENOUGH MONEY");
			return;
		}

		PlayerStats.Money -= turretBluePrint.upgradeCost;

		//Get rid of the old turret
		Destroy(turret);


		//Build upgraded turret
		GameObject _turret = Instantiate (turretBluePrint.upgradedPrefab, getBuildPosition(), Quaternion.identity);
		turret = _turret;

		GameObject effect = (GameObject)Instantiate (buildManager.buildTurretEffect, getBuildPosition (), Quaternion.identity);
		Destroy (effect, 1.0f);

		isUpgraded = true;
	}

	public void SellTurret()
	{
		PlayerStats.Money += turretBluePrint.GetSellAmount ();

		GameObject sellEffect = (GameObject)Instantiate (buildManager.sellTurretEffect, getBuildPosition (), Quaternion.identity);
		Destroy (sellEffect, 1.0f);

		Destroy (turret);
		turretBluePrint = null;

		isUpgraded = false;
	}

	void OnMouseEnter()
	{
		if (EventSystem.current.IsPointerOverGameObject ())
			return;

		if (!buildManager.CanBuild)
			return;

		if (buildManager.HasMoney)
		{
			rend.material.color = hoverColor;
		}
		else
		{
			rend.material.color = notEnoughMoneyColor;
		}

	}

	void OnMouseExit()
	{
		rend.material.color = startColor;
	}
}
