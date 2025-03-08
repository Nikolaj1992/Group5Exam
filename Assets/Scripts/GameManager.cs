using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    // If necessary we can later make this a singleton for easier use
    private bool gameIsOver = false;

    [SerializeField] private int playerScore = 0;
    [SerializeField] private int enemiesKilled = 0;

    public string gameScene = "SampleScene";    // Handle target scene in inspector
    
    private GameObject mainMenu;
    private GameObject loadoutMenu;
    
    private void Start()
    {
        if (string.IsNullOrEmpty(gameScene))
        {
            Debug.LogError("Game scene name is not set!");
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    private void Awake()
    {
        GameManager existingInstance = FindFirstObjectByType<GameManager>();

        if (existingInstance != null && existingInstance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main Menu")
        {
            Transform menuCanvas = GameObject.Find("MenuCanvas")?.transform;
            if (menuCanvas != null)
            {
                mainMenu = menuCanvas.Find("MainMenu")?.gameObject;
                loadoutMenu = menuCanvas.Find("LoadoutMenu")?.gameObject;

                if (mainMenu == null || loadoutMenu == null)
                {
                    Debug.LogError("MainMenu or LoadoutMenu not found in the scene!");
                }
                else
                {
                    // We want the loadout menu
                    mainMenu.SetActive(false);
                    loadoutMenu.SetActive(true);
                }
            }
        }
    }
    
    public void AddScore(int amount)
    {
        playerScore += amount;
    }

    public void AddKill()
    {
        enemiesKilled++;
    }

    public void EndRound()
    {
        if (gameIsOver) return;

        gameIsOver = true;

        StartCoroutine(EndRoundCoroutine());
    }

    private IEnumerator EndRoundCoroutine()
    {
        // Potentially insert animation or screen fade here?
        
        yield return new WaitForSeconds(1f);

        SceneManager.LoadSceneAsync("Main Menu");
    }

    public void StartNewRound()
    {
        gameIsOver = false;
        
        // We should have reset logic here for player position, resetting puzzles ect.
        
        if (!string.IsNullOrEmpty(gameScene))
        {
            SceneManager.LoadScene(gameScene);  // Load by scene name
        }
        else
        {
            Debug.LogError("Game scene name is not set!");
        }
    }
    
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
