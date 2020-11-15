using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class MinigameWheresWally : MonoBehaviour
    {
        private List<GameObject> _aliens = new List<GameObject>();
        public int AlienEnemyAmount = 3;

        // Use this for initialization
        void Start()
        {
            Transform alienListTransform = GameObject.Find("Aliens").transform;

            foreach (Transform child in alienListTransform)
                _aliens.Add(child.gameObject);

            var aliensList = new List<GameObject>();
            aliensList.AddRange(_aliens);
            var rand = new Random();
            var shuffledList = aliensList.OrderBy(x => System.Guid.NewGuid()).ToList();
            foreach (var alien in aliensList)
                Debug.Log(alien.name);
            foreach (var alien in shuffledList)
                Debug.Log(alien.name);

            for (var i = 0; i < AlienEnemyAmount; i++)
            {
            }

            foreach (var alien in _aliens)
            {
                alien.SetActive(false);
            }
        }
    }
}