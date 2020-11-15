using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class CoinFlip : MonoBehaviour, ITarget
    {
        public float GroundHeight = 0f;
        private bool _started = false;
        private bool _gameOver = false;
        private Rigidbody _rigidBody;
        public int FlipCounter = 0;
        public int WinScore = 3;
        public float ForceMultiplier = 2f;
        public TextMesh ScoreCounter;

        void Start()
        {
            _rigidBody = gameObject.GetComponent<Rigidbody>();
        }

        void Update()
        {
            if (transform.position.y < GroundHeight)
                GameOver();
        }

        private void GameOver()
        {
            _gameOver = true;
            _rigidBody.isKinematic = true;
            transform.position.Set(transform.position.x, GroundHeight + 0.1f, transform.position.z);
            if (FlipCounter < WinScore)
                Lose();
            else
                Win();
        }

        private void Win()
        {
            Debug.Log("COIN FLIP WIN");
            GetComponent<MinigameManager>().EndMinigame(Result.Win);
        }

        private void Lose()
        {
            Debug.Log("COIN FLIP LOSS");
            GetComponent<MinigameManager>().EndMinigame(Result.Loss);

        }

        public void Hit()
        {
            if (!_started)
                StartFlipping();
            if(!_gameOver)
                Flip();
        }

        private void StartFlipping()
        {
            _started = true;
            _rigidBody.useGravity = true;
        }

        private void Flip()
        {
            FlipCounter++;
            ScoreCounter.text = FlipCounter.ToString();
            _rigidBody.AddForce(Random.Range(-0.05f, 0.05f), 1f * ForceMultiplier, Random.Range(-0.05f, 0.05f), ForceMode.Impulse);
        }
    }
}