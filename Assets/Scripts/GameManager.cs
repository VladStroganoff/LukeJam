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

    public static GameManager Instance;

    public List<Scene> Minigames;

    private int MinigameIterator = 0;

    public Scene ActiveMinigame;
    // int score
    // int difficulty

    public int PlayerHealth = 5;

    private void Awake()
    {
        if(Instance)
        {
            Debug.LogError("Why are there two game managers");
        }
        else
        {
            Instance = this;
        }
    }

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

    private void LoadMiniGame(Scene minigameScene)
    {
        //hide the scene somehow
        SceneManager.UnloadSceneAsync(ActiveMinigame);
        ActiveMinigame = minigameScene;
        SceneManager.LoadSceneAsync(minigameScene.name);
        //unhide the scene somehow
    }
}
