using System;

using UnityEngine;

[Serializable]
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

    [SerializeField]
    private int _lifePointMax;

    [SerializeField]
    private int _manaPoint;

    [SerializeField]
    private int _manaPointMax;

    [SerializeField]
    private int _armor;

    [SerializeField]
    private int _speed;

    [SerializeField]
    private int _position;

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

    public void attack() {}

    public void getDamage(string spell) {}

    public bool isDead() { return true; }

    #endregion Public methods
}
