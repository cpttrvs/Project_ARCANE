using UnityEngine;
using System.Collections;

public class Laser : Cone
{

    public float distance;

    public override void initPosition()
    {
        base.initPosition();
        transform.localScale = new Vector3(gameObject.transform.localScale.x * distance / 2, gameObject.transform.localScale.y, 0);
        transform.position = castingObject.transform.position + offset * size * distance * 2 * additionnalOffset;
    }

    public override void movement()
    {
        transform.position = castingObject.transform.position + offset * size * distance * 2 * additionnalOffset;
        float angle = Mathf.Atan2(-orientation.y, -orientation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }


    public void setDistance(float d)
    { distance = d; }

}