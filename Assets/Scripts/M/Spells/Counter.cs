using UnityEngine;
using System.Collections;

public class Counter : PBAoE {

    private bool hasCounter = false;

    public override void effect()
    {
        
        if (affected.tag == "Spell")
        {
            Spell s = affected.transform.GetComponent<Spell>();
            Debug.Log("aaaa" + s.GetType().Name);
            if((s.GetType().Name == "Projectile" || s.GetType().BaseType.Name == "Projectile")
                && s.getCaster() != caster)
            {
                s.setCaster(caster);
                Debug.Log(s.getCaster().ToString());
                Projectile p = s.GetComponent<Projectile>();

                GameObject castedSpell = Instantiate(s.gameObject,
                    new Vector3(transform.position.x, transform.position.y, s.transform.position.z),
                    transform.rotation) as GameObject;
                castedSpell.GetComponent<Spell>().setCaster(caster);
                castedSpell.GetComponent<Projectile>().setOrientation(-p.getOrientation());
                castedSpell.GetComponent<Projectile>().setOffset(-p.getOffset());

                Destroy(affected);
            }
            
            
        }
        base.effect();
    }

    public override void movement()
    {
        transform.position = caster.transform.position;
    }
}
