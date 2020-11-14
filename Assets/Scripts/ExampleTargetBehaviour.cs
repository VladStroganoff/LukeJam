using UnityEngine;

namespace Assets.Scripts
{
    public class ExampleTargetBehaviour : MonoBehaviour, ITarget
    {
        public void Hit()
        {
            Debug.Log("Ouch! I cannot beleive you shot me!");
        }
    }
}