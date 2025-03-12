using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingGame : MonoBehaviour
{
    [Header("Referencias De UI")]
    public Image fishArea;
    public Button fishingButton;
    public Text caughtFishText;
    public Text caughtFishSubtitleText;
    public Image caughtFishImage;

    [Header("Sprites para los peces")]
    public Sprite sardinaSprite;
    public Sprite arenqueSprite;
    public Sprite pezGloboSprite;
    public Sprite huachinangoSprite;
    public Sprite atunSprite;
    public Sprite tiburonSprite;
    public Sprite pezEspadaSprite;

    [Header("Variables")]
    private bool isFishing = false;
    private Vector2 fishPosition;
    private float fishingTime = 3.0f; // Tiempo para atrapar un pez
    private float currentTime = 0.0f;
    private List<Fish> fishList;
    private Fish currentFish;

    void Start()
    {
        fishingButton.onClick.AddListener(OnFishingButtonClicked);
        //fishPosition = GetRandomFishPosition();
        InitializeFishList();
        caughtFishText.text = "";
        caughtFishSubtitleText.text = "";
        caughtFishImage.enabled = false; // Esconder la imagen inicialmente
    }

    void Update()
    {
        if (isFishing)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= fishingTime)
            {
                CatchFish();
            }
        }
    }

    void OnFishingButtonClicked()
    {
        if (!isFishing)
        {
            StartFishing();
        }
        else
        {
            StopFishing();
        }
    }

    void StartFishing()
    {
        isFishing = true;
        currentTime = 0.0f;
        currentFish = GetRandomFish();
        caughtFishText.text = "";
        caughtFishSubtitleText.text = "";
        caughtFishImage.enabled = false; // Esconder la imagen cuando comienza la pesca
    }

    void StopFishing()
    {
        isFishing = false;
        currentTime = 0.0f;
    }

    void CatchFish()
    {
        isFishing = false;
        currentTime = 0.0f;
        caughtFishText.text = $"¡You have caught a {currentFish.Name}!";
        caughtFishSubtitleText.text = $"Sale Price: ${currentFish.Reward}";
        caughtFishImage.sprite = currentFish.FishSprite;
        caughtFishImage.enabled = true;
    }

    void InitializeFishList()
    {
        fishList = new List<Fish>
        {
            new Fish("Sardine", 0.7f, 10, sardinaSprite),
            new Fish("Herring", 0.6f, 30, arenqueSprite),
            new Fish("Puffer Fish", 0.5f, 50, pezGloboSprite),
            new Fish("Red Snapper", 0.4f, 100, huachinangoSprite),
            new Fish("Tuna", 0.3f, 300, atunSprite),
            new Fish("Shark", 0.1f, 450, tiburonSprite),
            new Fish("Sword Fish", 0.1f, 500, pezEspadaSprite)
        };
    }

    Fish GetRandomFish()
    {
        float totalProbability = 0;
        foreach (Fish fish in fishList)
        {
            totalProbability += fish.CatchProbability;
        }

        float randomPoint = Random.value * totalProbability;

        foreach (Fish fish in fishList)
        {
            if (randomPoint < fish.CatchProbability)
            {
                return fish;
            }
            else
            {
                randomPoint -= fish.CatchProbability;
            }
        }

        return fishList[0]; // Fallback, aunque no debería suceder
    }
}
