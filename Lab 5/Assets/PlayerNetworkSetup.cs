using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerNetworkSetup : NetworkBehaviour {

    [SerializeField]
    Camera FPSCam;
	// Use this for initialization
	void Start ()
    {
        if (isLocalPlayer)
        {
            GameObject.Find("Main Camera").SetActive(false);
            GetComponent<CharacterController>().enabled = true;
            GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
            FPSCam.enabled = true;
            FPSCam.GetComponent<AudioListener>().enabled = true;
        }
	}
	
}
