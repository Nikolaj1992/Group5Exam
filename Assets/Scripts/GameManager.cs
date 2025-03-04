using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    private bool gameIsOver = false;

    public void EndRound()
    {
        if (gameIsOver) return;

        gameIsOver = true;
        
        // Could later make some transition or effect here
        SceneManager.LoadSceneAsync("Main Menu");
        
        // Perhaps some score saving and gear saving logic here
    }

    public void StartNewRound()
    {
        gameIsOver = false;
        
        // We should have reset logic here for player position, resetting puzzles ect.

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);     // Reload current scene
    }

}
