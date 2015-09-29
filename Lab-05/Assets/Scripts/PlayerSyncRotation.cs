using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerSyncRotation : NetworkBehaviour 
{
    [SyncVar]
    private Quaternion syncPlayerRotation;
    [SyncVar]
    private Quaternion syncCamRotation;

    [SerializeField]
    Transform playerTransform;
    [SerializeField]
    Transform camTransform;
    [SerializeField]
    float lerpRate = 15;
	
	// Update is called once per frame
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
        camTransform.rotation = Quaternion.Lerp(camTransform.rotation, syncCamRotation, Time.deltaTime * lerpRate);
        }
    }

    [Command]
    void CmdProvideRotationToServer(Quaternion playerRot, Quaternion camRot)
    {
        syncPlayerRotation = playerRot;
        syncCamRotation = camRot;
    }

    [Client]
    void TransmitRotations()
    {
        if (isLocalPlayer)
        {
            CmdProvideRotationToServer(playerTransform.rotation, camTransform.rotation);
        }
    }
}
