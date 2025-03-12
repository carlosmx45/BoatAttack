using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreScript : MonoBehaviour
{
    public CharacterInfo characterInfo;

    public Text imprimirDinero;
    public Text imprimirNievlDeVelocidad;
    public Text imprimirNivelDeVida;
    public Text imprimirNivelDeAtaque;

    public GameObject investPanel;

    [SerializeField] GameObject anchorLv1;
    [SerializeField] GameObject anchorLv2;
    [SerializeField] GameObject updateAnchorParticle;
    [SerializeField] GameObject updateSound;

    public int storeLevel;

    private void Start()
    {
        storeLevel = characterInfo.storeLevel;

        if (storeLevel == 1)
        {
            anchorLv1.SetActive(true);
            anchorLv2.SetActive(false);
        }
        else
        {
            anchorLv1.SetActive(false);
            anchorLv2.SetActive(true);
            investPanel.SetActive(false);
        }
    }

    void Update()
    {
        imprimirDinero.text = "Your Money: $" + PlayerPrefs.GetInt("savedScore");
        imprimirNievlDeVelocidad.text = "Speed: " + characterInfo.baseMovementSpeed;
        imprimirNivelDeVida.text = "Life: " + characterInfo.baseHealth;
        imprimirNivelDeAtaque.text = "Cannons: " + characterInfo.cannonsLevel;
    }

    public void BuyInStore(string name)
    {
        switch (name)
        {
            case "Speed":
                if (storeLevel == 1)
                {
                    if (PlayerPrefs.GetInt("savedScore") >= 500)
                    {
                        characterInfo.baseMovementSpeed++;
                        PlayerPrefs.SetInt("savedScore", PlayerPrefs.GetInt("savedScore") - 500);
                        Debug.Log("Compraste Velocidad");
                    }
                    else
                    {
                        Debug.Log("Dinero Insuficiente");
                    }
                }
                else
                {
                    if (PlayerPrefs.GetInt("savedScore") >= 250)
                    {
                        characterInfo.baseMovementSpeed++;
                        PlayerPrefs.SetInt("savedScore", PlayerPrefs.GetInt("savedScore") - 250);
                        Debug.Log("Compraste Velocidad");
                    }
                    else
                    {
                        Debug.Log("Dinero Insuficiente");
                    }
                }
                break;
            case "Shield":
                if (storeLevel == 1)
                {
                    if (PlayerPrefs.GetInt("savedScore") >= 500)
                    {
                        characterInfo.baseHealth++;
                        PlayerPrefs.SetInt("savedScore", PlayerPrefs.GetInt("savedScore") - 500);
                        Debug.Log("Compraste Vida");
                    }
                    else
                    {
                        Debug.Log("Dinero Insuficiente");
                    }
                }
                else
                {
                    if (PlayerPrefs.GetInt("savedScore") >= 250)
                    {
                        characterInfo.baseHealth++;
                        PlayerPrefs.SetInt("savedScore", PlayerPrefs.GetInt("savedScore") - 250);
                        Debug.Log("Compraste Vida");
                    }
                    else
                    {
                        Debug.Log("Dinero Insuficiente");
                    }
                }
                break;
            case "Damage":
                if (storeLevel == 1)
                {
                    if (PlayerPrefs.GetInt("savedScore") >= 500)
                    {
                        characterInfo.baseAttackSpeed++;
                        PlayerPrefs.SetInt("savedScore", PlayerPrefs.GetInt("savedScore") - 500);
                        Debug.Log("Compraste Daño");
                    }
                    else
                    {
                        Debug.Log("Dinero Insuficiente");
                    }
                }
                else
                {
                    if (PlayerPrefs.GetInt("savedScore") >= 250)
                    {
                        characterInfo.baseAttackSpeed++;
                        PlayerPrefs.SetInt("savedScore", PlayerPrefs.GetInt("savedScore") - 250);
                        Debug.Log("Compraste Daño");
                    }
                    else
                    {
                        Debug.Log("Dinero Insuficiente");
                    }
                    break;
                }
                break;
            case "Invest":
                if (PlayerPrefs.GetInt("savedScore") >= 10000)
                {
                    characterInfo.storeLevel++;
                    storeLevel = 2;
                    anchorLv1.SetActive(false);
                    anchorLv2.SetActive(true);
                    investPanel.SetActive(false);
                    updateAnchorParticle.SetActive(true);
                    Instantiate(updateSound, this.transform.position, this.transform.rotation);
                    Debug.Log("Invercion de Tienda");
                }
                else
                {
                    Debug.Log("Dinero Insuficiente");
                }
                break;
            default:
                Debug.Log("Opcion Invalida");
                break;
        }
    }
}
