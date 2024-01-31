using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class BattleController : MonoBehaviour
{
    static string DATA_PATH = "Assets/Data/";

    private int _currentFighterNb;
    private int _currentTurnPlayer;
    private int _currentTurnNb = 0;
    public Player Player;
    public Enemy Enemy;

    public Canvas MyCanvas;

    private BattlePhase _currentPhase;

    [SerializeField]
    private Transform _roundOwner;
    public Transform RoundOwner
    {
        get
        {
            return this._roundOwner;
        }
        set
        {
            this._roundOwner = value;
        }
    }

    [SerializeField]
    private Transform _menuSpells;
    public Transform MenuSpells
    {
        get
        {
            return this._menuSpells;
        }
        set
        {
            this._menuSpells = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _currentFighterNb = 0;
        _currentTurnPlayer = 0;
        _currentTurnNb = 0;
        _currentPhase = BattlePhase.Main;

        Debug.Log(JsonHelper.LoadJson($"{DATA_PATH}Spells.json"));

        string jsonString = JsonHelper.LoadJson($"{DATA_PATH}Spells.json");

        DataSpell[] dataSpells = JsonHelper.FromJson<DataSpell>(jsonString);
        // for(int i = 0; i < 4; i++)
        // {
        //     Debug.Log($"{dataSpells[i].Name}: {dataSpells[i].Damage} dmg / {dataSpells[i].Cost}");
        // }

        Spell[] spells = {
            dataSpells[0].ToOriginalClass(),
            dataSpells[1].ToOriginalClass(),
            dataSpells[2].ToOriginalClass(),
            dataSpells[3].ToOriginalClass()
        };

        // for(int i = 0; i < 4; i++)
        // {
        //     Debug.Log(spells[i].Name);
        // }

        jsonString = JsonHelper.LoadJson($"{DATA_PATH}Characters.json");

        DataCharacter[] dataCharacs = JsonHelper.FromJson<DataCharacter>(jsonString);

        for(int i = 0; i < 4; i++)
        {
            Player.Team.Add(dataCharacs[i].ToOriginalClass());
        }

        // for(int i = 0; i < 4; i++)
        // {
        //     Debug.Log($"Here ! {Player.Team[i].Name}");
        // }

        for(int i = 4; i < 8; i++)
        {
            Enemy.Team.Add(dataCharacs[i].ToOriginalClass());
        }

        // for(int i = 0; i < 4; i++)
        // {
        //     Debug.Log($"Here ! {Enemy.Team[i].Name}");
        // }

        for(int i = 0; i < 4; i++)
        {
            Player.Team[i].Spells = spells;
        }

        for(int i = 0; i < 4; i++)
        {
            Enemy.Team[i].Spells = spells;
        }

        FillTeam("ContainerCharacter", Player.Team);
        FillTeam("ContainerEnemy", Enemy.Team);
        FillSpells();
    }

    // Update is called once per frame
    void Update()
    {

    }

    [SerializeField]
    public void GameTurn(int spellNumber)
    {
        Debug.Log($"Spell Number: {spellNumber}");
        Debug.Log($"Current fighter: {_currentFighterNb}");
        if(PlayerTurn())
        {
            Spell spell = Player.Attack(spellNumber, _currentFighterNb);
            GetDamage(spell);
        }
        else
        {
            spellNumber = Random.Range(0, 4);
            Spell spell = Enemy.Attack(spellNumber, _currentFighterNb);
            GetDamage(spell, true);
            _currentFighterNb++;
        }

        _currentTurnPlayer++;

        if(_currentFighterNb >= 4)
        {
            _currentFighterNb = 0;
            _currentTurnNb++;
        }

        var roundOwnerText = RoundOwner.GetComponent<TMP_Text>();
        if(roundOwnerText)
        {
            roundOwnerText.text = $"Round: {_currentTurnNb}\n {(PlayerTurn() ? "Player" : "Enemy")}";
        }
    }

    public void GetDamage(Spell spell, bool npcTurn = false)
    {
        if(spell.Name == null) return;

        Debug.Log($"Spell {spell.Name} is used !");
        for(int i = 0; i < spell.TargetAlly.Length; i++)
        {
            int actualTarget = spell.TargetAlly[i];
            if(npcTurn)
            {
                actualTarget = 4 + spell.TargetAlly[i];
            }

            Character target = GetPosition(actualTarget);
            target.GetDamage(- spell.Damage);
        }

        for(int i = 0; i < spell.TargetEnemy.Length; i++)
        {
            int actualTarget = 4 + spell.TargetEnemy[i];
            if(npcTurn)
            {
                actualTarget = spell.TargetEnemy[i];
            }

            Character target = GetPosition(actualTarget);
            target.GetDamage(spell.Damage);
        }
    }

    public bool PlayerTurn()
    {
        Debug.Log($"Player turn: {(_currentTurnPlayer % 2 == 0)}");
        return _currentTurnPlayer % 2 == 0;
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

    public void FillTeam(string ContainerName, List<Character> Team)
    {
        // /!\ Attention dépendance à la nomenclature des containers !!!!
        var CanvasContainers = MyCanvas.GetComponentsInChildren<Transform>().Where(child => child.name.Contains(ContainerName)).ToList();

        for(int i = 0; i < CanvasContainers.Count(); i++)
        {
            Team[i].Container = CanvasContainers[i];

            var characterNameContainer = Team[i].Container.Find("CharacterName");
            if(characterNameContainer)
            {
                var characterName = characterNameContainer.GetComponent<TMP_Text>();
                if(characterName)
                {
                    characterName.text = Team[i].Name;
                }
            }

            var healthPointContainer = Team[i].Container.Find("HealthPoint");
            if(healthPointContainer)
            {
                var healthPoint = healthPointContainer.GetComponent<TMP_Text>();
                if(healthPoint)
                {
                    healthPoint.text = $"HP: {Team[i].LifePoint}";
                }
            }

            var manaPointContainer = Team[i].Container.Find("ManaPoint");
            if(manaPointContainer)
            {
                var manaPoint = manaPointContainer.GetComponent<TMP_Text>();
                if(manaPoint)
                {
                    manaPoint.text = $"MP: {Team[i].ManaPoint}";
                }
            }
        }
    }

    public void FillSpells()
    {
        if(!_menuSpells) return;

        var currentFighter = Player.Team[_currentFighterNb];
        if(!PlayerTurn())
        {
            currentFighter = Enemy.Team[_currentFighterNb];
        }

        var spellButtons = _menuSpells.GetComponentsInChildren<TMP_Text>();
        for(int i = 0; i < spellButtons.Count(); i++)
        {
            // TODO, ils sont null wtf ?
            // if(currentFighter.Spells[i])
            // {
                spellButtons[i].text = $"{currentFighter.Spells[i].Name}";
            // }
        }
    }
}

enum BattlePhase
{
    Main,
    Action,
    End
}