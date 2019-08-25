using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class TurretBlueprint
{
	[Header("Basic")]
	public GameObject prefab;
	public int cost;

	[Header("Upgrade 1")]
	public GameObject upgradedPrefab;
	public int upgradeCost;

	public int GetSellAmount()
	{
		return cost / 2;
	}

	public int GetUpgradedSellAmount()
	{
		return (cost + (cost - upgradeCost)) / 2;
	}
}
