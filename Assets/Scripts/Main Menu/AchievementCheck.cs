using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementCheck : MonoBehaviour
{
    [SerializeField] List<GameObject> threshholdsArr = new List<GameObject>();

    [SerializeField] TMP_Text valueText;
    [SerializeField] TMP_Text titleText;

    public enum myEnum // your custom enumeration
    {
        EnemiesDefeated, 
        MinutesInGame, 
        SavedScore,
        TotalMoneySpent
    };
    public myEnum dataToCheck = new myEnum();

    private void OnEnable() {
               titleText.text = dataToCheck.ToString() + ":";

        //Dependiendo del valor seteado en el Engine, accede a esa PLayer Pref
       switch (dataToCheck){
        case myEnum.EnemiesDefeated: 
            valueText.text = PlayerPrefs.GetInt("Enemies Defeated").ToString(); //Convierte su valor a texto
            switch(PlayerPrefs.GetInt("Enemies Defeated")){ //Dependiendo del valor, activa los diferentes iconos
                case <=10:    threshholdsArr[0].SetActive(true);
                    break;
                case <=25:    threshholdsArr[0].SetActive(true);
                            threshholdsArr[1].SetActive(true);
                    break;
                case <=50:    threshholdsArr[0].SetActive(true);
                            threshholdsArr[1].SetActive(true);
                            threshholdsArr[2].SetActive(true);
                    break;
                case <=100:    threshholdsArr[0].SetActive(true);
                            threshholdsArr[1].SetActive(true);
                            threshholdsArr[2].SetActive(true);
                            threshholdsArr[3].SetActive(true);
                    break;
                case <=999:    threshholdsArr[0].SetActive(true);
                            threshholdsArr[1].SetActive(true);
                            threshholdsArr[2].SetActive(true);
                            threshholdsArr[3].SetActive(true);
                            threshholdsArr[4].SetActive(true);
                    break;
                default: 
                                threshholdsArr[0].SetActive(true);
                            threshholdsArr[1].SetActive(true);
                            threshholdsArr[2].SetActive(true);
                            threshholdsArr[3].SetActive(true);
                            threshholdsArr[4].SetActive(true);
                    break;
            }


            break;
        case myEnum.MinutesInGame:
            valueText.text = PlayerPrefs.GetInt("Minutes In Game").ToString();
            switch(PlayerPrefs.GetInt("Minutes In Game")){
                case <=60:    threshholdsArr[0].SetActive(true);
                    break;
                case <=180:    threshholdsArr[0].SetActive(true);
                            threshholdsArr[1].SetActive(true);
                    break;
                case <=560:    threshholdsArr[0].SetActive(true);
                            threshholdsArr[1].SetActive(true);
                            threshholdsArr[2].SetActive(true);
                    break;
                case <=1620:    threshholdsArr[0].SetActive(true);
                            threshholdsArr[1].SetActive(true);
                            threshholdsArr[2].SetActive(true);
                            threshholdsArr[3].SetActive(true);
                    break;
                case <=4860:    threshholdsArr[0].SetActive(true);
                            threshholdsArr[1].SetActive(true);
                            threshholdsArr[2].SetActive(true);
                            threshholdsArr[3].SetActive(true);
                            threshholdsArr[4].SetActive(true);
                    break;
                default: 
                        threshholdsArr[0].SetActive(true);
                        threshholdsArr[1].SetActive(true);
                        threshholdsArr[2].SetActive(true);
                        threshholdsArr[3].SetActive(true);
                        threshholdsArr[4].SetActive(true);
                    break;
            }
            break;
        case myEnum.SavedScore:
            valueText.text = PlayerPrefs.GetInt("savedScore").ToString(); //Convierte su valor a texto
                switch(PlayerPrefs.GetInt("savedScore")){ //Dependiendo del valor, activa los diferentes iconos
                    case <=100:    threshholdsArr[0].SetActive(true);
                        break;
                    case <=200:    threshholdsArr[0].SetActive(true);
                                threshholdsArr[1].SetActive(true);
                        break;
                    case <=400:    threshholdsArr[0].SetActive(true);
                                threshholdsArr[1].SetActive(true);
                                threshholdsArr[2].SetActive(true);
                        break;
                    case <=800:    threshholdsArr[0].SetActive(true);
                                threshholdsArr[1].SetActive(true);
                                threshholdsArr[2].SetActive(true);
                                threshholdsArr[3].SetActive(true);
                        break;
                    case <=99999:    threshholdsArr[0].SetActive(true);
                                threshholdsArr[1].SetActive(true);
                                threshholdsArr[2].SetActive(true);
                                threshholdsArr[3].SetActive(true);
                                threshholdsArr[4].SetActive(true);
                        break;
                    default: 
                                    threshholdsArr[0].SetActive(true);
                                threshholdsArr[1].SetActive(true);
                                threshholdsArr[2].SetActive(true);
                                threshholdsArr[3].SetActive(true);
                                threshholdsArr[4].SetActive(true);
                        break;
                }
            break;
        case myEnum.TotalMoneySpent:
            valueText.text = PlayerPrefs.GetInt("total Money Spent").ToString(); //Convierte su valor a texto
                switch(PlayerPrefs.GetInt("total Money Spent")){ //Dependiendo del valor, activa los diferentes iconos
                    case <=100:    threshholdsArr[0].SetActive(true);
                        break;
                    case <=200:    threshholdsArr[0].SetActive(true);
                                threshholdsArr[1].SetActive(true);
                        break;
                    case <=400:    threshholdsArr[0].SetActive(true);
                                threshholdsArr[1].SetActive(true);
                                threshholdsArr[2].SetActive(true);
                        break;
                    case <=800:    threshholdsArr[0].SetActive(true);
                                threshholdsArr[1].SetActive(true);
                                threshholdsArr[2].SetActive(true);
                                threshholdsArr[3].SetActive(true);
                        break;
                    case <=99999:    threshholdsArr[0].SetActive(true);
                                threshholdsArr[1].SetActive(true);
                                threshholdsArr[2].SetActive(true);
                                threshholdsArr[3].SetActive(true);
                                threshholdsArr[4].SetActive(true);
                        break;
                    default: 
                                    threshholdsArr[0].SetActive(true);
                                threshholdsArr[1].SetActive(true);
                                threshholdsArr[2].SetActive(true);
                                threshholdsArr[3].SetActive(true);
                                threshholdsArr[4].SetActive(true);
                        break;
                }
            break;
       } 
    }
    private void OnDisable() {
        threshholdsArr[0].SetActive(false);
                        threshholdsArr[1].SetActive(false);
                        threshholdsArr[2].SetActive(false);
                        threshholdsArr[3].SetActive(false);
                        threshholdsArr[4].SetActive(false);
    }
}
