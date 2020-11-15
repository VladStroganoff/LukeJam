using UnityEngine;

public class ArcadeMachine : MonoBehaviour, Assets.Scripts.ITarget
{
    public void Hit()
    {
        GameManager.Instance.StartMinigames();
    }
}