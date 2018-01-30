using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour
{
    private bool isInstantiate = false;

    protected Player player1 = null;
    protected Player player2 = null;

    protected Transform header;
    GameInstance gi;


    // Use this for initialization
    void Start()
    {
        gi = GameObject.Find("GameInstance").GetComponent<GameInstance>();
        header = transform.Find("header");
    }

    // Update is called once per frame
    void Update()
    {
        if(!isInstantiate)
        {
            player1 = gi.getPlayer1().GetComponent<Player>();
            player2 = gi.getPlayer2().GetComponent<Player>();
            initComponent();

            isInstantiate = true;
        }

        refreshComponent();
    }

    protected virtual void initComponent() { }
    protected virtual void refreshComponent() { }
}
