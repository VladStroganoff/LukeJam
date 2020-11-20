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

        private void Start()
        {
            _rigidBody = gameObject.GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (transform.position.y < GroundHeight)
                GameOver();
        }

        private void GameOver()
        {
            _gameOver = true;
            _rigidBody.isKinematic = true;

            transform.position.Set
            (
                newX: transform.position.x,
                newY: GroundHeight + 0.1f,
                newZ: transform.position.z
            );

            if (FlipCounter < WinScore)
                Lose();

            Win();
        }

        private void Win()
        {

        }

        private void Lose()
        {

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
            _rigidBody.AddForce
            (
                x: Random.Range(-0.05f, 0.05f),
                y: 1f * ForceMultiplier,
                z: Random.Range(-0.05f, 0.05f), ForceMode.Impulse
            );
        }
    }
}