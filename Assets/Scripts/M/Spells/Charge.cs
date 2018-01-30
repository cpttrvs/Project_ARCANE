using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Charge : Blink {
    
    public GameObject trail;
    private List<float> trailInstances = new List<float>();

    public override void endLife()
    {
        caster.GetComponent<BoxCollider2D>().isTrigger = false;
        caster.GetComponent<Rigidbody2D>().AddForce(-orientation * distance);
        caster.GetComponent<Player>().moveEnabled = true;
        base.endLife();
    }

    public override void initPosition()
    {
        caster.GetComponent<BoxCollider2D>().isTrigger = true;
        caster.GetComponent<Player>().moveEnabled = false;
        caster.GetComponent<Rigidbody2D>().AddForce(orientation * distance);
        gameObject.GetComponent<Rigidbody2D>().AddForce(orientation * distance);
    }

    public override void movement()
    {
        
        float currentDuration = Mathf.Round(durationLeft * 13);
        if (!trailInstances.Contains(currentDuration))
        {
            trailInstances.Add(currentDuration);
            GameObject t;
            t = Instantiate(trail.gameObject,
                new Vector3(transform.position.x, transform.position.y, transform.position.z),
                transform.rotation) as GameObject;
            t.GetComponent<PBAoE>().setCaster(caster);
        }
        //Debug.Log(currentDuration);
    }
}
