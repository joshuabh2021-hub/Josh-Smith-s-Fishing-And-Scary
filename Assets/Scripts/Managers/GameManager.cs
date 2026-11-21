using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject homeBaseLevel;
    [SerializeField] private GameObject fishingLevel;
    [SerializeField] private GameObject preppingLevel;
    public static GameManager instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    public void BaseToFishing()
    {
        fishingLevel.SetActive(true);
        homeBaseLevel.SetActive(false);
    }
}
