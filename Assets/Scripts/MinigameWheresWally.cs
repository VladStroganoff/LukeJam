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
            Stack<GameObject> queue = new Stack<GameObject>();
            foreach (var item in shuffledList)
                queue.Push(item);

            foreach (var alien in _aliens)
            {
                alien.SetActive(false);
            }

            for (var i = 0; i < AlienEnemyAmount; i++)
            {
                var alien = queue.Pop();
                alien.SetActive(true);
            }

            foreach (var alien in _aliens)
            {
                if (!alien.activeInHierarchy)
                    DestroyImmediate(alien);
            }
        }

        void Update()
        {
            if (_aliens.Count == 0)
                Win();
        }

        void Win()
        {
            GetComponent<MinigameManager>().EndMinigame(Result.Win);
        }
    }
}