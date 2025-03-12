using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class GroundSlash : MonoBehaviour
{
    public float speed = 30;
    public float slowDownRate = 0.01f;
    public float detectingDistance = 0.1f;
    public float destroyDelay = 5;
    
    private Rigidbody rb;
    private bool stopped;

    private Collider colliderChild;
    private List<GameObject> targets = new List<GameObject>();
    
    void Start()
    {
        colliderChild = GetComponentInChildren<MeshCollider>();
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        if (GetComponent<Rigidbody>() != null)
        {
            rb = GetComponent<Rigidbody>();
            StartCoroutine(SlowDown());
        }
        else
        {
            Debug.Log("NO RB");
            Destroy(gameObject, destroyDelay);
        }
    }
    
    void FixedUpdate()
    {
        if (!stopped)
        {
            RaycastHit hit;
            Vector3 distance = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            if (Physics.Raycast(distance, transform.TransformDirection(-Vector3.up), out hit, detectingDistance))
            {
                transform.position = new Vector3(this.transform.position.x, hit.point.y, this.transform.position.z);
            }
            else
            {
                transform.position = new Vector3(this.transform.position.x, 0, this.transform.position.z);
            }
            Debug.DrawRay(distance, transform.TransformDirection(-Vector3.up * detectingDistance), Color.red);
        }
    }

    IEnumerator SlowDown()
    {
        float t = 1;
        while (t > 0)
        { 
            rb.linearVelocity = Vector3.Lerp(Vector3.zero, rb.linearVelocity, t);
            t -= slowDownRate;
            yield return new WaitForSeconds(0.1f);
        }
        stopped = true;
        Destroy(gameObject, destroyDelay);
    }
    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision with: " + other.name);
        if (!targets.Contains(other.gameObject))
        {
            targets.Add(other.gameObject);
            PlayerAttackInput.ApplyEffectsToTarget(gameObject.transform.forward, rb.linearVelocity.magnitude/2, other.gameObject);
        }
    }
}
