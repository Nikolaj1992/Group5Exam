using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadoutMenu : MonoBehaviour
{
    
    public LoadoutSlot weaponSlot;
    public LoadoutSlot armorSlot;

    private GameManager gameManager;
    
    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        weaponSlot.OnItemSelect += (item) => LoadoutManager.Instance.SetWeapon(item);
        armorSlot.OnItemSelect += (item) => LoadoutManager.Instance.SetArmor(item);
    }

    public void StartGame()
    {
        // SceneManager.LoadSceneAsync("SampleScene");
        gameManager.StartNewRound();
    }
    
}
