using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerNetworkSetup : NetworkBehaviour 
{
    [SerializeField] 
    Camera FPSCharCam;
    [SerializeField]
    AudioListener listner;
	// Use this for initialization
	void Start () 
    {
        if (isLocalPlayer)
        {
            GameObject.Find("SceneCamera").SetActive(false);

            GetComponent<CharacterController>().enabled = true;
            GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
            FPSCharCam.enabled = true;
            listner.enabled = true;
        }
	}

}
