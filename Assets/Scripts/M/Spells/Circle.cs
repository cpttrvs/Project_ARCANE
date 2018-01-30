using UnityEngine;
using System.Collections;

public class Circle : Channel {

    public override void movement()
    {
        transform.position = caster.transform.position;
    }

    public override void channelingState()
    { base.channelingState(); }

    public override void castingState()
    {
        Vector2 scale;
        if (channelTime < 1)
            scale = new Vector2(size * transform.localScale.x, size * transform.localScale.y);
        else
            scale = new Vector2(size * channelTime, size * channelTime);

        transform.localScale = scale;
    }

}
