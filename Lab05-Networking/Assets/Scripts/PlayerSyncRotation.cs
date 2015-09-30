/**
 * @author Darrick Hilburn
 * 
 * This script synchronizes player position data on the network.
 */
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerSyncRotation : NetworkBehaviour 
{

    // References to the player's position and their camera position
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform cameraTransform;
    // Speed to interpolate rotations at
    [SerializeField] float lerpRate = 15;
    // Rotations according to the server
    [SyncVar] Quaternion syncPlayerRotation;
    [SyncVar] Quaternion syncCameraRotation;

	void FixedUpdate () 
    {
        TransmitRotations();
        LerpRotations();
	}

    void LerpRotations()
    {
        if (!isLocalPlayer)
        {
            playerTransform.rotation = Quaternion.Lerp(playerTransform.rotation, syncPlayerRotation, Time.deltaTime * lerpRate);
            cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, syncCameraRotation, Time.deltaTime * lerpRate);

        }
    }

    /**
     * CmdProvideRotationsToServer tells the server the player's rotation in the network
     * 
     * @var playerRot: Player's current rotation in the server game scene
     * @var cameraRot: Player's camera rotation in the server game scene
     */
    [Command]
    void CmdProvideRotationsToServer(Quaternion playerRot, Quaternion cameraRot)
    {
        syncPlayerRotation = playerRot;
        syncCameraRotation = cameraRot;
    }

    /**
     * TransmitRotations is a client call to the server
     */
    [Client]
    void TransmitRotations()
    {
        if (isLocalPlayer)
        {
            CmdProvideRotationsToServer(playerTransform.rotation, cameraTransform.rotation);
        }
    }
}
