using System.Collections.Generic;
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
        while (isSpawning) // creates clusters of fish
        {
            SpawnFish();
            spawnChance *= 1 - 0.125f; // spawn chance decreases as more fish are spawned
            isSpawning = Random.Range(0f,1f) <= spawnChance; // if spawn chance is lower than random number, stop spawning fish for this cluster
        }
    }

    void SpawnFish() // spawns a singular fish. type is based off of depth
    {
        Debug.Log("Spawned"); 

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
