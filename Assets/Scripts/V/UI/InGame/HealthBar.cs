using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class HealthBar : UIBehaviour {

    int healthP1Max;
    int healthP2Max;
    Transform hBar1;
    Transform hBar2;

    // Use this for initialization
    protected override void initComponent(){
        
        healthP1Max = player1.getHealth();
        healthP2Max = player2.getHealth();

        hBar1 = header.Find("hBar1");
        foreach (Transform t in hBar1)
            t.gameObject.SetActive(true);

        hBar2 = header.Find("hBar2");
        foreach (Transform t in hBar2)
            t.gameObject.SetActive(true);
    }

    // Update is called once per frame
    protected override void refreshComponent()
    { 
        int healthP1 = player1.getHealth();
        if (healthP1 < healthP1Max)
            hBar1.GetChild(healthP1).gameObject.SetActive(false);

        int healthP2 = player2.getHealth();
        if (healthP2 < healthP2Max)
            hBar2.GetChild(healthP2).gameObject.SetActive(false);
    }
}
