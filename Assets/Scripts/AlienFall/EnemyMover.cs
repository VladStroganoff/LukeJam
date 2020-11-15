using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMover : MonoBehaviour
{
    private GameObject _player;

    public float moveSpeed = 1.6f;
    public float rotateSpeed = 1.4f;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("PlayerHurtArea");
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion targetRotation = Quaternion.LookRotation(_player.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        //transform.LookAt(player.transform);

        transform.position += transform.forward * Time.deltaTime * moveSpeed;
    }



    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "PlayerHurtArea")
            Destroy(gameObject);
    }
}
