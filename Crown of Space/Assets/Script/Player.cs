using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShootPrefab;
    [SerializeField]
    private float _tripleShootDuration = 5f;
    [SerializeField]
    private float _tripleCan = 0f;
    [SerializeField]
    private float _speed=3;
    [SerializeField]
    private float _horizontalInput;
    [SerializeField]
    private float _verticalInput;
    [SerializeField]
    private float _fireRate = .5f;
    [SerializeField]
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _tripleShoot = false;
    [SerializeField]
    private GameObject _shield;
    [SerializeField]
    private int _score;
    [SerializeField]
    private UiManager _uiManager;
    [SerializeField]
    private GameObject right_Engine;
    [SerializeField]
    private GameObject left_Engine;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _laserSoundClip;


    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        right_Engine.gameObject.SetActive(false);
        left_Engine.gameObject.SetActive(false);

        transform.position = new Vector3(0,0,0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("UI_Manager_Canvas").GetComponent<UiManager>();

        if (_uiManager == null)
        {
            Debug.Log("error Ui Manager missing, the Computer will explote in T minus 5");
        }
        if (_audioSource == null)
        {
            Debug.Log("error Audio Source is  missing");
        }
        else
        {
            _audioSource.clip = _laserSoundClip;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }


    }

    void CalculateMovement(){

        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(_horizontalInput, _verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.7f, .25f));

        if (transform.position.x < -11.2)
        {
            transform.position = new Vector3(11.11f, transform.position.y, 0);
        }
        else if (transform.position.x > 11.2)
        {
            transform.position = new Vector3(-11.11f, transform.position.y, 0);
        }

    }
    void FireLaser()
    {
        _canFire = _fireRate + Time.time;
        _audioSource.Play();


        /*if (_tripleShoot && Time.time < _tripleCan )
        {
            Instantiate(_tripleShootPrefab, transform.position + new Vector3(0, .8f, 0), Quaternion.identity);
        }*/
        if (_tripleShoot)
        {
            Instantiate(_tripleShootPrefab, transform.position + new Vector3(0, .8f, 0), Quaternion.identity);
            
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, .8f, 0), Quaternion.identity);
            _tripleShoot = false;
            
        }


        //Instantiate(_laserPrefab, transform.position + new Vector3(0, .8f, 0), Quaternion.identity);
    }

    public void Damage()
    {
        if (_shield.GetComponent<SpriteRenderer>().enabled)
        {
            _shield.GetComponent<SpriteRenderer>().enabled = false;
            return;
        }

        _lives--;
        _uiManager.updateLives(_lives);

        if (_lives == 2)
        {
            right_Engine.gameObject.SetActive(true);

        }else if (_lives == 1)
        {
            left_Engine.gameObject.SetActive(true);

        }else if (_lives < 1)
        {
            if(_spawnManager!=null)
            _spawnManager.OnDeathPlayer();
            _uiManager.GameOverActive();
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

    }

    public void ActivarTripleShoot()
    {
        _tripleShoot = true;
        StartCoroutine(tripleShootCourutine());
        /*
        _tripleShoot = true;
        _tripleCan = Time.time + _tripleShootDuration;
        */
    }

    public IEnumerator tripleShootCourutine()
    {
        

        yield return new WaitForSeconds(5f);
        _tripleShoot = false;
    }

    public void ActivarSpeed()
    {
        StartCoroutine(SpeedCourutine());
        _speed += _speed;
    }

    public IEnumerator SpeedCourutine()
    {
        
        yield return new WaitForSeconds(5f);
        _speed = _speed/2;
    }

    public void ActivarShield()
    {
        _shield.GetComponent<SpriteRenderer>().enabled = true;

    }

    public void AddToScore(int enemyValue)
    {
        Debug.Log("1");
        _score += enemyValue;
        _uiManager.upScore(_score);
    }

}
