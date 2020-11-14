using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts
{
    public class Gun : MonoBehaviour
    {
        public List<ParticleSystem> Particles = new List<ParticleSystem>();
        public List<AudioSource> Audio = new List<AudioSource>();
        public float RayDistance = 1000f;
        private List<Transform> _bones = new List<Transform>();
        private Transform _muzzleBone;

        void Start()
        {

            foreach (var bone in GameObject.FindGameObjectsWithTag("Bone"))
            {
                _bones.Add(bone.transform);
                Debug.Log(bone.name);
                if (bone.name == "Bone_Muzzle")
                    _muzzleBone = bone.transform;
            }
        }

        public void Fire()
        {
            var ray = new Ray(_muzzleBone.position, _muzzleBone.forward);
            var cast = Physics.Raycast(ray, out var rayHit, RayDistance, 0, QueryTriggerInteraction.UseGlobal);
            foreach (ITarget target in rayHit.transform.gameObject.GetComponents<ITarget>())
            {
                target.Hit();
            }
        }

        public void ShootingGraphics()
        {
            foreach (ParticleSystem sys in Particles)
            {
                sys.Emit(1);
            }
        }
    }
}