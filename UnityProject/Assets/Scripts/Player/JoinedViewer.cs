using System.Collections;
using System.Collections.Generic;
using Facepunch.Steamworks;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// This is the Viewer object for a joined player.
/// Once they join they will have local ownership of this object until a job is determined
/// and then they are spawned as player entity
/// </summary>
public class JoinedViewer : NetworkBehaviour
{
	ulong steamId = 1;

	[SyncVar]
	bool isLoggedOff;
    public override void OnStartServer()
    {
        base.OnStartServer();
        //Add player to player list
        isLoggedOff = PlayerList.Instance.IsLoggedOff(steamId);
        Logger.Log($"HAMISH: OnServer() Is logged off = {isLoggedOff}");
        PlayerList.Instance.JoinCheck(this, new ConnectedPlayer
        {
	        Connection = connectionToClient,
	        GameObject = gameObject,
	        Job = JobType.NULL,
	        SteamId = steamId
        });
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        PlayerManager.SetViewerForControl(this);
        UIManager.ResetAllUI();
        //HAMISHTODO: Check if rejoining before calling this
        Logger.Log($"HAMISH: OnStartLocalPlayer() Is logged off = {isLoggedOff}");
        if (!isLoggedOff)
        {
	        UIManager.Display.DetermineGameMode();
        }
        UIManager.SetDeathVisibility(true);

        if (BuildPreferences.isSteamServer)
        {
            //Send request to be authenticated by the server
            StartCoroutine(WaitUntilServerInit());
        }
    }


    //Just ensures connected player record is set on the server first before Auth req is sent
    IEnumerator WaitUntilServerInit()
    {
        yield return YieldHelper.EndOfFrame;
        if (Client.Instance != null)
        {
            Logger.Log("Client Requesting Auth", Category.Steam);
            // Generate authentication Ticket
            var ticket = Client.Instance.Auth.GetAuthSessionTicket();
            var ticketBinary = ticket.Data;
            // Send Clientmessage to authenticate
            RequestAuthMessage.Send(Client.Instance.SteamId, ticketBinary);
        }
        else
        {
            Logger.Log("Client NOT requesting auth", Category.Steam);
        }
    }

    /// <summary>
    /// At the moment players can choose their jobs on round start:
    /// </summary>
    [Command]
    public void CmdRequestJob(JobType jobType)
    {
        SpawnHandler.RespawnPlayer(connectionToClient, playerControllerId,
            GameManager.Instance.GetRandomFreeOccupation(jobType));
    }
}