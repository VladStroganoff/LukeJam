using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMover : MonoBehaviour
{
    public GameObject player;


    public bool canTurn = true;

    public float moveSpeed = 1.6f;
    public float rotateSpeed = 1.4f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Quaternion targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        //transform.LookAt(player.transform);

        transform.position += transform.forward * Time.deltaTime * moveSpeed;
    }
}
