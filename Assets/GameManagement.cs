using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    private bool gameEnded = false;
    void Update()
    {
        if (gameEnded)
        {
            return;
        }

        if (PlayerStats.Lives<=0)
        {
            EndGame();
        }
        
    }

    private void EndGame()
    {
        gameEnded = true;
    }
}
