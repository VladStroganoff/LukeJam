using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Enemy : MonoBehaviour
    {
        public GameObject player;
        public GameObject enemyObj;
        public Rigidbody playerBody;
        public Rigidbody enemyBody;
        public Vector3 Movement = new Vector3(0.0f, 0.01f, 0.0f);
        public Vector3 enemyPos;
        public Vector3 playerPos;
        public float maxHeight;
        public enum STATE
        {
            Hidden,
            Close,
            Action,
            Retreat
        }

        public STATE stateEnum;
        // Use this for initialization
        void Start()
        {
            if (player != null)
            {
                playerBody = player.GetComponent<Rigidbody>();
            }
            if (enemyObj != null)
            {
                enemyBody = enemyObj.GetComponent<Rigidbody>();
            }
        }

        // Update is called once per frame
        void Update()
        {
            playerPos = playerBody.position;
            enemyPos = enemyBody.position;
            float dist = Vector3.Distance(playerPos, enemyPos);
            stateEnum = STATE.Retreat;
            if (dist > 5)
            {
                stateEnum = STATE.Close;
            }
            else if(dist < 5)
            {
                stateEnum = STATE.Action;
            }
            
            switch(stateEnum)
            {
                case STATE.Retreat:
                    
                    enemyBody.AddForceAtPosition(Movement, enemyPos);
                    break;
                case STATE.Close:
                    if (enemyPos.y > maxHeight)
                    {
                        Debug.Log("going down");
                        enemyBody.AddForceAtPosition(Movement.normalized, enemyPos);
                    }
                    else
                    {
                        Debug.Log("going down");
                        enemyBody.AddForceAtPosition(-Movement.normalized, enemyPos);
                    }
                    break;
                case STATE.Action:
                    Debug.Log("action");
                    break;
                     
            }
        }
    }
}