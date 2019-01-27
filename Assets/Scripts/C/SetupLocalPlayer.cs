using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetupLocalPlayer : NetworkBehaviour
{
    void Start()
    {
        if(isLocalPlayer)
        {
            GetComponent<Player>().enabled = true;
        }
    }
   
}
