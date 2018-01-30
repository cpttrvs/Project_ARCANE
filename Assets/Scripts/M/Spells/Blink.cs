using UnityEngine;
using System.Collections;

public class Blink : Spell {

    protected Vector2 orientation;
    public float distance;

    public override void endLife()
    {
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

    public override void initPosition()
    {
        Vector2 translation = new Vector2(orientation.x * distance, orientation.y * distance);
        caster.transform.Translate(translation);
        gameObject.transform.Translate(translation);
    }

    public void setOrientation(Vector2 v)
    {
        this.orientation = v;
    }

}
