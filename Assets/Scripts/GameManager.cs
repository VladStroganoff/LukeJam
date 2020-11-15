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

    public Object HubWorldScene;

    public List<Object> MinigameScenes;

    private int MinigameIterator = 0;

    public string ActiveMinigame;
    // int score
    // int difficulty

    public int PlayerHealth = 5;

    private void Awake()
    {
        if(Instance)
        {
            Debug.LogError("Why are there two game managers");
            return;
        }
        else
        {
            Instance = this;
        }

        SceneManager.LoadSceneAsync(HubWorldScene.name, LoadSceneMode.Additive);

    }

    public void StartMinigames()
    {
        SceneManager.UnloadSceneAsync(HubWorldScene.name);
        LoadMiniGame(MinigameScenes[MinigameIterator]);
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
        if (MinigameIterator >= MinigameScenes.Count)
            MinigameIterator = 0; //Loop
        LoadMiniGame(MinigameScenes[MinigameIterator]);
    }

    private void LoadMiniGame(Object minigameScene)
    {
        //hide the scene somehow
        if(!string.IsNullOrEmpty(ActiveMinigame))
            SceneManager.UnloadSceneAsync(ActiveMinigame);
        ActiveMinigame = minigameScene.name;
        SceneManager.LoadSceneAsync(minigameScene.name, LoadSceneMode.Additive);
        //unhide the scene somehow
    }
}
