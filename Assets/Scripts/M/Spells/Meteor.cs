using UnityEngine;
using System.Collections;

public class Meteor : Channel{

    public float distanceModificator;
    public float sizeModificator;
        
    public override void castingState()
    {
        gameObject.GetComponent<Animator>().Play("cast");

        transform.position += new Vector3(orientation.x * Mathf.Clamp(channelTime, 0, maxChannelingTime) * distanceModificator,
            orientation.y * Mathf.Clamp(channelTime, 0, maxChannelingTime) * distanceModificator);

        Vector2 scale;
        if (channelTime < 1)
            scale = new Vector2(size, size);
        else
            scale = new Vector2(size * Mathf.Pow(Mathf.Clamp(channelTime, 0, maxChannelingTime), 2) * sizeModificator,
                size * Mathf.Pow(Mathf.Clamp(channelTime, 0, maxChannelingTime), 2) * sizeModificator);

        transform.localScale = scale;
    }

}
