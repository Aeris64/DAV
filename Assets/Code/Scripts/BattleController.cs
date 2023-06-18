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
        _currentFighter = 2;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [SerializeField]
    public void GameTurn(int spellNumber)
    {
        if(PlayerTurn())
        {
            Spell spell = Player.Attack(spellNumber, _currentFighter);
            Enemy.GetDamage(spell);
        }
        else
        {
            spellNumber = Random.Range(0, 4);
            Spell spell = Enemy.Attack(spellNumber, _currentFighter);
            Player.GetDamage(spell);
        }
        _currentFighter++;
    }

    public bool PlayerTurn()
    {
        Debug.Log($"Player turn: {(_currentFighter % 2 == 0 ? true : false)}");
        return (_currentFighter % 2 == 0 ? true : false);
    }
}

enum BattlePhase
{
    Main,
    Action,
    End
}