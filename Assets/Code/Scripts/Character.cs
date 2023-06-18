using System;
using UnityEngine;

public class Character : MonoBehaviour
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
    private int _lifePoint;
    public int LifePoint
    {
        get
        {
            return this._lifePoint;
        }
        set
        {
            this._lifePoint = value;
        }
    }

    [SerializeField]
    private int _lifePointMax;
    public int LifePointMax
    {
        get
        {
            return this._lifePointMax;
        }
        set
        {
            this._lifePointMax = value;
        }
    }

    [SerializeField]
    private int _manaPoint;
    public int ManaPoint
    {
        get
        {
            return this._manaPoint;
        }
        set
        {
            this._manaPoint = value;
        }
    }

    [SerializeField]
    private int _manaPointMax;
    public int ManaPointMax
    {
        get
        {
            return this._manaPointMax;
        }
        set
        {
            this._manaPointMax = value;
        }
    }

    [SerializeField]
    private int _armor;
    public int Armor
    {
        get
        {
            return this._armor;
        }
        set
        {
            this._armor = value;
        }
    }

    [SerializeField]
    private int _speed;
    public int Speed
    {
        get
        {
            return this._speed;
        }
        set
        {
            this._speed = value;
        }
    }

    [SerializeField]
    private int _position;
    public int Position
    {
        get
        {
            return this._position;
        }
        set
        {
            this._position = value;
        }
    }

    [SerializeField]
    private Spell[] _spells;
    public Spell[] Spells
    {
        get
        {
            return this._spells;
        }
        set
        {
            this._spells = value;
        }
    }

    #endregion Fields

    #region Public methods

    public Character(string name, int lP, int mP, int armor, int speed, int pos)
    {
        _name = name;
        _lifePoint = lP;
        _lifePointMax = lP;
        _manaPoint = mP;
        _manaPointMax = mP;
        _armor = armor;
        _speed = speed;
        _position = pos;
        _spells = null;
    }

    public Character(string name)
    {
        _name = name;
        _lifePoint = 0;
        _lifePointMax = 0;
        _manaPoint = 0;
        _manaPointMax = 0;
        _armor = 0;
        _speed = 0;
        _position = 0;
    }

    public Spell Attack(int spellNumber)
    {
        if(_spells[spellNumber] == null) return null;
        Debug.Log($"{_name} attack with {_spells[spellNumber].Name} !");

        return _spells[spellNumber];
    }

    public void GetDamage(int damage)
    {
        if(IsDead()) return;

        _lifePoint -= damage;

        Debug.Log($"{_name} take {damage} damages !");
    }

    public bool IsDead()
    {
        Debug.Log($"{_name} have {_lifePoint} lifePoint(s) !");

        return _lifePoint <= 0;
    }

    #endregion Public methods
}
