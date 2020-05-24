using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private float _speed = 3f;

    [SerializeField]
    private enum PowerUpId{Triple, Speed, Shield};
    [SerializeField]
    PowerUpId powerUpType;
    [SerializeField]
    private AudioClip _audioClipPowerUp;



    void Start()
    {

    }

    private void Awake()
    {
        transform.position = new Vector3(Random.Range(-10, 11), 9, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y < -5)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
                AudioSource.PlayClipAtPoint(_audioClipPowerUp, transform.position);
                switch (powerUpType)
                {
                    case PowerUpId.Triple:
                        player.ActivarTripleShoot();                      
                        break;
                    case PowerUpId.Speed:
                        player.ActivarSpeed();                       
                        break;
                    case PowerUpId.Shield:
                        player.ActivarShield();                   
                        break;
                    default:
                        break;
                }
               

            Destroy(this.gameObject);
        }
    }

}
