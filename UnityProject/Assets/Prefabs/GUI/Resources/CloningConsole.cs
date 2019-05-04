using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloningConsole : NetworkTabTrigger
{
	public delegate void ChangeEvent();
	public static event ChangeEvent changeEvent;

	public List<CloningRecord> CloningRecords = new List<CloningRecord>();

	public override bool Interact(GameObject originator, Vector3 position, string hand)
	{
		if (!CanUse(originator, hand, position, false))
		{
			return false;
		}
		if (!isServer)
		{
			//ask server to perform the interaction
			InteractMessage.Send(gameObject, position, hand);
			return true;
		}

		UpdateGUI();
		TabUpdateMessage.Send(originator, gameObject, NetTabType, TabAction.Open);

		return true;
	}

	public void Start(){
		//DEBUG
		CloningRecord CRone = new CloningRecord("Dana Ray", "7602", 50, 117, 0, 33, "630990308360514eb1815");
		CloningRecords.Add(CRone);
		CloningRecord CRtwo = new CloningRecord("Trinity Ray", "3465", 50, 117, 0, 33, "630991235560514Eb1815");
		CloningRecords.Add(CRtwo);
	}

	public void UpdateGUI()
	{
		// Change event runs updateAll in CloningGUI
		if (changeEvent != null)
		{
			changeEvent();
		}
	}
}

public class CloningRecord
{
	public string Name;
	public string ScanID;
	public float OxyDmg;
	public float BurnDmg;
	public float ToxingDmg;
	public float BruteDmg;
	public string UniqueIdentifier;

	public CloningRecord(string name, string scanID, float oxyDmg, float burnDmg, float toxingDmg, float bruteDmg, string uniqueIdentifier)
	{
		Name = name;
		ScanID = scanID;
		OxyDmg = oxyDmg;
		BurnDmg = burnDmg;
		ToxingDmg = toxingDmg;
		BruteDmg = bruteDmg;
		UniqueIdentifier = uniqueIdentifier;
	}
}

