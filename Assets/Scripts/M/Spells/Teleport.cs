using UnityEngine;
using System.Collections;
using System;

public class Teleport : Rune {

    public Spell activation;

    public override void activate()
    {
        caster.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, caster.transform.position.z);
        //effect
        GameObject a;

        Vector2 offset = new Vector2(0, 0);
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (!(i == 0 && j == 0))
                {
                    a = Instantiate(activation.gameObject,
                        new Vector3(transform.position.x, transform.position.y, transform.position.z),
                        transform.rotation) as GameObject;
                    a.GetComponent<Projectile>().setOrientation(new Vector2(i, j));

                    if ((i == (-1) || i == 1) && (j == (-1) || j == 1))
                        offset = new Vector2(0.35f * i, 0.35f * j);
                    else
                        offset = new Vector2(0.8f * i, 0.8f * j);

                    Debug.Log(offset.x + " " + offset.y);

                    a.GetComponent<Projectile>().setOffset(offset);
                    a.GetComponent<Spell>().setCaster(caster);
                }
            }
        }

        Destroy(gameObject);
    }
}
