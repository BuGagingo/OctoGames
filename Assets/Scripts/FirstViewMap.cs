using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Naninovel;

public class FirstViewMap : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        VariableManager.UpdateVariables();
        if (VariableManager.quest_stage == 1)
        {
            Engine.GetService<ICustomVariableManager>().SetVariableValue("quest_stage", (VariableManager.quest_stage+1).ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
