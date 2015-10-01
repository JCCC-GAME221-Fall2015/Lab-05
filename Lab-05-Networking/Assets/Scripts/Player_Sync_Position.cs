using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player_Sync_Position : NetworkBehaviour {

	[SyncVar]
	private Vector3 syncPos;

	[SerializeField] Transform myTransform;
	[SerializeField] float LerpRate = 15;
	

	// Update is called once per frame
	void FixedUpdate () 
	{
		TransmitPosition ();
		LerpPosition ();
	}

	void LerpPosition()
	{
		if (!isLocalPlayer) 
		{
			myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime * LerpRate);
		}
	}

	[Command]
	void CmdProvidePositionToServer(Vector3 pos)
	{
		syncPos = pos;
	}

	[ClientCallback]
	void TransmitPosition()
	{
		if (isLocalPlayer) 
		{
			CmdProvidePositionToServer (myTransform.position);
		}
	}
}
