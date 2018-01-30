using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class SpellsBar : UIBehaviour
{
    Spell[] player1spells;
    Spell[] player2spells;
    float[] player1cooldowns;
    float[] player2cooldowns;

    Transform sBar1;
    Transform sBar2;

    bool isInitialised = false;
    Rect[] labelsP1 = new Rect[3];
    Rect[] labelsP2 = new Rect[3];

    // Use this for initialization
    protected override void initComponent()
    {
        player1spells = player1.getSpells();
        player2spells = player2.getSpells();
        
        
        sBar1 = header.Find("sBar1");
        for (int i = 0; i < player1spells.Length; i++)
            sBar1.GetChild(i).gameObject.GetComponent<Image>().overrideSprite = player1spells[i].icon;

        sBar2 = header.Find("sBar2");
        for (int i = 0; i < player2spells.Length; i++)
            sBar2.GetChild(i).gameObject.GetComponent<Image>().overrideSprite = player2spells[i].icon;
        


    }

    // Update is called once per frame
    protected override void refreshComponent()
    {
        player1cooldowns = player1.getCooldown();
        player2cooldowns = player2.getCooldown();
    }

    
    void OnGUI()
    {
        for (int i = 0; i < player1cooldowns.Length; i++)
        {
            Transform concernedSpell = sBar1.GetChild(player1cooldowns.Length - 1 - i);
            labelsP1[i] = new Rect(-concernedSpell.position.x * (Screen.width/12.5f), concernedSpell.position.y * (Screen.height/45),
                ((RectTransform)concernedSpell).rect.width, ((RectTransform)concernedSpell).rect.height);
        }
        for (int i = 0; i < player2cooldowns.Length; i++)
        {
            Transform concernedSpell = sBar2.GetChild(player2cooldowns.Length - 1 - i);
            labelsP2[i] = new Rect((-concernedSpell.position.x * (Screen.width / 12.5f) + Screen.width), concernedSpell.position.y * (Screen.height / 45),
                ((RectTransform)concernedSpell).rect.width, ((RectTransform)concernedSpell).rect.height);
        }

        for (int i = 0; i < player1cooldowns.Length; i++)
            if(player1cooldowns[i] > 0)
                GUI.Label(labelsP1[i], player1cooldowns[i].ToString("0.0"));

        for (int i = 0; i < player2cooldowns.Length; i++)
            if (player2cooldowns[i] > 0)
                GUI.Label(labelsP2[i], player2cooldowns[i].ToString("0.0"));
    }
    
}
