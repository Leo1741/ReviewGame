using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehavior : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;
    private bool isEnemyLaser=false;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (!isEnemyLaser)
        {
            MoveUp();
        }else
        {
            MoveDown();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && isEnemyLaser==true)
        {
            Player player = other.GetComponent<Player>();
            if (player !=null) {
                player.Damage();
                Destroy(this.gameObject);
            }
        }
    }

    public void DestroyLaser()
    {
        Destroy(this.gameObject);
    }

    public void MoveUp()
    {

        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        if (transform.position.y >= 7.5f)
        {

            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    public void MoveDown()
    {

        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y >= 7.5f)
        {

            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    public void AssignEnemyLaser()
    {
        isEnemyLaser = true;
        transform.tag = "EnemyLaser";
    }

}
