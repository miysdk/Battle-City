using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{

    #region UI Screens
    [Header("UI Screens")]
    public GameObject gameOver;
    public GameObject gameWon;
    public GameObject pause;
    #endregion

    #region Stats UI Elements
    [Header("Stats UI Elements")]
    public TextMeshProUGUI score;
    public TextMeshProUGUI totalScore;
    public TextMeshProUGUI lives;
    public TextMeshProUGUI enemies;
    #endregion

    GameController gc;
    PlayerController pc;

    private void Start() {
        gc = FindObjectOfType<GameController>();
    }

    private void Update()
    {
        if(pc is null || pc.entity is null){
            pc = FindObjectOfType<PlayerController>();
            return;
        }

        score.text = pc.score.ToString();
        lives.text = $"x{pc.entity.lives}";

        if((pc.entity.lives == 0 || gc.townhall == null) && !gameOver.activeSelf)
        {
            gc.Pause();
            gameOver.SetActive(true);
        }

        if(gc is not null)
        {
            enemies.text = $"x{gc.totalEnemies + gc.spawnedEnemies.Count}";
            if(gc.spawnedEnemies.Count == 0 && gc.totalEnemies == 0 && !gameWon.activeSelf){
            totalScore.text = $"Total Score: {pc.score}";
            gc.Pause();
            gameWon.SetActive(true);
            }
        }
       
    }
}
