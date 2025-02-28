using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float coneAngle = 45f;  // Angle of the cone in degrees
    public float range = 10f;      // Detection range
    public LayerMask detectionMask; // Mask for filtering objects (set in Inspector)
    
    private List<GameObject> detectedObjects = new List<GameObject>();
    public GameObject closestObject; // The closest object to the player (public for access from other scripts)
    
    private Camera playerCamera;
    
    void Start()
    {
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    
    void Update()
    {
        if (playerCamera != null)
        {
            DetectInteractables();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (detectedObjects.Count > 0 && closestObject != null)
            {
                InteractWithClosestObject();
            }
        }
    }

    void InteractWithClosestObject()
    {
        if (closestObject.layer != LayerMask.NameToLayer("Interactable")) return;
        GameObject rootParent = closestObject.transform.root.gameObject;
        // Debug.Log(rootParent.name);
        Animator animator = closestObject.GetComponentInChildren<Animator>();
        if (!animator) return;
        switch (rootParent.name)
        {
            case "LeverPuzzle":
                // Debug.Log("Successfully detected lever puzzle"); 
                animator.SetTrigger("LeverPull");
                animator.SetBool("LeverDown", !animator.GetBool("LeverDown"));
                rootParent.GetComponent<LeverPuzzleScript>().LeverPulled();
                break;
            default:
                Debug.Log("default");
                break;
        }
    }

    void DetectInteractables()
    {
        Vector3 cameraPosition = playerCamera.transform.position;
        Vector3 cameraForward = playerCamera.transform.forward;

        // Get all colliders within range
        Collider[] colliders = Physics.OverlapSphere(cameraPosition, range, detectionMask);
        
        // Saves the previous closestObject, so that its outline can be disabled
        GameObject previousClosestObject = closestObject;
        
        foreach (Collider col in colliders)
        {
            Vector3 directionToObject = (col.transform.position - cameraPosition).normalized;

            // Check if object is within the cone angle
            float angle = Vector3.Angle(cameraForward, directionToObject);
            if (angle <= coneAngle / 2)
            {
                // If it's within the cone and not already in the list, add it
                if (!detectedObjects.Contains(col.gameObject))
                {
                    detectedObjects.Add(col.gameObject);
                }
            }
        }

        // Find the closest object
        float closestDistance = float.MaxValue; // Initialize to a very large value
        closestObject = null; // Reset the closest object

        foreach (GameObject obj in detectedObjects)
        {
            Vector3 directionToObject = (obj.transform.position - cameraPosition);
            float distance = directionToObject.magnitude; // Get distance to the object

            if (distance < closestDistance) // If this object is closer than the previously closest one
            {
                closestDistance = distance;
                closestObject = obj; // Set the closest object to this one
            }
        }

        // If the closest object is not null and different from the previous closest object, update outlines
        if (previousClosestObject != closestObject)
        {
            // Disable the outline on the previous closest object if it's no longer the closest
            if (previousClosestObject != null)
            {
                Outline previousOutlineScript = previousClosestObject.GetComponent<Outline>();
                if (previousOutlineScript != null)
                {
                    previousOutlineScript.enabled = false;
                }
            }

            // Enable the outline on the new closest object
            if (closestObject != null)
            {
                Outline outlineScript = closestObject.GetComponent<Outline>();
                if (outlineScript != null)
                {
                    outlineScript.enabled = true;
                }
            }
        }

        if (closestObject != null && closestObject.layer != LayerMask.NameToLayer("Interactable"))
        {
            Outline outlineScript = closestObject.GetComponent<Outline>();
            if (outlineScript != null)
            {
                outlineScript.enabled = false;
            }
        }

        // Remove objects that are no longer within the cone or range
        List<GameObject> objectsToRemove = new List<GameObject>();

        foreach (GameObject obj in detectedObjects)
        {
            Vector3 directionToObject = (obj.transform.position - cameraPosition).normalized;
            float angle = Vector3.Angle(cameraForward, directionToObject);

            // If the object is no longer within the cone or range, add it to the removal list
            if (angle > coneAngle / 2 || Vector3.Distance(cameraPosition, obj.transform.position) > range)
            {
                objectsToRemove.Add(obj);
            }
        }

        // Remove objects that are no longer within range or in the cone
        foreach (GameObject obj in objectsToRemove)
        {
            detectedObjects.Remove(obj);
        }
    }
}
