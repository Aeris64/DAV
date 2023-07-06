// using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    static string DATA_PATH = "Assets/Data/";

    private int _currentFighter;
    public Player Player;
    public Enemy Enemy;

    private BattlePhase _currentPhase;

    // Start is called before the first frame update
    void Start()
    {
        _currentFighter = 0;
        _currentPhase = BattlePhase.Main;

        Debug.Log(JsonHelper.LoadJson($"{DATA_PATH}Spells.json"));

        string jsonString = JsonHelper.LoadJson($"{DATA_PATH}Spells.json");

        DataSpell[] dataSpells = JsonHelper.FromJson<DataSpell>(jsonString);
        for(int i = 0; i < 4; i++)
        {
            Debug.Log(dataSpells[i].Name);
        }

        Spell[] spells = {
            dataSpells[0].ToOriginalClass(),
            dataSpells[1].ToOriginalClass(),
            dataSpells[2].ToOriginalClass(),
            dataSpells[3].ToOriginalClass()
        };

        for(int i = 0; i < 4; i++)
        {
            Debug.Log(spells[i].Name);
        }

        jsonString = JsonHelper.LoadJson($"{DATA_PATH}Characters.json");

        DataCharacter[] dataCharacs = JsonHelper.FromJson<DataCharacter>(jsonString);

        for(int i = 0; i < 4; i++)
        {
            Player.Team.Add(dataCharacs[i].ToOriginalClass());
        }

        for(int i = 0; i < 4; i++)
        {
            Debug.Log($"Here ! {Player.Team[i].Name}");
        }

        for(int i = 4; i < 8; i++)
        {
            Enemy.Team.Add(dataCharacs[i].ToOriginalClass());
        }

        for(int i = 0; i < 4; i++)
        {
            Debug.Log($"Here ! {Enemy.Team[i].Name}");
        }

        for(int i = 0; i < 4; i++)
        {
            Player.Team[i].Spells = spells;
        }

        for(int i = 0; i < 4; i++)
        {
            Enemy.Team[i].Spells = spells;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [SerializeField]
    public void GameTurn(int spellNumber)
    {
        Debug.Log($"Spell Number: {spellNumber}");
        Debug.Log($"Current fighter: {_currentFighter}");
        if(PlayerTurn())
        {
            Spell spell = Player.Attack(spellNumber, _currentFighter);
            // Enemy.GetDamage(spell);
            GetDamage(spell);
        }
        else
        {
            spellNumber = Random.Range(0, 4);
            Spell spell = Enemy.Attack(spellNumber, _currentFighter);
            // Player.GetDamage(spell);
            GetDamage(spell);
        }
        _currentFighter++;
    }

    public void GetDamage(Spell spell)
    {
        if(spell.Name == null) return;

        Debug.Log($"Spell {spell.Name} is use !");
        for(int i = 0; i < spell.Targets.Length; i++)
        {
            GetPosition(spell.Targets[i]).GetDamage(spell.Damage);
        }
    }

    public bool PlayerTurn()
    {
        Debug.Log($"Player turn: {(_currentFighter % 2 == 0 ? true : false)}");
        return (_currentFighter % 2 == 0 ? true : false);
    }

    public Character GetPosition(int position)
    {
        if(position < 4)
        {
            return Player.Team[position];
        }
        else if(position < 8)
        {
            return Enemy.Team[position - 4];
        }
        return null;
    }
}

enum BattlePhase
{
    Main,
    Action,
    End
}