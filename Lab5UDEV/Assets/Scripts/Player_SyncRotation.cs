﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Player_SyncRotation : NetworkBehaviour {

	[SyncVar]
	private Quaternion syncPlayerRotation;
	[SyncVar]
	private Quaternion syncCamRotation;

	[SerializeField]
	private Transform playerTransform;
	[SerializeField]
	private Transform camTransform;
	[SerializeField]
	private float lerpRate = 15;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		TransmitRotations();
		LerpRotations();
	}

	void LerpRotations()
	{
		if (!isLocalPlayer)
		{
			playerTransform.rotation = Quaternion.Lerp(playerTransform.rotation, syncPlayerRotation, Time.deltaTime * lerpRate);
			playerTransform.rotation = Quaternion.Lerp(camTransform.rotation, syncCamRotation, Time.deltaTime * lerpRate);

		}

	}


	[Command]
	void CmdProvideRotationsToServer(Quaternion playerRot, Quaternion camRot)
	{
		syncPlayerRotation = playerRot;
		syncCamRotation = camRot;
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
