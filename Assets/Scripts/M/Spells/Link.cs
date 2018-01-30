using UnityEngine;
using System.Collections;
using System;

public class Link : Rune
{

    public Spell activation;
    private GameObject castedSpell;

    private Vector2 desiredOrientation;
    private float desiredDistance;
    private float pX;
    private float pY;
    private float rX;
    private float rY;

    private bool hasBeenActivated = false;
    private float timeLeft;

    public override void activate()
    {
        refreshState();
        castedSpell = Instantiate(activation.gameObject,
            new Vector3(transform.position.x, transform.position.y, transform.position.z),
            transform.rotation) as GameObject;
        castedSpell.GetComponent<Spell>().setCaster(caster);
        castedSpell.GetComponent<Spell>().setCastingObject(gameObject);
        castedSpell.GetComponent<Cone>().setOrientation(desiredOrientation);
        castedSpell.GetComponent<Laser>().setDistance(desiredDistance);
     
        timeLeft = castedSpell.GetComponent<Spell>().duration;

        hasBeenActivated = true;
    }

    public override void movement()
    {
        if (hasBeenActivated)
        {
            if (timeLeft <= 0)
            {
                Destroy(castedSpell);
                Destroy(gameObject);
            }
            else
            {
                refreshState();
                castedSpell.GetComponent<Laser>().setOrientation(desiredOrientation);
                castedSpell.GetComponent<Laser>().setOffset(desiredOrientation * 0.1f);

                timeLeft -= Time.deltaTime;
            }
        }

    }

    private void refreshState()
    {
        //effect
        pX = caster.transform.position.x;
        pY = caster.transform.position.y;
        rX = transform.position.x;
        rY = transform.position.y;

        float alpha = (Mathf.Atan(
            Mathf.Abs(caster.transform.position.y - transform.position.y) /
            Mathf.Abs(caster.transform.position.x - transform.position.x)
            ) * (180 / Mathf.PI));


        float trueAlpha = alpha;
        if (pX <= rX && pY > rY)
            trueAlpha = 180 - alpha;
        if (pX < rX && pY <= rY)
            trueAlpha = -180 + alpha;
        if (pX >= rX && pY < rY)
            trueAlpha = -alpha;

        float desiredX = Mathf.Cos(Mathf.Deg2Rad * trueAlpha);
        float desiredY = Mathf.Sin(Mathf.Deg2Rad * trueAlpha);

        desiredOrientation = new Vector2(desiredX, desiredY);

        desiredDistance = (Mathf.Sqrt(Mathf.Pow((pX - rX), 2) + Mathf.Pow((pY - rY), 2)) * 10) / 5;
    }


}