using UnityEngine;
using System.Collections;
using System;

public class Cone : Spell
{

    protected Vector2 orientation;
    protected Vector3 offset;

    public float additionnalOffset = 1;


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
        transform.position = castingObject.transform.position + offset * additionnalOffset * size;
        //orientation
        float angle = Mathf.Atan2(-orientation.y, -orientation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public override void movement()
    {
        transform.position = castingObject.transform.position + offset * additionnalOffset * size;
    }

    public void setOrientation(Vector2 v)
    {
        this.orientation = v;
    }

    public void setOffset(Vector2 v)
    {
        this.offset = new Vector3(v.x, v.y, 0);
    }

}