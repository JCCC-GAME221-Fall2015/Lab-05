using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

/// <summary>
/// Author: Matt Gipson
/// Contact: Deadwynn@gmail.com
/// Domain: www.livingvalkyrie.com
/// 
/// Description: PlayerNetworkSetup 
/// </summary>
public class PlayerNetworkSetup : NetworkBehaviour {
    #region Fields

    [SerializeField]
    Camera FPSCam;

    [SerializeField]
    AudioListener listener;

    #endregion

    void Start() {
        if (isLocalPlayer) {
            GameObject.Find("SceneCamera").SetActive(false);

            //player components
            GetComponent<CharacterController>().enabled = true;
            GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;

            //children components
            FPSCam.enabled = true;
            listener.enabled = true;

        }
    }
    
}