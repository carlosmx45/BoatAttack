using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUIDisplay : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject elementPrefab;
    
    #endregion
    
    #region Unity Methods

    /*private void OnEnable()
    {
        //Get all the quests from the QuestHandler
        foreach (var item in QuestHandler.Instance.questMap)
        {
            foreach (Quest quest in item.Value)
            {
                //Instantiate the prefab
                GameObject element = Instantiate(elementPrefab, this.transform);
                //Get the QuestUIElement component
                QuestUIElement questUIElement = element.GetComponent<QuestUIElement>();
                //Set the quest
                questUIElement.SetQuest(quest);
            }
        }
    }*/

    private void Start()
    {
        //Get all the quests from the QuestHandler
        foreach (var item in QuestHandler.Instance.questMap)
        {
            foreach (Quest quest in item.Value)
            {
                //Instantiate the prefab
                GameObject element = Instantiate(elementPrefab, this.transform);
                //Get the QuestUIElement component
                QuestUIElement questUIElement = element.GetComponent<QuestUIElement>();
                //Set the quest
                questUIElement.SetQuest(quest);
            }
        }
    }

    #endregion
}
