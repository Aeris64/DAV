// using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    private int _currentFighter;
    public Player Player;
    public Enemy Enemy;

    private BattlePhase _currentPhase;

    // Start is called before the first frame update
    void Start()
    {
        _currentFighter = 2;
        _currentPhase = BattlePhase.Main;

        Player.Team.Add(new Character("Lyrvis", 10, 5, 0, 15, 0));
        Player.Team.Add(new Character("Ewillian", 15, 0, 5, 10, 1));
        Player.Team.Add(new Character("Limiir", 20, 8, 6, 8, 2));
        Player.Team.Add(new Character("Aeris", 8, 15, 0, 10, 3));

        Enemy.Team.Add(new Character("NPC 1", 10, 5, 0, 15, 0));
        Enemy.Team.Add(new Character("NPC 2", 15, 0, 5, 10, 1));
        Enemy.Team.Add(new Character("NPC 3", 20, 8, 6, 8, 2));
        Enemy.Team.Add(new Character("NPC 4", 8, 15, 0, 10, 3));

        Spell spellA = new Spell("Feu", 5, new int[0, 1], 0, 0);
        Spell spellB = new Spell("Eau", 10, new int[1, 2], 0, 0);
        Spell spellC = new Spell("Terre", 5, new int[2, 3], 0, 0);
        Spell spellD = new Spell("Air", 5, new int[3, 4], 0, 0);

        Spells[] spells = new Spells[spellA, spellB, spellC, spellD];

        Player.Team[0].Spells.Clone(spells);
        Player.Team[1].Spells.Clone(spells);
        Player.Team[2].Spells.Clone(spells);
        Player.Team[3].Spells.Clone(spells);

        Enemy.Team[0].Spells.Clone(spells);
        Enemy.Team[1].Spells.Clone(spells);
        Enemy.Team[2].Spells.Clone(spells);
        Enemy.Team[3].Spells.Clone(spells);
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