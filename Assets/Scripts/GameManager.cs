using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    // If necessary we can later make this a singleton for easier use
    private bool gameIsOver = false;

    public string gameScene = "SampleScene";    // Handle target scene in inspector
    
    private void Start()
    {
        if (string.IsNullOrEmpty(gameScene))
        {
            Debug.LogError("Game scene name is not set!");
        }
    }
    
    private void Awake()
    {
        // We only want one instance
        if (GameObject.FindWithTag("GameManager") != null && GameObject.FindWithTag("GameManager") != this.gameObject)
        {
            Destroy(this.gameObject);  // Remove duplicates
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);  // To save stats, gear and stuff we should have one GameMagager at all times
        }
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
        
        // We'll need save score and/or gear logic here
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

}
