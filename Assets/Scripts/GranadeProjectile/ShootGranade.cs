using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGranade : MonoBehaviour
{
    public Granade GranadePrefab;
    public void ShootGranadeAtPlayer(Vector3 targetPos)
    {
        Instantiate(GranadePrefab, transform.position, transform.rotation);
    }

}
