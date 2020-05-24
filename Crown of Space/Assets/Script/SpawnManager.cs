using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject [] _powerUp;
    [SerializeField]
    private float _waitTimeSpawnPowerUp = 10f;
    [SerializeField]
    private bool _spawnearPowerUp;
    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private float _waitTimeSpawnEnemy=5f;
    [SerializeField]
    private bool _spawnearEnemy;

    // Start is called before the first frame update
    void Start()
    {
        _spawnearEnemy = false;
        _spawnearPowerUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3);

        while (_spawnearEnemy == false)
        {
            GameObject newEnemy = Instantiate(_enemy);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_waitTimeSpawnEnemy);
        }

       
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(3);

        while (_spawnearEnemy == false)
        {
            if (_spawnearPowerUp)
            {
                int randomPowerUp = Random.Range(0, 3);
                Instantiate(_powerUp[randomPowerUp]);
            }

            _spawnearPowerUp = true;
            _waitTimeSpawnPowerUp = Random.Range(6, 13);
            yield return new WaitForSeconds(_waitTimeSpawnPowerUp);
        }
    } 

    public void OnDeathPlayer()
    {
        _spawnearEnemy = true;
    }

    public void startSpawining()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }
}
