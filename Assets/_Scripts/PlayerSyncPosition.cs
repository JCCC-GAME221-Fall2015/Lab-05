using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

/// <summary>
/// Author: Matt Gipson
/// Contact: Deadwynn@gmail.com
/// Domain: www.livingvalkyrie.com
/// 
/// Description: PlayerSyncPosition 
/// </summary>
public class PlayerSyncPosition : NetworkBehaviour {
    #region Fields

    [SyncVar]
    Vector3 syncPos;

    [SerializeField]
    Transform myTransform;

    [SerializeField]
    float lerpRate = 15;

    #endregion

    void FixedUpdate() {
        TransmitPosition();
        LerpPosition();
    }

    void LerpPosition() {
        if (!isLocalPlayer) {
            myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime * lerpRate);
        }
    }

    [Command]
    void CmdProvidePositionToServer(Vector3 pos) {
        syncPos = pos;
    }

    [ClientCallback]
    void TransmitPosition() {
        if (isLocalPlayer) {
            CmdProvidePositionToServer(myTransform.position);
        }
    }
}