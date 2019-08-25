using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
	public GameObject ui;
	private Node target;

	public Text upgradeCost;
	public Button upgradeButton;

	public Text SellAmount;


	public void SetTarget(Node _target)
	{
		target = _target;

		transform.position = target.getBuildPosition ();

		if (!target.isUpgraded)
		{
			upgradeCost.text = "$" + target.turretBluePrint.upgradeCost;
			SellAmount.text = "$" + target.turretBluePrint.GetSellAmount ();
			upgradeButton.interactable = true;
		}
		else
		{
			upgradeCost.text = "DONE";
			SellAmount.text = "$" + target.turretBluePrint.GetUpgradedSellAmount ();
			upgradeButton.interactable = false;
		}



		ui.SetActive (true);
	}

	public void Hide()
	{
		ui.SetActive (false);
	}

	public void Upgrade()
	{
		target.UpgradeTurret ();
		BuildManager.instance.DeselectNode ();
	}

	public void Sell()
	{
		target.SellTurret ();
		BuildManager.instance.DeselectNode ();
	}
}
