using UnityEngine;
using System.Collections;

public class Grab : Projectile
{
    public float grabbingTime;

    private bool translationOver = false;

    public override void endLife()
    {
        if(hasHit)
            StartCoroutine(translationWait());
        else
            base.endLife();
    }

    public override void effect()
    {
        //grab affected
        if (affected.name != caster.name && affected.tag == "Player")
        {
            hasHit = true;

            affected.GetComponent<Player>().addMark(mark);
            affected.GetComponent<Rigidbody2D>().AddForce(-orientation * speed);
            affected.GetComponent<BoxCollider2D>().isTrigger = true;

            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            gameObject.GetComponent<SpriteRenderer>().sprite =
                Resources.Load<Sprite>("Sprites/Spells/grab2");
            endLife();
        }
    }

    IEnumerator translationWait()
    {
        yield return new WaitForSeconds((cooldown-0.7f) - durationLeft);
        affected.GetComponent<Rigidbody2D>().AddForce(orientation * speed); //cancel force
        affected.GetComponent<BoxCollider2D>().isTrigger = false;
        base.endLife();
    }

    public override void movement()
    {
        if (hasHit)
        {
            transform.GetComponent<BoxCollider2D>().enabled = false;
            transform.position = affected.transform.position;
        }
            
    }
}
