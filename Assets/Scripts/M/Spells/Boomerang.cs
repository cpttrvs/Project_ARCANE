using UnityEngine;
using System.Collections;

public class Boomerang : Projectile {

    Vector3 direction;

    public override void effect()
    {
        //apply mark
        if (affected.name != caster.name && affected.tag == "Player")
        {
            affected.GetComponent<Player>().addMark(base.mark);
            hasHit = true;
            endLife();
        } else if (affected.name == caster.name && durationLeft <= 0)
            Destroy(gameObject);
    }

    public override void endLife()
    {
        if (hasHit || (transform.position.x == caster.transform.position.x && transform.position.y == caster.transform.position.y))
            Destroy(gameObject); //collision
        
        setDirection(caster.transform.position - transform.position);

        //moveForward
        Vector3 pos = transform.position;

        Vector3 velocity = new Vector3(0, speed / 1000, 0);

        pos += transform.rotation * velocity;

        transform.position = pos;
    }

    public void setDirection(Vector3 dir)
    {
        //face
        dir.Normalize();

        float zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

        Quaternion desiredRot = Quaternion.Euler(0, 0, zAngle);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRot, 400);
    }
}
