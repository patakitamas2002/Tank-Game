using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField] PlayerAssets playerAssets;
    [SerializeField] int enemiesLeft = 0;

    bool endGame = false;
    // Start is called before the first frame update

    public void AddEnemy()
    {
        enemiesLeft++;
    }
    public void EnemyDefeated()
    {
        enemiesLeft--;
        if (enemiesLeft == 0)
        {
            endGame = true;
            playerAssets.pauseMenu.WinGame();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (endGame)
            Time.timeScale -= Time.deltaTime;
    }

    public void LoseGame()
    {
        endGame = true;
        playerAssets.pauseMenu.GetComponent<PauseMenu>().LoseGame();
    }
}
