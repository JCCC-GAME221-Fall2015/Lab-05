using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player_SyncRotation : NetworkBehaviour
{

    [SyncVar] private Quaternion syncPlayerRotation;
    [SyncVar] private Quaternion syncCamRotation;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform camTransform;
    [SerializeField] private float lerpRate = 15;
    
	
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
            playerTransform.rotation = Quaternion.Lerp(playerTransform.rotation, syncPlayerRotation,
                Time.deltaTime*lerpRate);
            camTransform.rotation = Quaternion.Lerp(camTransform.rotation, syncCamRotation, Time.deltaTime*lerpRate);
        }
    }

    [Command]
    void CmdProvideRotationsToServer(Quaternion playerRotation, Quaternion camRotation)
    {
        syncPlayerRotation = playerRotation;
        syncCamRotation = camRotation;
    }

    [Client]
    void TransmitRotations()
    {
        if (isLocalPlayer)
        {
            CmdProvideRotationsToServer(playerTransform.rotation, camTransform.rotation);
        }
    }
}
