using UnityEngine;

public class MainMenu : MonoBehaviour
{
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    public void QuitGame()
    {
        Debug.Log("Game was terminated");
        Application.Quit();
    }
    
}
