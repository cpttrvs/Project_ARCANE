using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class ImmuneBar : UIBehaviour {

    Mark[] currentMarksP1;
    Mark[] currentMarksP2;
    List<Mark[]> immuneP1 = new List<Mark[]>();
    List<Mark[]> immuneP2 = new List<Mark[]>();

    Transform iBar1;
    Transform iBar2;

    // Use this for initialization
    protected override void initComponent()
    {
        //Mark[] m = new Mark[2] { null, null };
        //immuneP2.Add(m);
        iBar1 = header.Find("immune1");
        iBar2 = header.Find("immune2");
    }

    // Update is called once per frame
    protected override void refreshComponent()
    {
        currentMarksP1 = player1.getCurrentMarks();
        immuneP1 = player1.getImmunes();

        if (immuneP1.Count < 3) //temporary mark (first tile)
        {
            Image tile1 = iBar1.GetChild((immuneP1.Count) * 2).gameObject.GetComponent<Image>();
            if (currentMarksP1[0] != null)
                tile1.overrideSprite = currentMarksP1[0].icon;
            else
                tile1.overrideSprite = Resources.Load<Sprite>("Sprites/UI/tileCombo");
                
        }
        if (immuneP1.Count > 0) //second tile
        {
            Image tile2 = iBar1.GetChild((immuneP1.Count * 2 - 1)).gameObject.GetComponent<Image>();
            tile2.overrideSprite = immuneP1[immuneP1.Count - 1][1].icon;
        }

        currentMarksP2 = player2.getCurrentMarks();
        immuneP2 = player2.getImmunes();
        
        if(immuneP2.Count < 3) //temporary mark (first tile)
        {
            Image tile1 = iBar2.GetChild((immuneP2.Count) * 2).gameObject.GetComponent<Image>();
            if (currentMarksP2[0] != null)
                tile1.overrideSprite = currentMarksP2[0].icon;
            else
                tile1.overrideSprite = Resources.Load<Sprite>("Sprites/UI/tileCombo");
        }
        if (immuneP2.Count > 0) //second tile
        {
            Image tile2 = iBar2.GetChild((immuneP2.Count * 2 - 1)).gameObject.GetComponent<Image>();
            tile2.overrideSprite = immuneP2[immuneP2.Count - 1][1].icon;
        }
    }
}
