/**
 * @author Darrick Hilburn
 * 
 * This script synchronizes player position data on the network.
 */

using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerSyncPosition : NetworkBehaviour 
{
    // Reference to this player's transform
    [SerializeField] Transform myTransform;
    // Speed to interpolate position at
    [SerializeField] float lerpRate = 15;
    // Position according to the server
    [SyncVar] private Vector3 syncPos;

    void FixedUpdate()
    {
        TransmitPosition();
        LerpPosition();
    }

    void LerpPosition()
    {
        if (!isLocalPlayer)
            myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime * lerpRate);
    }

    /**
     * CmdProvidePositionToServer tells the server where this player is in the network
     * 
     * @var pos: Player position in the server game scene
     */
    [Command]
    void CmdProvidePositionToServer(Vector3 pos)
    {
        syncPos = pos;
    }

    /**
     * TransmitPosition is a client call to the server
     */
    [ClientCallback]
    void TransmitPosition()
    {
        if(isLocalPlayer)
            CmdProvidePositionToServer(myTransform.position);
    }
}
