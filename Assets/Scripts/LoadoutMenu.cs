using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadoutMenu : MonoBehaviour
{

    private GameManager gameManager;
    
    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        // SceneManager.LoadSceneAsync("SampleScene");
        gameManager.StartNewRound();
    }
    
}
