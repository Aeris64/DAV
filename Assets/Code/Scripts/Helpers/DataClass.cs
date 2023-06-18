using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Primitive class for JSON utilities.
*/

[Serializable]
public class DataCharacter
{
    public string Name = "";

    public int LifePointMax = 0;

    public int ManaPointMax = 0;

    public int Armor = 0;

    public int Speed = 0;

    public int Position = 0;
    public Spell[] Spells = null;

    public Character ToOriginalClass()
    {
        return new Character(Name, LifePointMax, ManaPointMax, Armor, Speed, Position);
    }
}

[Serializable]
public class DataSpell
{
    public string Name = "";

    public int Damage = 0;

    public int[] Targets = null;

    private int Cooldown = 0;

    private int Cost = 0;

    public Spell ToOriginalClass()
    {
        return new Spell(Name, Damage, Targets, Cooldown, Cost);
    }
}
