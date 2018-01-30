using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Spell : MonoBehaviour {

    public string spellName;
    //public string codeName;
    public Sprite icon;

    public float duration;
    public float size;
    public float cooldown;

    protected Sprite sprite;
    protected Animator animator;
    protected Rigidbody2D rb2d;
    protected Collider2D c2d;

    protected Mark mark;
    protected GameObject affected;
    protected bool hasHit = false;

    protected float durationLeft;

    protected GameObject caster;
    public GameObject castingObject;

    //on cast
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>().sprite;
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        c2d = GetComponent<Collider2D>();

        durationLeft = duration;
        mark = new Mark(caster.GetComponent<Player>(), spellName, cooldown, icon);

        Vector3 scale = new Vector3(transform.localScale.x * size,transform.localScale.y * size, 0);
        transform.localScale = scale;


        initPosition();
    }

    //per frame
    void Update()
    {
        movement();
        if (durationLeft > 0)
            durationLeft -= Time.deltaTime;
        else
            endLife();
    }

    //apply mark
    void OnTriggerEnter2D(Collider2D collider)
    {
        affected = collider.gameObject;
        Debug.Log(spellName + " : " + collider.gameObject.name);
        if(!hasHit)
            effect();
    }

    public void setCaster(GameObject c)
    {
        if (c.tag == "Player")
            caster = c;
    }

    public GameObject getCaster()
    {
        return caster;
    }

    public void setCastingObject(GameObject c)
    {
        castingObject = c;
    }

    public GameObject getCastingObject()
    {
        return castingObject;
    }

    public void setAffected(GameObject a)
    { affected = a; }

    public virtual void endLife()
    { }

    public virtual void effect()
    { }

    public virtual void initPosition()
    { }

    public virtual void movement()
    { }

    public virtual string text()
    { return spellName; }
}
