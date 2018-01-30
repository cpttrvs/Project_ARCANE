using UnityEngine;
using System.Collections;

public class Lightning : Projectile {

    bool isChild = false; 

    public override void initPosition()
    {
        offset = offset * 1.2f;
        transform.position = caster.transform.position + offset * size;

        if(!isChild)
        {
            GameObject a1, a2;

            a1 = Instantiate(gameObject,
                new Vector2(transform.position.x, transform.position.y),
                transform.rotation) as GameObject;
            a2 = Instantiate(gameObject,
                new Vector2(transform.position.x, transform.position.y),
                transform.rotation) as GameObject;

            Vector2 orientation1 = new Vector2();
            Vector2 orientation2 = new Vector2();
            if (orientation.x == 0)
            {
                orientation1 = new Vector2(0.25f, 1 * orientation.y);
                orientation2 = new Vector2(-0.25f, 1 * orientation.y);
            } else if (orientation.y == 0)
            {
                orientation1 = new Vector2(1 * orientation.x, 0.33f);
                orientation2 = new Vector2(1 * orientation.x, -0.33f);
            } else
            {
                orientation1 = new Vector2(0.66f * orientation.x, orientation.y);
                orientation2 = new Vector2(orientation.x, 0.66f * orientation.y);
            }

            a1.GetComponent<Projectile>().setOffset(offset);
            a1.GetComponent<Spell>().setCaster(caster);
            a1.GetComponent<Lightning>().setIsChild(true);
            a1.GetComponent<Projectile>().setOrientation(orientation1);
            
            a2.GetComponent<Projectile>().setOffset(offset);
            a2.GetComponent<Spell>().setCaster(caster);
            a2.GetComponent<Lightning>().setIsChild(true);
            a2.GetComponent<Projectile>().setOrientation(orientation2);


        }

        float angle = Mathf.Atan2(-orientation.y, -orientation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        initMovement();
    }

    public void setIsChild(bool b)
    {
        isChild = b;
    }
}
