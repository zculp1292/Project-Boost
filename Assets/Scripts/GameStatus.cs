using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStatus : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hullIntegrityText;

    int hullIntegrity = 100;

    void awake()
    {
        int gameStatusCount = FindObjectsOfType<GameStatus>().Length;

        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hullIntegrityText.text = "Hull Integrity: " + hullIntegrity + "%";
    }

    public void DamageShip(string typeOfDamage)
    {
        switch (typeOfDamage)
        {
            case "Wall":
                hullIntegrity = hullIntegrity - 5;
                break;
            case "Ground":
                hullIntegrity = hullIntegrity - 10;
                break;
        }
    }
}
