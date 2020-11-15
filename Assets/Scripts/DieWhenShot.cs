using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class DieWhenShot : MonoBehaviour, ITarget
    {
        public void Hit()
        {
            Destroy(this);
        }
    }
}