using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Networking;

public class PlayerOnline : NetworkBehaviour
{

    void Start()
    {
        GetComponent<Player>().enabled = isLocalPlayer;
    }
}
