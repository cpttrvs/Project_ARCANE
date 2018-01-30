using UnityEngine;
using System.Collections;
using System;

public class Rune : Spell
{
    public float recastCooldown;

    public override void endLife()
    {
    }

    public override void effect()
    {
        //apply mark if it's not the caster
        if (affected.name != caster.name && affected.tag == "Player")
        {
            affected.GetComponent<Player>().addMark(mark);
            hasHit = true;
            Destroy(gameObject);
        }
            
    }

    public virtual void activate()
    { }
}
