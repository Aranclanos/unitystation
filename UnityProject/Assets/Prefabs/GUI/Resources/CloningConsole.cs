using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloningConsole : NetworkTabTrigger
{
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

		TabUpdateMessage.Send(originator, gameObject, NetTabType, TabAction.Open);

		return true;
	}
}
