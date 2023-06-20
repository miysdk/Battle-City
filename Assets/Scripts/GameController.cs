using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int totalEnemies = 20;
    public List<GameObject> spawnedEnemies = new List<GameObject>();
    public bool isPause = false;

    public GameObject townhall;

    private void Start()
    {
        townhall = GameObject.FindGameObjectWithTag("Base");
    }

    public void Reload(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Pause(){
        Time.timeScale = 0;
    }

    public void Unpause(){
        Time.timeScale = 1;
    }
}
