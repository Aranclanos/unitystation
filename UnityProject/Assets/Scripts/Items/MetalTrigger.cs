using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class MetalTrigger : PickUpTrigger
{


    public override void UI_Interact(GameObject originator, string hand)
    {
        base.UI_Interact(originator, hand);

        if (!isServer)
        {
            UIInteractMessage.Send(gameObject, UIManager.Hands.CurrentSlot.eventName);
        }
        else
        {
            //Vector3Int cellPos = metaTileMap.WorldToCell(Vector3Int.RoundToInt(transform.position));
			Debug.Log("ARAN: UI_Interact()");
            Vector3Int cellPos = Vector3Int.RoundToInt(transform.position);
            RequestTileConstructMessage.Send(originator, gameObject, TileType.Wall, cellPos, transform.position);

            //startBuilding(originator);
        }
    }
/*
    public void startBuilding(){
        var progressFinishAction = new FinishProgressAction(
            FinishProgressAction.Action.CleanTile,
            targetWorldPos,
            this
        );

        //Start the progress bar:
        UIManager.ProgressBar.StartProgress(Vector3Int.RoundToInt(targetWorldPos), 5f, progressFinishAction, originator);
    }
*/
}
