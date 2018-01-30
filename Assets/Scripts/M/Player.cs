using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Player : MonoBehaviour {
    
    public int playerId;
    public string inputPrefix;

    
    private int health = 3;
    private string[] currentState;
    private Mark markOne = null;
    private Mark markTwo = null;
    private List<Mark[]> immune = new List<Mark[]>();
    private float invulnerabilityLeft = 0;
    private Transform immuneIcon1;
    private Transform immuneIcon2;

    private GameObject castedSpell = null;
    public Spell[] spells;
    private float[] spellCooldown;

    private Rune[] currentRunes;

    private bool[] isChanneling = new bool[3] { false, false, false };
    private bool[] isReleased = new bool[3] { false, false, false };
    private float channelingDuration = 0;

    private float[] direction = new float[2];
    private float[] facing = new float[2];
    private Vector2 orientation;

    private float speed = 0.03f;
    private float speedModificator = 1f;
    private Animator animator;

    public bool activate = true;
    public bool moveEnabled = true;
    public bool castEnabled = true;

    //Mark[] temp = new Mark[2] { new Mark("", 0), new Mark("", 0) };

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        currentState = new string[2] { null, null };
        currentRunes = new Rune[3] { null, null, null };
        spellCooldown = new float[3] { 0, 0, 0 };
        animator.SetFloat("x", 0);
        animator.SetFloat("y", -1);

        immuneIcon1 = GameObject.Find("Game").transform.Find("GameInstance").transform.Find("Player" + playerId).GetChild(0);
        immuneIcon2 = GameObject.Find("Game").transform.Find("GameInstance").transform.Find("Player" + playerId).GetChild(1);
        
    }

    // Update is called once per frame
    void Update () {
        if (activate)
        {
            if(moveEnabled)
                move();
            if(castEnabled)
                cast();
        }

        immuneIcon1.GetComponent<SpriteRenderer>().enabled = transform.GetComponent<SpriteRenderer>().enabled;
        immuneIcon2.GetComponent<SpriteRenderer>().enabled = transform.GetComponent<SpriteRenderer>().enabled;
       

        if (invulnerabilityLeft <= 0)
        {
            invulnerabilityLeft = 0;
            if (markOne != null)
                markOneTimer();
            if (markTwo != null)
                markTwoTimer();
        }
        else
        {
            invulnerabilityLeft -= Time.deltaTime;
            if ((Math.Round((Decimal)invulnerabilityLeft, 1, MidpointRounding.AwayFromZero) * 10) % 2 == 0)
                transform.GetComponent<SpriteRenderer>().enabled = true;
            else
                transform.GetComponent<SpriteRenderer>().enabled = false;

            //invisibility debug
            if(invulnerabilityLeft <= 0)
            {
                transform.GetComponent<SpriteRenderer>().enabled = true;
                immuneIcon1.GetComponent<SpriteRenderer>().sprite = null;
                immuneIcon2.GetComponent<SpriteRenderer>().sprite = null;
            } 
        }
    }

    void cast()
    {
        //spell1
        if (spellCooldown[0] <= 0)
            spellCooldown[0] = 0;
        else
            spellCooldown[0] -= Time.deltaTime;

        if (Input.GetAxisRaw(inputPrefix + "Fire1") == 1 && spellCooldown[0] <= 0)
        {
            //animation
            animator.Play("Casting");
            spellCooldown[0] = spells[0].cooldown;
            spellBehaviour(spells[0]);
        } else if (Input.GetAxisRaw(inputPrefix + "Fire1") == 0 && isChanneling[0]) //Channeling spells
        {
            isChanneling[0] = false; isReleased[0] = true;
            spellBehaviour(spells[0]);
        }

        //spell2
        if (spellCooldown[1] <= 0)
            spellCooldown[1] = 0;
        else
            spellCooldown[1] -= Time.deltaTime;

        if (Input.GetAxisRaw(inputPrefix + "Fire2") == 1 && spellCooldown[1] <= 0)
        {
            //animation
            animator.Play("Casting");
            spellCooldown[1] = spells[1].cooldown;
            spellBehaviour(spells[1]);
        } else if (Input.GetAxisRaw(inputPrefix + "Fire2") == 0 && isChanneling[1]) //Channeling spells
        {
            isChanneling[1] = false; isReleased[1] = true;
            spellBehaviour(spells[1]);
        }

        //spell3
        if (spellCooldown[2] <= 0)
            spellCooldown[2] = 0;
        else
            spellCooldown[2] -= Time.deltaTime;

        if (Input.GetButton(inputPrefix + "Fire3") && spellCooldown[2] <= 0)
        {
            //animation
            animator.Play("Casting");
            spellCooldown[2] = spells[2].cooldown;
            spellBehaviour(spells[2]);
        } else if (!Input.GetButton(inputPrefix + "Fire3") && isChanneling[2]) //Channeling spells
        {
            isChanneling[2] = false; isReleased[2] = true;
            spellBehaviour(spells[2]);
        }
    }

    void move()
    {
        direction = new float[2] { Input.GetAxisRaw(inputPrefix + "Horizontal"), Input.GetAxisRaw(inputPrefix + "Vertical") };
        if (direction[0] >= 0.5f)
            direction[0] = 1;
        else if (direction[0] <= -0.5f)
            direction[0] = -1;
        else
            direction[0] = 0;

        if (direction[1] >= 0.5f)
            direction[1] = 1;
        else if (direction[1] <= -0.5f)
            direction[1] = -1;
        else
            direction[1] = 0;

        facing = new float[2] { Input.GetAxisRaw(inputPrefix + "FacingHorizontal"), Input.GetAxisRaw(inputPrefix + "FacingVertical") };
        if (facing[0] >= 0.5f)
            facing[0] = 1;
        else if (facing[0] <= -0.5f)
            facing[0] = -1;
        else
            facing[0] = 0;

        if (facing[1] >= 0.5f)
            facing[1] = 1;
        else if (facing[1] <= -0.5f)
            facing[1] = -1;
        else
            facing[1] = 0;


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
            transform.position += new Vector3(direction[0], direction[1], 0).normalized * speed * speedModificator;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -6f, 6f),
                Mathf.Clamp(transform.position.y, -3.2f, 2.6f), 0); 
        }

        orientation = new Vector2(animator.GetFloat("x"), animator.GetFloat("y"));
        if ((orientation.x == 1 || orientation.x == (-1)) && (orientation.y == 1 || orientation.y == (-1)))
            orientation = new Vector2(orientation.x / Mathf.Sqrt(2), orientation.y / Mathf.Sqrt(2));
    }

    private void spellBehaviour(Spell spell)
    {
        Vector2 offset = new Vector2(0.4f * orientation.x, 0.4f * orientation.y);
        
        string spellType = spell.GetType().Name;
        string spellBaseType = spell.GetType().BaseType.Name;

        int numSpell = Array.IndexOf(spells, spell);

        if (spellBaseType == "Rune")
        {
            if (currentRunes[numSpell] == null) // place the rune
            {
                castedSpell = instantiateSpell(spell);
                currentRunes[numSpell] = castedSpell.GetComponent<Rune>();
            }
            else // already placed
            {
                resetRune(currentRunes[numSpell]);
            }
        } else if (spellBaseType == "Channel")
        {
            bool allowed = true;

            if (isReleased[numSpell] && allowed) //channel stopped -> cast
            {
                //Debug.Log("has been cast");
                castedSpell.GetComponent<Channel>().setOrientation(orientation);
                castedSpell.GetComponent<Channel>().setChannelState(false);
                
                channelingDuration = 0;
                isReleased[numSpell] = false;
            } else
            {
                if (!isChanneling[numSpell] && isChanneling.Count(x => x) > 0) // prevent
                {
                    //Debug.Log("already a spell channeling");
                    allowed = false;
                }
                else
                {
                    if (!isChanneling[numSpell] && allowed) //first instance of channel
                    {
                        //Debug.Log("first instance");
                        castedSpell = instantiateSpell(spell);
                    }

                    //channeling
                    //Debug.Log("channeling");
                    isChanneling[numSpell] = true;
                    channelingDuration += Time.deltaTime;

                    for (int i = 0; i < spellCooldown.Length; i++) //prevent other spells to be cast
                        if (spellCooldown[i] >= 0 && spellCooldown[i] <= 0.2f && spells[i] != spell)
                            spellCooldown[i] = 0.2f ;

                    castedSpell.GetComponent<Channel>().setChannelTime(channelingDuration);
                }
            }
        }
        else
        {
            castedSpell = instantiateSpell(spell);
            if (spellType == "Projectile" || spellBaseType == "Projectile")
            {
                castedSpell.GetComponent<Projectile>().setOrientation(orientation);
                castedSpell.GetComponent<Projectile>().setOffset(offset);
            }
            if (spellType == "Blink" || spellBaseType == "Blink")
            {
                castedSpell.GetComponent<Blink>().setOrientation(orientation);
            }
            if (spellType == "Cone" || spellBaseType == "Cone") // Laser
            {
                castedSpell.GetComponent<Spell>().setCastingObject(gameObject);
                castedSpell.GetComponent<Cone>().setOrientation(orientation);
                castedSpell.GetComponent<Cone>().setOffset(offset);
            }
        }
    }

    public void resetRune(Rune rune)
    {
        for(int i = 0; i < currentRunes.Length; i++)
        {
            if(currentRunes[i] == rune)
            {
                currentRunes[i].activate();
                setSpellCooldown(currentRunes[i], currentRunes[i].recastCooldown);
                currentRunes[i] = null;
            }
        }
    }

    private GameObject instantiateSpell(Spell s)
    {
        GameObject castedSpell = Instantiate(s.gameObject,
            new Vector3(transform.position.x, transform.position.y, s.transform.position.z),
            transform.rotation) as GameObject;
        castedSpell.GetComponent<Spell>().setCaster(this.gameObject);
        return castedSpell;
    }

    private void markOneTimer()
    {
        if(markOne.duration >= 0)
            markOne.duration -= Time.deltaTime;
        else
        {
            //switch the second mark with the first mark
            currentState[0] = null;
            markOne = null;

            if (markTwo != null)
            {
                markOne = markTwo;
                markTwo = null;
                currentState[0] = markOne.spellName;
                immuneIcon1.GetComponent<SpriteRenderer>().sprite = markOne.icon;
            } else
                immuneIcon1.GetComponent<SpriteRenderer>().sprite = null;
        }
    }

    private void markTwoTimer()
    {
        if (markTwo.duration >= 0)
            markTwo.duration -= Time.deltaTime;
        else
        {
            currentState[1] = null;
            markTwo = null;
            immuneIcon2.GetComponent<SpriteRenderer>().sprite = null;
        }  
    }

    public void addMark(Mark mark)
    {
        if(invulnerabilityLeft <= 0)
        {
            if (currentState[0] == null && currentState[1] != mark.spellName) //no mark yet (spell different)
            {
                //Debug.Log(gameObject.name + " : add to key " + "(" + mark.spellName + ")");
                markOne = mark;
                currentState[0] = markOne.spellName;

                immuneIcon1.GetComponent<SpriteRenderer>().sprite = markOne.icon;
            }
            else if (currentState[1] == null) 
            {
                if (currentState[0] != null && currentState[0] != mark.spellName) //already one mark (spell different)
                {
                    //Debug.Log(gameObject.name + " : add to value " + "(" + mark.spellName + ")");
                    markTwo = mark;
                    currentState[1] = markTwo.spellName;

                    immuneIcon2.GetComponent<SpriteRenderer>().sprite = markTwo.icon;
                } else if (currentState[0] != null && currentState[0] == mark.spellName) //already one mark (same spell)
                {
                    markOne = mark;
                    currentState[0] = markOne.spellName;

                    immuneIcon1.GetComponent<SpriteRenderer>().sprite = markOne.icon;
                }
            }

            if (currentState[0] != null && currentState[1] != null) //not immuned yet
            {
                bool contains = false;

                for (int i = 0; i < immune.Count; i++)
                {
                    if (immune[i][0].spellName == currentState[0] && immune[i][1].spellName == currentState[1])
                    {
                        contains = true;
                        immuneIcon1.GetComponent<SpriteRenderer>().sprite = null;
                        immuneIcon2.GetComponent<SpriteRenderer>().sprite = null;
                    }    
                }

                if (!contains)
                {
                    Debug.Log(gameObject.name + " : add to immune (" + currentState[0] + "|" + currentState[1] + ")");

                    immune.Add(new Mark[2] { markOne, markTwo });

                    //invulnerability
                    invulnerabilityLeft = 1f;

                    getDamage();
                }
                else
                {
                    //debuff
                    markOne.getCaster().addGlobalCooldown(2f);
                    Debug.Log(gameObject.name + " : already got this combo (" + currentState[0] + "|" + currentState[1] + ")");
                }

                currentState[0] = null; markOne = null;
                currentState[1] = null; markTwo = null;
            }
            else
            {
                //Debug.Log(gameObject.name + " : nothing happened (" + currentState[0] + "|" + currentState[1]+")");
            }
        }
    }

    private void getDamage()
    {
        health--;
        Debug.Log(gameObject.name + " health : " + health);
        if (health <= 0)
        {
            Debug.Log(gameObject.name + " died");
            animator.Play("die");
            activate = false;
        }
    }

    public int getHealth()
    { return health; }

    public Spell[] getSpells()
    { return spells; }

    public void setSpells(Spell spell1, Spell spell2, Spell spell3)
    {
        if(spell1 != null) spells[0] = spell1;
        if (spell2 != null) spells[1] = spell2;
        if (spell3 != null) spells[2] = spell3;
    }

    public float getSpellCooldown(int i)
    { return spellCooldown[i]; }

    public Mark[] getCurrentMarks()
    { return new Mark[2] { markOne, markTwo }; }

    public List<Mark[]> getImmunes()
    { return immune; }

    public float[] getCooldown()
    { return spellCooldown; }

    public float[] getDirection()
    { return direction; }

    public float[] getFacing()
    { return facing; }

    public float getSpeed()
    { return speed; }

    public void setSpeedModifcator(float s)
    { speedModificator = s; }

    public void setInvulnerability(float s)
    { invulnerabilityLeft = s; }

    public void addGlobalCooldown(float s)
    {
        for (int i = 0; i < spellCooldown.Length; i++)
        {
            spellCooldown[i] += s;
            Debug.Log(playerId + " " + spellCooldown[i]);
        }
    }

    public void setSpellCooldown(Spell s, float cd)
    {
        for(int i = 0; i < spellCooldown.Length; i++)
        {
            if (spells[i].spellName == s.spellName)
            {
                spellCooldown[i] = cd + 0.1f;
                isChanneling[i] = false; isReleased[i] = false; channelingDuration = 0;
            }
        }
    }
}
