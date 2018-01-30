using UnityEngine;
using System.Collections;

public class Iceblock : Spell {

    public GameObject activation;

    public override void initPosition()
    {
        caster.GetComponent<Player>().activate = false;
        caster.GetComponent<Player>().setInvulnerability(duration);
    }

    public override void movement()
    {
        caster.GetComponent<SpriteRenderer>().enabled = false;
    }

    public override void endLife()
    {
        GameObject a;

        Vector2 offset = new Vector2(0, 0);
        for (int i = -1; i < 2; i++)
        {
            for(int j = -1; j < 2; j++)
            {
                if(!(i == 0 && j == 0))
                {
                    a = Instantiate(activation.gameObject,
                        new Vector3(transform.position.x, transform.position.y, transform.position.z),
                        transform.rotation) as GameObject;
                    a.GetComponent<Projectile>().setOrientation(new Vector2(i, j));

                    if (( i == (-1) || i == 1) && (j == (-1) || j == 1))
                        offset = new Vector2(0.25f * i, 0.25f * j);
                    else
                        offset = new Vector2(0.8f * i, 0.8f * j);

                    Debug.Log(offset.x + " " + offset.y);

                    a.GetComponent<Projectile>().setOffset(offset);
                    a.GetComponent<Spell>().setCaster(caster);
                }
            }
        }
        
        caster.GetComponent<Player>().activate = true;
        caster.GetComponent<SpriteRenderer>().enabled = true;
        Destroy(gameObject);
    }
}
