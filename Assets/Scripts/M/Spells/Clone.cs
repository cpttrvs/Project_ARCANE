using UnityEngine;
using System.Collections;
using System;

public class Clone : Rune {

    public Spell activation;
    private Mark cloneMark;
    private Mark spellMark;

    public override void initPosition()
    {
        gameObject.GetComponent<Animator>().runtimeAnimatorController =
            Resources.Load<RuntimeAnimatorController>("Animations/Player" + caster.GetComponent<Player>().playerId +
            "/Player" + caster.GetComponent<Player>().playerId);

        cloneMark = new Mark(caster.GetComponent<Player>(), spellName, recastCooldown/2, icon);
        spellMark = new Mark(caster.GetComponent<Player>(), spellName, recastCooldown, icon);
    }

    public override void activate()
    {
        //nova
        GameObject a = Instantiate(activation.gameObject,
            new Vector3(transform.position.x, transform.position.y, transform.position.z),
            transform.rotation) as GameObject;
        a.GetComponent<Spell>().setCaster(caster);

        Destroy(gameObject);
    }

    public override void effect()
    {
        if(affected.tag == "Spell")
        {
            if(affected.name != "invisibility" && affected.GetComponent<Spell>().getCaster() != caster)
            {
                affected.GetComponent<Spell>().getCaster().GetComponent<Player>().addMark(spellMark);
                hasHit = true;
                activate();
                caster.GetComponent<Player>().resetRune(gameObject.GetComponent<Rune>());
            }
        }

        //apply mark if it's not the caster
        if (affected.name != caster.name && affected.tag == "Player")
        {
            affected.GetComponent<Player>().addMark(cloneMark);
            hasHit = true;
            activate();
            caster.GetComponent<Player>().resetRune(gameObject.GetComponent<Rune>());
        }
    }

    public override void movement()
    {
        //inverted from the caster
        float[] direction = new float[2] { caster.GetComponent<Player>().getDirection()[0],
            -caster.GetComponent<Player>().getDirection()[1] };
        float[] facing = new float[2] { caster.GetComponent<Player>().getFacing()[0],
            -caster.GetComponent<Player>().getFacing()[1] };

        bool isWalking = (Mathf.Abs(direction[0]) + Mathf.Abs(direction[1])) > 0;
        bool isFacing = (Mathf.Abs(facing[0]) + Mathf.Abs(facing[1])) > 0;

        //Debug.Log("dH : " + direction[0] + " | dV : " + direction[1] + "\nfH : " + facing[0] + " fV : " + facing[1]);

        if (isFacing)
        {
            animator.SetFloat("x", facing[0]);
            animator.SetFloat("y", facing[1]);
        }
        else if (isWalking && !isFacing)
        {
            animator.SetFloat("x", direction[0]);
            animator.SetFloat("y", direction[1]);
        }

        animator.SetBool("isWalking", isWalking);
        if (isWalking)
        {
            transform.position += new Vector3(direction[0], direction[1], 0).normalized *
                caster.GetComponent<Player>().getSpeed();

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -6f, 6f),
                Mathf.Clamp(transform.position.y, -3.2f, 2.6f), 0);
        }
    }

    public override void endLife()
    {
        caster.GetComponent<Player>().resetRune(gameObject.GetComponent<Rune>());
        activate();
    }
}
