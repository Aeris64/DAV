using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    [SerializeField]
    private Transform _container;
    public Transform Container
    {
        get
        {
            return this._container;
        }
        set
        {
            this._container = value;
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
        // No attack TODO
        if(_spells[spellNumber].Name == null) return null;

        Debug.Log($"{_name} attack with {_spells[spellNumber].Name} !");

        GetMana(_spells[spellNumber].Cost);

        return _spells[spellNumber];
    }

    public bool GetDamage(int damage)
    {
        if(IsDead()) return false;

        _lifePoint -= damage;

        Debug.Log($"{_name} take {damage} damage(s) !");

        UpdateContainerStat();

        return !IsDead();
    }

    public bool GetMana(int mana)
    {
        if(_manaPoint - mana < 0)
        {
            Debug.Log($"{_name} have {_manaPoint} mana(s) and can't pay {mana} !");
            return false;
        }

        _manaPoint -= mana;

        UpdateContainerStat();

        Debug.Log($"{_name} pay {mana} mana(s) !");

        return true;
    }

    public bool IsDead()
    {
        Debug.Log($"{_name} have {_lifePoint}/{_lifePointMax} lifePoint(s) !");

        return _lifePoint <= 0;
    }

    public bool UpdateContainerStat()
    {
        if(!_container) return false;

        var healthPointContainer = _container.Find("HealthPoint");
        if(healthPointContainer)
        {
            var healthPoint = healthPointContainer.GetComponent<TMP_Text>();
            if(healthPoint)
            {
                healthPoint.text = $"HP: {_lifePoint}";
            }
        }

        var manaPointContainer = _container.Find("ManaPoint");
        if(manaPointContainer)
        {
            var manaPoint = manaPointContainer.GetComponent<TMP_Text>();
            if(manaPoint)
            {
                manaPoint.text = $"MP: {_manaPoint}";
            }
        }

        if(IsDead())
        {
            var panelContainer = _container.Find("Panel");
            if(panelContainer)
            {
                var panelImage = panelContainer.GetComponent<Image>();
                if(panelImage)
                {
                    panelImage.color = new Color(0, 0, 0);
                }
            }
        }

        return true;
    }

    #endregion Public methods
}
