using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Explosion : MonoBehaviour
{
    public float power = 10.0f;
    public float radius = 1.0f;
    public float upForce = 1.0f;
    public GameObject explosionPrefab, bombPrefab;

    private void Awake()
    {
        Time.timeScale = 1;
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        Invoke("Detonate", 4.0f);
    }

    void Detonate()
    {
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Vector3 explosionPosition = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius);
        foreach(Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(power, explosionPosition, radius, upForce, ForceMode.Impulse);
                if(rb.tag == "Player1")
                {
                    Time.timeScale = 0f;
                    GameObject.Find("UIManager").GetComponent<UIManager>().winnerText.text = "WINNER: PLAYER 2";

                    GameObject.Find("UIManager").GetComponent<UIManager>().gameOverCanvas.SetActive(true);
                }

                if(rb.tag == "Player2")
                {
                    Time.timeScale = 0f;
                    GameObject.Find("UIManager").GetComponent<UIManager>().winnerText.text = "WINNER: PLAYER 1";
                    
                    GameObject.Find("UIManager").GetComponent<UIManager>().gameOverCanvas.SetActive(true);
                }
                
                rb.gameObject.SetActive(false);
            }
        }
        Destroy(gameObject, 5.05f);
    }
}
