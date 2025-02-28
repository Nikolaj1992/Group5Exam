using UnityEngine;

public class MainMenu : MonoBehaviour
{
    
    public void QuitGame()
    {
        Debug.Log("Game was terminated");
        Application.Quit();
    }
    
}
