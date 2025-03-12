using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class QuestHandler : MonoBehaviour
{
    #region Variables
    //Singleton Instance
    public static QuestHandler Instance;

    //QuestMap to store all the quests by type in Dictionary using a list
    public readonly Dictionary<string, List<Quest>> questMap = new ();
    
    //Number of quests to be generated
    public int numberOfQuests = 3;

    #endregion
    
    #region Quest Handling Methods
    //Method to add Objective to list
    public void AddQuest(Quest quest)
    {
        //If the quest type is not in the dictionary, add it
        if (!questMap.ContainsKey(quest.QuestType))
        {
            questMap.Add(quest.QuestType, new List<Quest>());
        }
        //Add the quest to the list
        questMap[quest.QuestType].Add(quest);
    }
    
    //Method to add progress to the quest
    public void AddProgress(string questType, int amount)
    {
        //If the quest type is in the dictionary
        if (!questMap.ContainsKey(questType)) return;
        //Loop through the list of quests
        foreach (Quest quest in questMap[questType])
        {
            //Increment the value of the quest
            quest.IncrementValue(amount);
            //Print the status of the quest
            Debug.Log(quest.GetStatusText());
        }
        
    }
    
    //Method to check completion of quest map
    public bool CheckAllQuestCompletion()
    {
        foreach (var item in questMap)
        {
            foreach (Quest quest in item.Value)
            {
                if (!quest.isCompleted)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void CreateRandomQuest()
    {
        //Randomly select a quest type
        string questType = UnityEngine.Random.Range(0, 2) == 0 ? "Kill" : "Collect";
        //Randomly select a target value
        int targetValue = UnityEngine.Random.Range(1, 10);
        //Create a new quest
        Quest quest = new Quest(targetValue, questType);
        //Add the quest to the quest map
        AddQuest(quest);
    }
    
    #endregion
    
    #region Unity Methods

    private void Awake()
    {
        //Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            //Dont destroy on load
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        
        //Generate random quests
        for (int i = 0; i < numberOfQuests; i++)
        {
            CreateRandomQuest();
        }
        
        //Subscribe to the OnComplete event of the quest
        foreach (var item in questMap)
        {
            foreach (Quest quest in item.Value)
            {
                //Subscribe to the OnComplete event
                quest.OnComplete += () =>
                {
                    //Log the quest completion
                    Debug.Log($"Quest Completed: {quest.GetQuestTitle()}");
                    if (CheckAllQuestCompletion())
                    {
                        Debug.Log("All Quests Completed");
                        //Make Something happen here
                    }
                };
            }
        }
        
        //_DEBUG
        //Print the quest map
        foreach (var item in questMap)
        {
            foreach (Quest quest in item.Value)
            {
                Debug.Log(quest.GetStatusText());
            }
        }
    }
    

    #endregion
}
