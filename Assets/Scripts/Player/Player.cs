using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int _speed = 2;
    private Rigidbody _rigidBody1, _rigidBody2;
    private Animator _animator1, _animator2;
    [SerializeField]
    private int _playerNumber = 1;
    public GameObject bombPrefab, clusterBombPrefab;
    [SerializeField]
    private PowerUp[] _powerUp;
    public bool isClusterPowerUpActive;
    
    private void Awake()
    {
        _rigidBody1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<Rigidbody>();
        _animator1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<Animator>();

        _rigidBody2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Rigidbody>();
        _animator2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Animator>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_playerNumber == 1)
        {
            Player1Movement();
        }

        else
        {
            Player2Movement();
        }
    }

    void Player1Movement()
    {
        if(Input.GetKey(KeyCode.W))
        {
           _rigidBody1.MovePosition(transform.position + (transform.forward * _speed * Time.deltaTime));
           _rigidBody1.velocity = new Vector3(_rigidBody1.velocity.x, _rigidBody1.velocity.y, _speed * Time.deltaTime);
           transform.rotation = Quaternion.Euler(0f, 180f, 0f);
           _animator1.SetBool("Run", true);
        }

        else
        {
            _animator1.SetBool("Run", false);
        }

        if(Input.GetKey(KeyCode.S))
        {
            _rigidBody1.MovePosition(transform.position + (transform.forward * _speed * Time.deltaTime));
           _rigidBody1.velocity = new Vector3(_rigidBody1.velocity.x, _rigidBody1.velocity.y, -_speed * Time.deltaTime);
           transform.rotation = Quaternion.Euler(0f, 0f, 0f);
           _animator1.SetBool("Run", true);
        }

        if(Input.GetKey(KeyCode.A))
        {
            _rigidBody1.MovePosition(transform.position + (transform.forward * _speed * Time.deltaTime));
           _rigidBody1.velocity = new Vector3(-_speed * Time.deltaTime, _rigidBody1.velocity.y, _rigidBody1.velocity.z);
           transform.rotation = Quaternion.Euler(0f, -90f, 0f);
           _animator1.SetBool("Run", true);
        }

        if(Input.GetKey(KeyCode.D))
        {
            _rigidBody1.MovePosition(transform.position + (transform.forward * _speed * Time.deltaTime));
           _rigidBody1.velocity = new Vector3(_speed * Time.deltaTime, _rigidBody1.velocity.y, _rigidBody1.velocity.z);
           transform.rotation = Quaternion.Euler(0f, 90f, 0f);
           _animator1.SetBool("Run", true);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(isClusterPowerUpActive == true)
            {
                Instantiate(clusterBombPrefab, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), clusterBombPrefab.transform.rotation);
            }

            if(isClusterPowerUpActive == false)
            {
                Instantiate(bombPrefab, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), bombPrefab.transform.rotation);
            }
        }
    }

    void Player2Movement()
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
           _rigidBody2.MovePosition(transform.position + (transform.forward * _speed * Time.deltaTime));
           _rigidBody2.velocity = new Vector3(_rigidBody2.velocity.x, _rigidBody2.velocity.y, _speed * Time.deltaTime);
           transform.rotation = Quaternion.Euler(0f, 0f, 0f);
           _animator2.SetBool("Run", true);
        }

        else
        {
            _animator2.SetBool("Run", false);
        }

        if(Input.GetKey(KeyCode.DownArrow))
        {
            _rigidBody2.MovePosition(transform.position + (transform.forward * _speed * Time.deltaTime));
           _rigidBody2.velocity = new Vector3(_rigidBody2.velocity.x, _rigidBody2.velocity.y, -_speed * Time.deltaTime);
           transform.rotation = Quaternion.Euler(0f, 180f, 0f);
           _animator2.SetBool("Run", true);
        }

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            _rigidBody2.MovePosition(transform.position + (transform.forward * _speed * Time.deltaTime));
           _rigidBody2.velocity = new Vector3(-_speed * Time.deltaTime, _rigidBody2.velocity.y, _rigidBody2.velocity.z);
           transform.rotation = Quaternion.Euler(0f, -90f, 0f);
           _animator2.SetBool("Run", true);
        }

        if(Input.GetKey(KeyCode.RightArrow))
        {
            _rigidBody2.MovePosition(transform.position + (transform.forward * _speed * Time.deltaTime));
           _rigidBody2.velocity = new Vector3(_speed * Time.deltaTime, _rigidBody2.velocity.y, _rigidBody2.velocity.z);
           transform.rotation = Quaternion.Euler(0f, 90f, 0f);
           _animator2.SetBool("Run", true);
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(isClusterPowerUpActive == true)
            {
                Instantiate(clusterBombPrefab, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), clusterBombPrefab.transform.rotation);
            }

            if(isClusterPowerUpActive == false)
            {
                Instantiate(bombPrefab, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), bombPrefab.transform.rotation);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "RangePowerUp")
        {
            _powerUp[0].ActivateRangePowerUp();
        }

        if(other.tag == "ClusterPowerUp")
        {
            isClusterPowerUpActive = true;
            if(isClusterPowerUpActive == true)
            {
                StartCoroutine(_powerUp[1].ActivateClusterPowerUp());
            }
            Invoke("DeactivateClusterPowerUp", 15.0f);
        } 
    }

    void DeactivateClusterPowerUp()
    {
        isClusterPowerUpActive = false;
    }
}
