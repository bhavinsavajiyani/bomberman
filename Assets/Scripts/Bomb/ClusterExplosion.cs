using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClusterExplosion : MonoBehaviour
{
    public float power = 10.0f;
    public float radius = 1.0f;
    public float upForce = 1.0f;
    public GameObject explosionPrefab, bombPrefab;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        Invoke("Detonate", 4.0f);
    }

    void Detonate()
    {
        Instantiate(explosionPrefab, transform.position, transform.rotation);

        Instantiate(bombPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z + 1.0f), transform.rotation);
        Instantiate(bombPrefab, new Vector3(transform.position.x + 1.0f, transform.position.y, transform.position.z - 0.2f), transform.rotation);
        Instantiate(bombPrefab, new Vector3(transform.position.x - 1.0f, transform.position.y, transform.position.z - 0.2f), transform.rotation);
        Vector3 explosionPosition = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius);
        foreach(Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(power, explosionPosition, radius, upForce, ForceMode.Impulse);
                if(rb.tag == "Player1" || rb.tag == "Player2")
                {
                    // Make changes here. Add Game Over Panel!
                    SceneManager.LoadScene(1);
                }
            }
        }

        Destroy(gameObject, 5.05f);
    }
}
