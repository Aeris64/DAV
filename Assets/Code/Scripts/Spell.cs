using System;
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
            return this._name;
        }
        set
        {
            this._name = value;
        }
    }

    [SerializeField]
    private int _damage;
    public int Damage
    {
        get
        {
            return this._damage;
        }
        set
        {
            this._damage = value;
        }
    }

    [SerializeField]
    private int[] _targetAlly;
    public int[] TargetAlly
    {
        get
        {
            return this._targetAlly;
        }
        set
        {
            this._targetAlly = value;
        }
    }

    [SerializeField]
    private int[] _targetEnemy;
    public int[] TargetEnemy
    {
        get
        {
            return this._targetEnemy;
        }
        set
        {
            this._targetEnemy = value;
        }
    }

    [SerializeField]
    private int _cooldown;
    public int Cooldown
    {
        get
        {
            return this._cooldown;
        }
        set
        {
            this._cooldown = value;
        }
    }

    [SerializeField]
    private int _cost;
    public int Cost
    {
        get
        {
            return this._cost;
        }
        set
        {
            this._cost = value;
        }
    }

    #endregion Fields

    #region Public methods

    public Spell(string name, int damage, int[] targetAlly, int[] targetEnemy, int cd, int cost)
    {
        _name = name;
        _damage = damage;
        _targetAlly = targetAlly;
        _targetEnemy = targetEnemy;
        _cooldown = cd;
        _cost = cost;
    }

    public Spell(string name)
    {
        _name = name;
        _damage = 0;
        _targetAlly = new int[] {0, 1, 2, 3};
        _targetEnemy = new int[] {0, 1, 2, 3};
        _cooldown = 0;
        _cost = 0;
    }

    #endregion Public methods
}
