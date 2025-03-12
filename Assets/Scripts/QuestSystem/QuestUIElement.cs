using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestUIElement : MonoBehaviour
{
    #region Variables
    private Quest quest;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Image iconImage;
    [SerializeField] private Image progressBar; //MISSING
    #endregion

    #region Methods
    public void SetQuest(Quest quest)
    {
        this.quest = quest;
        quest.OnValueChange += UpdateUI;
        quest.OnComplete += QuestComplete;
        titleText.text = this.quest.GetQuestTitle();
        UpdateUI();
    }

    private void UpdateUI()
    {
        descriptionText.text = quest.GetStatusText();
    }

    private void QuestComplete()
    {
        Debug.Log("Quest Completed");
    }
    #endregion
    
    #region Unity Methods

    /*private void OnEnable()
    {
        UpdateUI();
    }*/

    private void OnDestroy()
    {
        quest.OnValueChange -= UpdateUI;
        quest.OnComplete -= QuestComplete;
    }
    #endregion
}
