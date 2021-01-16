using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameStatus : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hullIntegrityText;

    enum State { Alive, Dying, Transition };
    State gameState = State.Alive;

    Rocket playerRocket;
    
    int hullIntegrity = 100;

    void Awake()
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
        playerRocket = FindObjectOfType<Rocket>();
    }

    // Update is called once per frame
    void Update()
    {
        hullIntegrityText.text = "Hull Integrity: " + hullIntegrity + "%";
    }

    public void ShipStatus(string typeOfContact)
    {
        switch (typeOfContact)
        {
            case "Wall":
                hullIntegrity = hullIntegrity - 5;
                break;
            case "Ground":
                if (hullIntegrity > 5)
                {
                    hullIntegrity = hullIntegrity - 10;
                }
                else if (hullIntegrity > 0)
                {
                    hullIntegrity = hullIntegrity - 5;
                }
                break;
            case "Safe":
                gameState = State.Alive;
                break;
        }
    }

    public void LevelLoader(string loadLevel)
    {
        switch (loadLevel)
        {
            case "Next":
                gameState = State.Transition;
                Invoke("LoadNextLevel", 3f);
                break;
            case "Restart":
                gameState = State.Dying;
                Invoke("Restart", 3f);
                break;
        }
    }
    public int HullIntegrityCheck()
    {
        return hullIntegrity;
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    private void Restart()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
