using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player_Sync_Rotation : NetworkBehaviour {

	[SyncVar] private Quaternion syncPlayerRotation;
	[SyncVar] private Quaternion syncCameraRotation;

	[SerializeField] private Transform playerTransform;
	[SerializeField] private Transform camTransform;
	[SerializeField] float lerpRate = 15;
	
	// Update is called once per frame
	void FixedUpdate () {
		TransmitRotations ();
		LerpRotations ();
	}

	void LerpRotations()
	{
		if (!isLocalPlayer)
		{
			playerTransform.rotation = Quaternion.Lerp (playerTransform.rotation, syncPlayerRotation, Time.deltaTime * lerpRate);
			camTransform.rotation = Quaternion.Lerp (camTransform.rotation, syncCameraRotation, Time.deltaTime * lerpRate);
		}
	}

	[Command]
	void CmdProvideRotationsToServer(Quaternion playerRot, Quaternion camRot)
	{
		syncPlayerRotation = playerRot;
		syncCameraRotation = camRot;
	}

	[ClientCallback]
	void TransmitRotations()
	{
		if(isLocalPlayer)
		{
			CmdProvideRotationsToServer(playerTransform.rotation, camTransform.rotation);
		}
	}
}
