using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fissure : Projectile {

    public Spell activation;

    private List<float> trailInstances = new List<float>();

    public override void movement()
    {
        float currentDuration = Mathf.Round(durationLeft * 10);
        if (!trailInstances.Contains(currentDuration))
        {
            trailInstances.Add(currentDuration);
            GameObject t;
            t = Instantiate(activation.gameObject,
                new Vector3(transform.position.x, transform.position.y, transform.position.z),
                transform.rotation) as GameObject;
            t.GetComponent<PBAoE>().setCaster(caster);
        }
        //Debug.Log(currentDuration);
    }

    public override void effect()
    {
        //apply mark
        if (affected.tag == "Player")
        {
            affected.GetComponent<Player>().addMark(mark);
            hasHit = true;
        } 
    }
}
