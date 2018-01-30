using UnityEngine;
using System.Collections;
using System;

public class PBAoE : Spell
{
    public bool casterImmune;

    public override void endLife()
    {
        Destroy(gameObject);
    }

    public override void effect()
    {
        
        if(casterImmune)
        {
            //apply mark if it's not the caster
            if (affected.name != caster.name && affected.tag == "Player")
            {
                affected.GetComponent<Player>().addMark(mark);
                hasHit = true;
            }
        } else
        {
            //can touch the caster
            if (affected.tag == "Player")
            {
                affected.GetComponent<Player>().addMark(mark);
                hasHit = true;
            }
        }
        
            
    }

}
