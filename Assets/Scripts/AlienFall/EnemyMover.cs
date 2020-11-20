using System;
using UnityEngine;


public class EnemyMover : MonoBehaviour
{
    private GameObject _player;

    public float MoveSpeed = 1.6f;
    public float RotateSpeed = 1.4f;

    protected void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    protected void Update()
    {
        Quaternion targetRotation = Quaternion.LookRotation(_player.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotateSpeed * Time.deltaTime);

        transform.position += transform.forward * Time.deltaTime * MoveSpeed;
    }

    protected void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "PlayerHurtArea") //TODO: replace tagging system [?]
        {
            Destroy(gameObject);
            Lose();
        }
    }

    private void Lose()
    {
        throw new NotImplementedException();
    }
}
