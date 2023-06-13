using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private string _name;
    public string Name
    {
        get
        {
            return this.Name;
        }
        set
        {
            this.Name = value;
        }
    }

    [SerializeField]
    private int _damage;
    public int Damage
    {
        get
        {
            return this.Damage;
        }
        set
        {
            this.Damage = value;
        }
    }

    [SerializeField]
    private int[] _targets;
    public int[] Targets
    {
        get
        {
            return this._targets;
        }
        set
        {
            this._targets = value;
        }
    }

    [SerializeField]
    private int _cooldown;

    [SerializeField]
    private int _cost;

    #endregion Fields

    #region Public methods

    public Spell(string name, int damage, int[] targets, int cd, int cost)
    {
        _name = name;
        _damage = damage;
        _targets = targets;
        _cooldown = cd;
        _cost = cost;
    }

    public Spell(string name)
    {
        _name = name;
        _damage = 0;
        _targets = new int[] {0, 2, 3};
        _cooldown = 0;
        _cost = 0;
    }

    #endregion Public methods
}
