using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steroid : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private SpawnManager _spawnManager;

    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.Log("error Ui Manager missing, the Computer will explote in T minus 5");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, 45 * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Laser")
        {
            GameObject explosions =  Instantiate(explosion, transform.position, Quaternion.identity);
            _spawnManager.startSpawining();
            Destroy(explosions, 3f);
            Destroy(collision.gameObject);
            Destroy(this.gameObject, .4f);
        }
    }
}
