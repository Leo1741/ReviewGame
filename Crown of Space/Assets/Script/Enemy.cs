using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private Player _player;
    [SerializeField]
    private int _enemyValue;
    private Animator _anim;
    [SerializeField]
    private AudioSource _audioSource;




    void Start()
    {
        _enemyValue = 11;
        _player = GameObject.Find("Player").GetComponent<Player>();
        _anim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        if (_player == null)
        {
            Debug.Log("error Player missing, the Computer will explote in T minus 8");
        }
        if (_anim == null)
        {
            Debug.Log("error _anim missing, the screen will melt in T minus 10");
        }
        if (_audioSource == null)
        {
            Debug.Log("error Audio Source is  missing");
        }
    }

    private void Awake()
    {
        transform.position = new Vector3(Random.Range(-10,11),7.5f,0);
        StartCoroutine(shootToPlayer());
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.down *_speed * Time.deltaTime);

        if (transform.position.y < -5.5) {
            transform.position = new Vector3(Random.Range(-10, 11), 7.5f, 0);
            StartCoroutine(shootToPlayer());
        }

    }

    private void OnTriggerEnter2D(Collider2D other)  //change to 3d FOR 3D
    {
        
        if(other.tag == "Player")
        {
           _audioSource.Play();
           _speed = 0;
           _player.Damage();
           _anim.SetTrigger("OnEnemyDeath");
           Destroy(this.gameObject, 2.30f);
        }

        if (other.tag == "Laser")
        {
            _audioSource.Play();
            _speed = 0;
            _player.AddToScore(_enemyValue);
            Destroy(other.gameObject);
            _anim.SetTrigger("OnEnemyDeath");
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.30f);
        }
    }

    IEnumerator shootToPlayer()
    {
        yield return new WaitForSeconds(Random.Range(0,3));
        shoot();
    }

    private void shoot()
    {
        GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
        LaserBehavior[] laser = enemyLaser.GetComponentsInChildren<LaserBehavior>();

        for (int i = 0; i < laser.Length; i++)
        {
            laser[i].AssignEnemyLaser();
        }
    }
}
