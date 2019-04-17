using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construction : MonoBehaviour
{
	//Prefab fields:
	public GameObject uniFloorTilePrefab;

	//This only works serverside:
	public void SpawnFloorTile(Vector3 pos, Transform parent) //TODO: Floor Tile Type!
	{
		var floorTile = PoolManager.PoolNetworkInstantiate(uniFloorTilePrefab, pos, parent);
		//TODO we need to get the tile that was removed from MetaData and add its name to the unifloortile script
	}

    public void TryWallConstruct(TileChangeManager tileChangeManager, TileType tileType, Vector3 cellPos, Vector3 worldPos)
    {
        var cellPosInt = Vector3Int.RoundToInt(cellPos);
        switch (tileType)
        {
            case TileType.Floor:
                DoWallConstruction(cellPosInt, tileChangeManager, worldPos);
                tileChangeManager.gameObject.GetComponent<SubsystemManager>().UpdateAt(cellPosInt);
                break;
        }
    }

    public void ProcessConstructRequest(GameObject player, GameObject matrixRoot, TileType tileType,
        Vector3 cellPos, Vector3 worldCellPos)
    {
        Debug.Log("ARAN: ProcessConstructRequest()");
        if (Vector3.Distance(player.transform.position, worldCellPos) > 1.5f)
        {
            return;
        }

        if (tileType == TileType.Floor)
        {
            var progressFinishAction = new FinishProgressAction(
                FinishProgressAction.Action.TileConstruction,
                matrixRoot.GetComponent<TileChangeManager>(),
                tileType,
                cellPos,
                worldCellPos,
                player
            );
            UIManager.ProgressBar.StartProgress(Vector3Int.RoundToInt(worldCellPos), 10f, progressFinishAction, player);
        }
    }

    private void DoWallConstruction(Vector3Int cellPos, TileChangeManager tcm, Vector3 worldPos)
    {
        tcm.UpdateTile(Vector3Int.RoundToInt(cellPos), TileType.Wall, "Wall");
    }
}