using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Result
{
    Win,
    Loss
}

public class GameManager : MonoBehaviour
{
    /*
     * And we want to spawn (start) a minigame
     * wait for it's win/lose condition
     * unspawn it
     * spawn the next one
     * we need a list of minigames
     * */
    public List<GameObject> Minigames;

    private int MinigameIterator = 0;

    public GameObject ActiveMinigame;
    // int score
    // int difficulty

    public int PlayerHealth = 5;
    
    public void StartGame()
    {
        LoadMiniGame(Minigames[MinigameIterator]);
    }

    public void MiniGameFinished(Result miniGameResult)
    {
        switch(miniGameResult)
        {
            case Result.Loss:
                PlayerHealth -= 1;
                break;
            case Result.Win:
                //Whatever I guess
                break;
        }
        NextMiniGame();
    }

    private void NextMiniGame()
    {
        MinigameIterator += 1;
        if (MinigameIterator >= Minigames.Count)
            MinigameIterator = 0; //Loop
        LoadMiniGame(Minigames[MinigameIterator]);
    }

    private void LoadMiniGame(GameObject minigame)
    {
        Destroy(ActiveMinigame);
        ActiveMinigame = minigame;
        Instantiate(minigame, transform);
        var mgManager = minigame.GetComponent<MinigameManager>();
        if (!mgManager)
            Debug.LogError("Minigame Manager not found");
        mgManager.StartMinigame();
        mgManager.OnMinigameEnded += MiniGameFinished;
    }
}
