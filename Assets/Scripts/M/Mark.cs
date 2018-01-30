using UnityEngine;
using System.Collections;

public class Mark {

    public Player caster;
    public string spellName;
    public float duration;
    public Sprite icon;

    public Mark(Player c, string name, float time, Sprite i)
    {
        caster = c;
        spellName = name;
        duration = time;
        icon = i;
    }

    public Player getCaster()
    { return caster; }
}
