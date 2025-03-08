using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Naninovel;

public class MenuManager : MonoBehaviour
{
    public GameObject map;
    public GameObject blockLoc3;
    public List<GameObject> questLogs = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject questLog in questLogs)
        {
            questLog.SetActive(false);
        }
        questLogs[0].SetActive(true);
    }



    // Update is called once per frame
    void Update()
    {
        VariableManager.UpdateVariables();
        if (Input.GetKeyDown(KeyCode.Tab) && VariableManager.quest_stage != 0)
        {
            map.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Tab) && VariableManager.quest_stage != 0)
        {
            map.SetActive(false);
        }
        if (VariableManager.quest_stage == 3)
        {
            blockLoc3.SetActive(false);
        }
        for(int i = 0; i < questLogs.Count; i++)
        {
            if (VariableManager.quest_stage == i)
            {
                questLogs[i].SetActive(true);
            }
        }
    }
}


