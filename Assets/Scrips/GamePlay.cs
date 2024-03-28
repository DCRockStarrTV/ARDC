using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class GamePlay : MonoBehaviour
{
    #region START

    private bool hasGameFinished;

    public static GamePlay Instance;

    private void Awake()
    {
        Instance = this;

        hasGameFinished = false;
        GameManager.Instance.IsInitialized = true;

        score = 0;
        currentLevel = 0;

        scoreSpeed = levelSpeed[currentLevel];

        for (int i = 0; i < 8; i++)
        {
            SpawnObstacle();
        }
    }

    #endregion

    #region SCORE

    private float score;
    private float scoreSpeed;
    private int currentLevel;
    [SerializeField] private List<int> levelSpeed, levelMax;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private float spawnX, spawnY;

    private void Update()
    {
        if (hasGameFinished) return;

        score += scoreSpeed * Time.deltaTime;

        scoreText.text = ((int)score).ToString();

        if (score > levelMax[Mathf.Clamp(currentLevel + 1, 0, levelMax.Count - 1)])
        {
            SpawnObstacle();
            currentLevel = Mathf.Clamp(currentLevel + 1, 0, levelMax.Count - 1);
            scoreSpeed = levelSpeed[currentLevel];
        }
    }

    private void SpawnObstacle()
    {
        Vector3 spawnPos = new Vector3(Random.Range(spawnX, spawnX), Random.Range(spawnY, spawnY), 0f);
        RaycastHit2D hit = Physics2D.CircleCast(spawnPos, 1f, Vector2.zero);
        bool canSpawn = hit;

        while (canSpawn)
        {
            spawnPos = new Vector3(Random.Range(spawnX, spawnX), Random.Range(spawnY, spawnY), 0f);
            hit = Physics2D.CircleCast(spawnPos, 1f, Vector2.zero);
            canSpawn = hit;
        }

        Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
    }

    #endregion

    #region GAME_OVER

    [SerializeField] private AudioClip _loseClip;

    public void GameEnded()
    {
        
        hasGameFinished = true;
        GameManager.Instance.CurrentScore = (int)score;
        //StartCoroutine(GameOver());
    }

    private IEnumerable GameOver()
    {
        yield return new WaitForSeconds(2f);
        GameManager.Instance.GoToMainMenu();
    }

    #endregion
}