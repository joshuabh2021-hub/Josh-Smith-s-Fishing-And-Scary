using UnityEngine;

public class OceanCage : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        GameManager.instance.BaseToFishing();
        Debug.Log("changing to fishing level");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
