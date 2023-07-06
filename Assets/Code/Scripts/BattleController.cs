using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleController : MonoBehaviour
{
    static string DATA_PATH = "Assets/Data/";

    private int _currentFighter;
    public Player Player;
    public Enemy Enemy;

    public Canvas MyCanvas;

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

        // /!\ Attention dépendance à la nomenclature des containers !!!!
        var CanvasContainers = MyCanvas.GetComponentsInChildren<Transform>().Where(child => child.name.Contains("Container")).ToList();

        for(int i = 0; i < CanvasContainers.Count(); i++)
        {
            var actualContainer = CanvasContainers[i];
            if(!actualContainer) continue;

            var characterNameContainer = actualContainer.Find("CharacterName");
            if(characterNameContainer)
            {
                var characterName = characterNameContainer.GetComponent<TMP_Text>();
                if(characterName)
                {
                    characterName.text = Player.Team[i].Name;
                }
            }

            var healthPointContainer = actualContainer.Find("HealthPoint");
            if(healthPointContainer)
            {
                var healthPoint = healthPointContainer.GetComponent<TMP_Text>();
                if(healthPoint)
                {
                    healthPoint.text = $"HP: {Player.Team[i].LifePoint}";
                }
            }

            var manaPointContainer = actualContainer.Find("ManaPoint");
            if(manaPointContainer)
            {
                var manaPoint = manaPointContainer.GetComponent<TMP_Text>();
                if(manaPoint)
                {
                    manaPoint.text = $"MP: {Player.Team[i].ManaPoint}";
                }
            }
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
            GetDamage(spell);
        }
        else
        {
            spellNumber = Random.Range(0, 4);
            Spell spell = Enemy.Attack(spellNumber, _currentFighter);
            GetDamage(spell, true);
        }
        _currentFighter++;
    }

    public void GetDamage(Spell spell, bool npcTurn = false)
    {
        if(spell.Name == null) return;

        Debug.Log($"Spell {spell.Name} is use !");
        for(int i = 0; i < spell.Targets.Length; i++)
        {
            int actualTarget = spell.Targets[i];
            if(npcTurn)
            {
                actualTarget = 7 - spell.Targets[i];
            }
            GetPosition(actualTarget).GetDamage(spell.Damage);
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