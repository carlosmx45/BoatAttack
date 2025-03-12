using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Quest 
{
    //Progress counters
    public int currentValue { get; private set; }
    public int targetValue { get;}
    public bool isCompleted { get; private set; }
    
    //EventTriggers
    public Action OnComplete;
    public Action OnValueChange;

    public string QuestType { get; }
    
    //Constructor
    public Quest(int targetValue, string questType)
    {
        this.targetValue = targetValue;
        this.QuestType = questType;
    }
    
    //Method to increment the value of the quest
    public void IncrementValue(int amount)
    {
        currentValue += amount;
        OnValueChange?.Invoke(); //Invoke for UI update
        
        //If the current value is greater than or equal to the target value, the quest is completed
        //Can be separate method
        if (currentValue >= targetValue)
        {
            isCompleted = true;
            OnComplete?.Invoke();
        }
    }

    public string GetStatusText()
    {
        //Switch expression for quest type
        return QuestType switch
        {
            "Kill" => $"Kill {currentValue} / {targetValue} enemies",
            "Collect" => $"Collect {currentValue} / {targetValue} coins",
            _ => "Quest Type not found"
        };
    }
    
    public string GetQuestTitle()
    {
        return QuestType switch
        {
            "Kill" => "Hunt them down!",
            "Collect" => "Gather the coins!",
            _ => "Quest Type not found"
        };
    }
}
