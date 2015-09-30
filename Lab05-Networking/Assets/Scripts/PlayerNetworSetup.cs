/**
 * @author Darrick Hilburn
 * 
 * This script initializes player components in the network.
 */
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerNetworSetup : NetworkBehaviour 
{
    [SerializeField] AudioListener audioListener;

	// Use this for initialization
	void Start () 
    {
        if (isLocalPlayer)
        {
            GetComponent<CharacterController>().enabled = true;
            GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
            GetComponentInChildren<Camera>().enabled = true;
            audioListener.enabled = true;
        }
	}
}
