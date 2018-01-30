using UnityEngine;
using System.Collections;

public class Defrag : Projectile {

    public Spell activationPBAoE;
    public Spell activationProjectile;
    
    public override void endLife()
    {
        
        //explosion
        GameObject g = Instantiate(activationPBAoE.gameObject,
            new Vector2(transform.position.x, transform.position.y),
            activationPBAoE.transform.rotation) as GameObject;
        g.GetComponent<Spell>().setCaster(caster);

        //defrag
        GameObject a;
        for (int i = -1; i < 2; i++)
        {
            if(i !=0)
            {
                a = Instantiate(activationProjectile.gameObject,
                    new Vector2(transform.position.x, transform.position.y),
                    transform.rotation) as GameObject;

                a.GetComponent<Projectile>().setOrientation(new Vector2(i, -i));
                a.GetComponent<Projectile>().setOffset(new Vector2(0,0));
                a.GetComponent<Spell>().setCastingObject(gameObject);
                a.GetComponent<Spell>().setCaster(caster);

                a = Instantiate(activationProjectile.gameObject,
                    new Vector2(transform.position.x, transform.position.y),
                    transform.rotation) as GameObject;
                a.GetComponent<Projectile>().setOrientation(new Vector2(i, i));
                a.GetComponent<Projectile>().setOffset(new Vector2(0,0));
                a.GetComponent<Spell>().setCastingObject(gameObject);
                a.GetComponent<Spell>().setCaster(caster);
            }
        }
        
        Destroy(gameObject);
    }
}
