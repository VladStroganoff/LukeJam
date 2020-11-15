using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace Assets.Scripts
{
    public class Gun : MonoBehaviour
    {
        public List<ParticleSystem> Particles = new List<ParticleSystem>();
        public List<AudioSource> Audio = new List<AudioSource>();
        public float RayDistance = 1000f;
        private List<Transform> _bones = new List<Transform>();
        private Transform _muzzleBone;
        public SteamVR_Action_Boolean ShootAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("default", "Shoot");
        public bool InfiniteAmmo = true;
        public int Ammo = 3;

        void Start()
        {
            foreach (var bone in GameObject.FindGameObjectsWithTag("Bone"))
            {
                _bones.Add(bone.transform);
                if (bone.name == "Bone_Muzzle")
                    _muzzleBone = bone.transform;

                ShootAction.onStateDown += ShootAction_onStateDown;
            }
        }

        public void Fire()
        {
            var ray = new Ray(_muzzleBone.position, _muzzleBone.forward);
            var cast = Physics.Raycast(ray, out var rayHit, RayDistance, -1, QueryTriggerInteraction.UseGlobal);
            var rayhit = rayHit.transform?.gameObject?.GetComponents<ITarget>();
            if (rayhit != null && rayhit.Length > 0)
                foreach (ITarget target in rayhit)
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

        private void ShootAction_onStateDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
            Fire();
        }
    }
}