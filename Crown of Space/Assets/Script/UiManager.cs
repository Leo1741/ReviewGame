using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _restartText;
    [SerializeField]
    private TMP_Text _gameOverText;
    [SerializeField]
    private TMP_Text _scoreText;
    [SerializeField]
    private Sprite[] _livesSprites;
    [SerializeField]
    private Image _livesImages;
    [SerializeField]
    private GameManager gameManager;

    private bool flicker;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _scoreText.SetText("SCORE : 000");
    }

    public void upScore(int score)
    {
        Debug.Log("3");
        _scoreText.SetText("SCORE : "+score);
    }

    public void updateLives(int actualLives)
    {
        
        _livesImages.sprite = _livesSprites[actualLives];
    }

    public void GameOverActive() {

        gameManager.gameOverExecuted();
        _gameOverText.gameObject.SetActive(true);
        flicker = true ;
        StartCoroutine(flickering());
    }

    IEnumerator flickering()
    {
        while (true)
        {
            float flickerTime = 1.25f;

            if (flicker == false)
            {
                flickerTime = flickerTime / 2;
            }
            _gameOverText.gameObject.SetActive(flicker);
            _restartText.gameObject.SetActive(flicker);
            flicker = !flicker;
            
            yield return new WaitForSeconds(flickerTime);
        }
        
    }
}
