using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private string _name;

    [SerializeField]
    private int _damage;

    [SerializeField]
    private int[] _targets;

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
