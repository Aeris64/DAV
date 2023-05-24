using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    #region Fields

    [SerializeField]
    protected List<Character> _team;
    public List<Character> Team
    {
        get
        {
            return this._team;
        }
        set
        {
            this._team = value;
        }
    }

    #endregion Fields

    #region Public methods
    public void attack()
    {}

    public string isInTeam(string character) { return ""; }

    #endregion Public methods

    #region Private methods

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    #endregion Private methods
}