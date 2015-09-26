using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

/// <summary>
/// Author: Matt Gipson
/// Contact: Deadwynn@gmail.com
/// Domain: www.livingvalkyrie.com
/// 
/// Description: PlayerSyncRotation 
/// </summary>
public class PlayerSyncRotation : NetworkBehaviour {
    #region Fields

    [SyncVar] Quaternion syncPlayerRotation;
    [SyncVar] Quaternion syncCamRotation;

    [SerializeField] Transform playerTransform;
    [SerializeField] Transform camTransform;
    [SerializeField] float lerpRate = 15;

    #endregion

    void FixedUpdate() {
        TransmitRotation();
        LerpRotations();
    }

    void LerpRotations() {
        if (!isLocalPlayer) {
            playerTransform.rotation = Quaternion.Lerp(playerTransform.rotation, syncPlayerRotation, Time.deltaTime * lerpRate);
            camTransform.rotation = Quaternion.Lerp(camTransform.rotation, syncCamRotation, Time.deltaTime * lerpRate);
        }
    }

    [Command]
    void CmdProvideRotationsToServer(Quaternion playerRot, Quaternion camRot) {
        syncPlayerRotation = playerRot;
        syncCamRotation = camRot;
    }

    [Client]
    void TransmitRotation() {
        if (isLocalPlayer) {
            CmdProvideRotationsToServer(playerTransform.rotation, camTransform.rotation);
        }
    }

}