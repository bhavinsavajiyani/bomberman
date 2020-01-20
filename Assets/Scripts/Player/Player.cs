using System.Security;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int _speed = 2;
    private Rigidbody _rigidBody1, _rigidBody2;
    private Animator _animator1, _animator2;
    public int _playerNumber = 1;
    public GameObject bombPrefab, clusterBombPrefab;
    [SerializeField]
    private PowerUp[] _powerUp;
    public bool isClusterPowerUpActive, dead = false;
    private static int _coinCount1, _coinCount2;
    [HideInInspector]
    public static int P1Score, P2Score;
    public UIManager uIManager;
    public AudioSource audioSource;
    public static int highScore;
    
    private void Awake()
    {
        _rigidBody1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<Rigidbody>();
        _animator1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<Animator>();

        _rigidBody2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Rigidbody>();
        _animator2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Animator>();

        audioSource = GameObject.Find("CollectiblesHolder").GetComponent<AudioSource>();
        _coinCount1 = 0;
        _coinCount2 = 0;

        Time.timeScale = 1;
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

        CalculateHighScore();
        DeclareWinner();
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

        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
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

        if(other.tag == "Coin" && _playerNumber == 1)
        {
            _coinCount1++;
            P1Score = _coinCount1;
            uIManager.P1Score.text = "P1 SCORE: " + P1Score.ToString();
            audioSource.PlayOneShot(audioSource.clip);
            Destroy(other.gameObject, 0.1f);
              
        }

        else if(other.tag == "Coin" && _playerNumber > 1)
        {
            _coinCount2++;
            P2Score = _coinCount2;
            uIManager.P2Score.text = "P2 SCORE: " + P2Score.ToString();
            audioSource.PlayOneShot(audioSource.clip);
            Destroy(other.gameObject, 0.1f);
        }
    }

    void DeactivateClusterPowerUp()
    {
        isClusterPowerUpActive = false;
    }

    public void CalculateHighScore()
    {
        if(_coinCount1 > _coinCount2)
        {
            highScore = _coinCount1;
            SetHighScore();
            uIManager.highScoreText.text = "HIGH SCORE: P1 = " + highScore.ToString();
        }

        if(_coinCount1 < _coinCount2)
        {
            highScore = _coinCount2;
            SetHighScore();
            uIManager.highScoreText.text = "HIGH SCORE: P2 = " + highScore.ToString();
        }

        if(_coinCount1 == _coinCount2)
        {
            highScore = _coinCount2;
            SetHighScore();
            uIManager.highScoreText.text = "HIGH SCORE: P1 = P2 = " + highScore.ToString();
        }
    }

    public void DeclareWinner()
    {
        if(uIManager.timer == 0 &&
            GameObject.FindGameObjectWithTag("Player1").activeInHierarchy == true &&
                GameObject.FindGameObjectWithTag("Player2").activeInHierarchy == true)
        {
            if(_coinCount1 > _coinCount2)
            {
                uIManager.winnerText.text = "WINNER: PLAYER 1";
            }

            if(_coinCount1 < _coinCount2)
            {
                uIManager.winnerText.text = "WINNER: PLAYER 2";
            }

            if(_coinCount1 == _coinCount2)
            {
                uIManager.winnerText.text = "DRAW";
            }
        }
    }

    public void SetHighScore()
    {
        if(highScore > PlayerPrefs.GetInt("HIGH SCORE: ", 0))
        {
            PlayerPrefs.SetInt("HIGH SCORE: ", highScore);
        }
        
    }
}
