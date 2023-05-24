using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    // Start is called before the first frame update
    void Start()
    {
        // var toto = JsonUtility.FromJson<List<Character>>("");
        // string jsonString = "{\r\n    \"Items\": [\r\n        {\r\n            \"playerId\": \"8484239823\",\r\n            \"playerLoc\": \"Powai\",\r\n            \"playerNick\": \"Random Nick\"\r\n        },\r\n        {\r\n            \"playerId\": \"512343283\",\r\n            \"playerLoc\": \"User2\",\r\n            \"playerNick\": \"Rand Nick 2\"\r\n        }\r\n    ]\r\n}";
        // string json = JsonHelper.LoadJson("Assets/Data/Characters.json");
        // Debug.Log(json);

        // Character[] CustomTeam = JsonHelper.FromJson<Character>(json);
        // Debug.Log(CustomTeam[0].Name);
        // Debug.Log(CustomTeam[1].Name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void attack()
    {
        int heroTurn = Random.Range(0, 3);

        // No attack
        if(Team[heroTurn].Spells.Length <= 0) return;

        int spellChoose = Random.Range(0, Team[heroTurn].Spells.Length);
    }
}