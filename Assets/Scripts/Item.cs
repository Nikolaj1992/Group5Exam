using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Loadout/Item")]
public class Item : ScriptableObject
{

    public string itemName;
    public Sprite itemIcon;
    public bool isWeapon;

    // [SerializeField] private string itemName;
    // [SerializeField] private Sprite sprite;
    // [SerializeField] private bool isWeapon;
    //
    // private LoadoutManager loadoutManager;
    //
    // private void Start()
    // {
    //     loadoutManager = GameObject.Find("LoadoutMenu").GetComponent<LoadoutManager>();
    // }
    //
    // // private void OnCollisionEnter(Collision other)
    // // {
    // //     if (other.gameObject.tag == "Player")
    // //     {
    // //         loadoutManager.AddItem(itemName, sprite);
    // //         Destroy(gameObject);
    // //     }
    // // }
    //
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         if (isWeapon)
    //             LoadoutManager.Instance.SetWeapon(itemName, sprite);
    //         else
    //             LoadoutManager.Instance.SetArmor(itemName, sprite);
    //
    //         Destroy(gameObject); // Simulate pickup
    //     }
    // }

}

