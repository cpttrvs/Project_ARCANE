using UnityEngine;
using System.Collections;

public class Channel : Spell {

    public float castDuration;
    public float recastCooldown;
    public float maxChannelingTime;
    protected float channelTime;
    protected bool isChanneling = true;
    
    protected Vector2 orientation;

    private bool hasBeenCast = false;

    public override void initPosition()
    {
        mark = new Mark(caster.GetComponent<Player>(), spellName, recastCooldown, icon);
    }
    public override void effect()
    {
        if(!isChanneling)
        {
            //apply mark if it's not the caster
            if (affected.name != caster.name && affected.tag == "Player")
            {
                affected.GetComponent<Player>().addMark(mark);
                hasHit = true;
            }
        }
    }

    //basic behaviour at endlife()

    public override void endLife()
    {
        if(isChanneling)
        {
            channelingState();
        } else
        {
            if (hasBeenCast)
            {
                Debug.Log("cac");
                Destroy(gameObject);
                
            } else
            {
                hasBeenCast = true;
                castingState();
                endCast();
            }
        }
    }
    
    public virtual void channelingState()
    {
        transform.position = caster.transform.position;
       
        caster.GetComponent<Player>().setSpeedModifcator(1 - (Mathf.Clamp(channelTime, 0, maxChannelingTime) / maxChannelingTime));
        
        if(channelTime >= (maxChannelingTime + 1)) //cancel channel
        {
            endCast();
            Destroy(gameObject);
        }
    }

    public virtual void castingState()
    { }

    private void endCast()
    {
        durationLeft = castDuration;
        caster.GetComponent<Player>().setSpeedModifcator(1);
        caster.GetComponent<Player>().setSpellCooldown(gameObject.GetComponent<Spell>(), recastCooldown);
    }


    
    public void setChannelTime(float time)
    {
        channelTime = time;
        Debug.Log(channelTime);
    }


    public void setOrientation(Vector2 v)
    {
        orientation = v;
    }

    public Vector2 getOrientation()
    {
        return orientation;
    }

    public void setChannelState(bool channel)
    { isChanneling = channel; }

    public void setCastState (bool state)
    { hasBeenCast = state; }
}
