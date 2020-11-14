using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{

    public delegate void MinigameEnded(Result result);
    public MinigameEnded OnMinigameEnded;


    public void StartMinigame()
    {

    }

    public void EndMinigame(Result result)
    {
        if (OnMinigameEnded != null)
            OnMinigameEnded(result);
    }
}
