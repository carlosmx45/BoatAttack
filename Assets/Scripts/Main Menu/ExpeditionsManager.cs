using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpeditionsManager : MonoBehaviour
{
    [Header("Objetos de UI")]
    [SerializeField] GameObject expeditionButton1;
    [SerializeField] GameObject expeditionButton2;
    [SerializeField] GameObject expeditionButton3;
    [SerializeField] GameObject expeditionComplitedSFX;

    [Header("Objetos de Timer")]
    public float timer1 = -10;
    public Text timer1Text;
    public float timer2 = -10;
    public Text timer2Text;
    public float timer3 = -10;
    public Text timer3Text;

    public bool expedition1Completed;
    public bool expedition2Completed;
    public bool expedition3Completed;

    public int randomRum;
    public int rumForEx1;
    public int rumForEx2;
    public int rumForEx3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        randomRum = Random.Range(10, 100);

        timer1 -= Time.deltaTime;
        timer1Text.text = "" + timer1.ToString("f0");
        timer2 -= Time.deltaTime;
        timer2Text.text = "" + timer2.ToString("f0");
        timer3 -= Time.deltaTime;
        timer3Text.text = "" + timer3.ToString("f0");

        if (timer1 <= 0 && timer1 >= -10)
        {
            expedition1Completed = true;
            rumForEx1 = randomRum;
            if (expedition1Completed == true)
            {
                Instantiate(expeditionComplitedSFX, this.transform.position, this.transform.rotation);
                expeditionButton1.SetActive(true);
                PlayerPrefs.SetInt("savedRum", PlayerPrefs.GetInt("savedRum") + rumForEx1);
                timer1 = -50;
                expedition1Completed = false;
            }
        }
        if (timer2 <= 0 && timer2 >= -10)
        {
            expedition2Completed = true;
            rumForEx2 = randomRum;
            if (expedition2Completed == true)
            {
                Instantiate(expeditionComplitedSFX, this.transform.position, this.transform.rotation);
                expeditionButton2.SetActive(true);
                PlayerPrefs.SetInt("savedRum", PlayerPrefs.GetInt("savedRum") + rumForEx2);
                timer2 = -50;
                expedition2Completed = false;
            }
        }
        if (timer3 <= 0 && timer3 >= -10)
        {
            expedition3Completed = true;
            rumForEx3 = randomRum;
            if (expedition3Completed == true)
            {
                Instantiate(expeditionComplitedSFX, this.transform.position, this.transform.rotation);
                expeditionButton3.SetActive(true);
                PlayerPrefs.SetInt("savedRum", PlayerPrefs.GetInt("savedRum") + rumForEx3);
                timer3 = -50;
                expedition3Completed = false;
            }
        }
    }

    public void Expedition1()
    {
        if (PlayerPrefs.GetInt("savedScore") >= 300)
        {
            PlayerPrefs.SetInt("savedScore", PlayerPrefs.GetInt("savedScore") - 300);
            expeditionButton1.SetActive(false);
            timer1 = 60;
        }
    }

    public void Expedition2()
    {
        if (PlayerPrefs.GetInt("savedScore") >= 300)
        {
            PlayerPrefs.SetInt("savedScore", PlayerPrefs.GetInt("savedScore") - 300);
            expeditionButton2.SetActive(false);
            timer2 = 60;
        }
    }

    public void Expedition3()
    {
        if (PlayerPrefs.GetInt("savedScore") >= 300)
        {
            PlayerPrefs.SetInt("savedScore", PlayerPrefs.GetInt("savedScore") - 300);
            expeditionButton3.SetActive(false);
            timer3 = 60;
        }
    }
}
