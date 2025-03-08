using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Naninovel;

public class VariableManager : MonoBehaviour
{
    public static int game_status = 0;
    public static int quest_stage = 0;

    public static string player_name = "";

    public static int item_bra = 0;
    public static int item_panties = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void SaveVariables()
    {
        Engine.GetService<ICustomVariableManager>().SetVariableValue("game_status", game_status.ToString());
        Engine.GetService<ICustomVariableManager>().SetVariableValue("quest_stage", quest_stage.ToString());
        Engine.GetService<ICustomVariableManager>().SetVariableValue("name", player_name);
        Engine.GetService<ICustomVariableManager>().SetVariableValue("item_bra", item_bra.ToString());
        Engine.GetService<ICustomVariableManager>().SetVariableValue("item_panties", item_panties.ToString());
    }
    public static void UpdateVariables()
    {
        game_status = int.Parse(Engine.GetService<ICustomVariableManager>().GetVariableValue("game_status"));
        quest_stage = int.Parse(Engine.GetService<ICustomVariableManager>().GetVariableValue("quest_stage"));
        player_name = Engine.GetService<ICustomVariableManager>().GetVariableValue("name");
        item_bra = int.Parse(Engine.GetService<ICustomVariableManager>().GetVariableValue("item_bra"));
        item_panties = int.Parse(Engine.GetService<ICustomVariableManager>().GetVariableValue("item_panties"));
        // print("game_status: " + game_status);
        // print("quest_stage: " + quest_stage);
        // print("player_name: " + player_name);
        // print("item_bra: " + item_bra);
        // print("item_panties: " + item_panties);
    }

}


