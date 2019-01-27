using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIBehaviourOnline : MonoBehaviour
{
    private bool isInstantiate = false;

    protected Player player1 = null;
    protected Player player2 = null;

    protected Transform header;
    GameOnline gi;


    // Use this for initialization
    void Start()
    {
        gi = GameObject.Find("GameInstance").GetComponent<GameOnline>();
        header = transform.Find("header");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInstantiate)
        {
            player1 = gi.getPlayer1().GetComponent<Player>();
            player2 = gi.getPlayer2().GetComponent<Player>();

            if(player1 != null && player2 != null)
            {
                initComponent();
                isInstantiate = true;
            }
        } else
        {
            refreshComponent();
        }

    }

    protected virtual void initComponent() { }
    protected virtual void refreshComponent() { }
}
