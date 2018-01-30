using UnityEngine;
using System.Collections;
using System;

public class Projectile : Spell
{
    protected Vector2 orientation;
    protected Vector3 offset;
    public float additionnalOffset = 1;

    protected float angle;
    public float speed;

    public override void endLife()
    {
        Destroy(gameObject);
    }

    public override void effect()
    {
        //apply mark
        if (affected.tag == "Player")
        {
            affected.GetComponent<Player>().addMark(mark);
            hasHit = true;
            endLife();
        }    
    }

    public override void initPosition()
    {
        if(castingObject != null)
            transform.position = castingObject.transform.position + offset * size * additionnalOffset;
        else
            transform.position = caster.transform.position + offset * size * additionnalOffset;

        angle = Mathf.Atan2(-orientation.y, -orientation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        initMovement();
    }

    public void setOrientation(Vector2 v)
    {
        this.orientation = v;
    }

    public Vector2 getOrientation()
    {
        return orientation;
    }

    public void setOffset(Vector2 v)
    {
        this.offset = new Vector3(v.x, v.y, 0);
    }

    public Vector2 getOffset()
    {
        return offset;
    }

    public virtual void initMovement()
    {
        rb2d.AddForce(this.orientation * this.speed);
    }
}
