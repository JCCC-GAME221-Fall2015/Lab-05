using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerSyncPosition : NetworkBehaviour 
{
    [SyncVar]
    private Vector3 syncPos;
    [SerializeField]
    Transform myTransform;
    [SerializeField]
    float lerpRate = 15;

	// Update is called once per frame
	void FixedUpdate () 
    {
        TransmitPosition();
        LerpPosition();
	}

    void LerpPosition()
    {
        if (!isLocalPlayer)
        {
            myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime * lerpRate);
        }
    }

    [Command]
    void CmdSyncPositionToServer(Vector3 pos)
    {
        syncPos = pos;
    }

    [ClientCallback]
    void TransmitPosition()
    {
        if(isLocalPlayer)
        {
        CmdSyncPositionToServer(myTransform.position);
        }
    }
}
