using UnityEngine;
using System.Collections;

public class Invisibility : Spell {

    public override void initPosition()
    {
        //caster.GetComponent<SpriteRenderer>().enabled = false;
    }

    public override void movement()
    {
        caster.GetComponent<SpriteRenderer>().enabled = false;
        transform.position = caster.transform.position;
    }

    public override void endLife()
    {
        caster.GetComponent<SpriteRenderer>().enabled = true;
        Destroy(gameObject);
    }

    public override void effect()
    {
        //apply mark if it's not the caster
        if (affected.name != caster.name && affected.tag == "Player")
        {
            affected.GetComponent<Player>().addMark(mark);
            hasHit = true;
        }

    }
}
