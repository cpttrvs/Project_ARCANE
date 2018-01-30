using UnityEngine;
using System.Collections;

public class GuidedProjectile : Projectile {

    public override void initPosition()
    {
        offset = offset * 1.35f;
        base.initPosition();
    }

    public override void movement()
    {
        float[] facing = caster.GetComponent<Player>().getFacing();
        
        if (!(facing[0] == 0 && facing[1] == 0))
        {
            if ((facing[0] != orientation.x) || (facing[1] != orientation.y))
            {
                rb2d.AddForce(-orientation * speed);
                orientation = new Vector2(facing[0], facing[1]);
                rb2d.AddForce(orientation * speed);
                float angle = Mathf.Atan2(-orientation.y, -orientation.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }
}
