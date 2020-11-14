using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public List<ParticleSystem> Particles = new List<ParticleSystem>();
    public List<AudioSource> Audio = new List<AudioSource>();

    void Start()
    {
        
    }

    public void ShootingGraphics()
    {
        foreach(ParticleSystem sys in Particles)
        {
            sys.Emit(1);
        }
    }
}
