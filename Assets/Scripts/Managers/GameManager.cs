using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject homeBaseLevel;
    [SerializeField] private GameObject fishingLevel;
    [SerializeField] private GameObject preppingLevel;
    public static GameManager instance;

    public float divingSpeedThisLevel; // the speed in which the background and the fish move during fishing

    private List<Fish> caughtFish = new List<Fish>();


    private float depth = 0; 
    [SerializeField] private GameObject[] fishesToSpawn;
    [SerializeField] private float timeBetweenSpawns = 5;
    float spawnTimer = 0;

    private enum GameStates
    {
        mainMenu,
        lobby,
        fishing,
        prepping
    }

    private GameStates state = GameStates.lobby;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    public void HandleFishing()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTimer > timeBetweenSpawns)
        {
            spawnCluster();
            spawnTimer  = 0;
        }
    }

    void Update()
    {
        if(state == GameStates.fishing)
        {
            HandleFishing();
        }
    }

    void spawnCluster()
    {
        float spawnChance = 1;
        bool isSpawning = true;
        GameObject mainFish = null;
        while (isSpawning) // creates clusters of fish
        {
            if(mainFish == null)
            {
                mainFish = SpawnFish();
            }
            else
            {
                SpawnFish(mainFish);
            }
            
            spawnChance *= 1 - 0.99f; // spawn chance decreases as more fish are spawned
            isSpawning = Random.Range(0f,1f) <= spawnChance; // if spawn chance is lower than random number, stop spawning fish for this cluster
        }
    }

    GameObject SpawnFish() // spawns a singular fish. type is based off of depth
    {
        Vector2 randomPosition =  Camera.main.ScreenToWorldPoint(new Vector2(Random.Range(0,Screen.width), 0));
        GameObject randomlySelectedFish = fishesToSpawn[Random.Range(0,fishesToSpawn.Count())];
        GameObject fishInstance = Instantiate(randomlySelectedFish);
        fishInstance.transform.position = randomPosition;
        return fishInstance;
    }
    GameObject SpawnFish(GameObject mainFish) // spawns a singular fish. type is based off of depth
    {
        mainFish.transform.Rotate(0,0,Random.Range(0,360));
        Vector2 randomPosition =  mainFish.transform.position + mainFish.transform.up * Random.Range(-10f,10f);
        GameObject randomlySelectedFish = fishesToSpawn[Random.Range(0,fishesToSpawn.Count())];
        GameObject fishInstance = Instantiate(randomlySelectedFish);
        fishInstance.transform.position = randomPosition;
        mainFish.transform.up = Vector2.up;
        return fishInstance;
    }

    public void BaseToFishing()
    {
        fishingLevel.SetActive(true); // sets the fishing level game object as active
        homeBaseLevel.SetActive(false); // sets the homeBaseLevel game object as in-active. this means that the components of that object and their children are disabled
        depth = 0;
        divingSpeedThisLevel = 1; // sets the diving speed level for this level
        state = GameStates.fishing;
    }

    public void AddFishToBucket(Fish fishToAdd)
    {
        caughtFish.Add(fishToAdd); // adds the fish game object to the list of caught fish. 
    }


}
