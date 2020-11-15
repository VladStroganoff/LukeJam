using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{

    public delegate void MinigameEnded(Result result);
    public MinigameEnded OnMinigameEnded;

    public void Start()
    {
        StartMinigame();
    }

    public void StartMinigame()
    {
        //enter minigame initialization code here
    }

    public void EndMinigame(Result result)
    {
        GameManager.Instance.MiniGameFinished(result);
    }
}
