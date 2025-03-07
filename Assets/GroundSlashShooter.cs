using UnityEngine;

public class GroundSlashShooter : MonoBehaviour, IAttack
{
    public GameObject projectile;
    public float fireRate = 4;
    
    private Vector3 destination;

    private float timeToFire;
    private GroundSlash groundSlashScript;
    
    public void ExecuteAttack(Transform muzzle, int i)
    {
        // only here to please the interface
    }
    
    public void ExecuteAttack(Transform muzzle)
    {
        // Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        Ray ray = new Ray(muzzle.position, muzzle.forward);
        destination = ray.GetPoint(1000);
        InstantiateProjectile(muzzle);
    }

    void InstantiateProjectile(Transform muzzle)
    {
        var projectileObj = Instantiate(projectile, muzzle.position, muzzle.rotation) as GameObject;

        groundSlashScript = projectileObj.GetComponent<GroundSlash>();
        RotateToDestination(projectileObj, destination, true);
        projectileObj.GetComponent<Rigidbody>().linearVelocity = muzzle.forward * groundSlashScript.speed;
    }

    void RotateToDestination(GameObject obj, Vector3 destination, bool onlyY)
    {
        var direction = destination - obj.transform.position;
        var rotation = Quaternion.LookRotation(direction);

        if (onlyY)
        {
            rotation.x = 0;
            rotation.z = 0;
        }
        obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, rotation, 1);
    }
}
