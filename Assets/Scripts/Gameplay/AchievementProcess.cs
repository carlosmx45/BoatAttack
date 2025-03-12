using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementProcess : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Initialize PlayerPref Data Keys
            //Minute Data
        if (PlayerPrefs.HasKey("Minutes In Game") == false)
        {
            PlayerPrefs.SetInt("Minutes In Game", 0);
        }

        //Invoke Repeating every minute to save MinuteData
        InvokeRepeating("SaveMinuteData", 60.0f, 60.0f);
        Debug.Log("Started Timer");


            //Enemies Defeated
        if (PlayerPrefs.HasKey("Enemies Defeated") == false)
        {
            PlayerPrefs.SetInt("Enemies Defeated", 0);
        }


    }

    //CancelInvokes  Call on GameOver
    public void StopInvokeMinuteData(){
        CancelInvoke("SaveMinuteData");
    }

    //Save Minute Data
    void SaveMinuteData(){
        PlayerPrefs.SetInt("Minutes In Game", PlayerPrefs.GetInt("Minutes In Game") + 1);
        Debug.Log("Saved Data!" + PlayerPrefs.GetInt("Minutes In Game"));
        
    }
}