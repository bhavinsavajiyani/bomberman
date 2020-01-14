using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject[] powerUp;
    public Explosion explosion;
    public ClusterExplosion clusterExplosion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateRangePowerUp()
    {
        powerUp[0].GetComponent<SpriteRenderer>().enabled = false;
        powerUp[0].GetComponent<AudioSource>().PlayOneShot(powerUp[0].GetComponent<AudioSource>().clip);
        explosion.power = 13.0f;
        explosion.radius = 3.0f;
        explosion.upForce = 2.0f;
        StartCoroutine(TurnOffRangePowerUp());
        Invoke("DeactivateRangePowerUp", 30.0f);
    }

    void DeactivateRangePowerUp()
    {
        explosion.power = 10.0f;
        explosion.radius = 1.0f;
        explosion.upForce = 1.0f;
        Destroy(this.gameObject);
    }

    IEnumerator TurnOffRangePowerUp()
    {
        yield return new WaitForSeconds(0.6f);
        powerUp[0].SetActive(false);
    }

    public IEnumerator ActivateClusterPowerUp()
    {
        powerUp[1].GetComponent<SpriteRenderer>().enabled = false;
        powerUp[1].GetComponent<AudioSource>().PlayOneShot(powerUp[1].GetComponent<AudioSource>().clip);
        yield return new WaitForSeconds(0.6f);
        powerUp[1].SetActive(false);
    }
}
