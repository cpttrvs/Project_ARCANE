using UnityEngine;
using System.Collections;

public class Repel : Cone {
    
    public override void effect()
    {
        Debug.Log(affected.name);
        //apply mark if it's not the caster
        if (affected.name != caster.name && affected.tag == "Player")
        {
            affected.transform.Translate(size * orientation);

            affected.GetComponent<Player>().addMark(mark);
            hasHit = true;
        }

    }
}
