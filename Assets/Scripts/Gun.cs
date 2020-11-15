using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace Assets.Scripts
{
    public class Gun : MonoBehaviour
    {
        public ParticleSystem Casing;
        public ParticleSystem MuzzleFlash;
        public AudioSource audioSource;
        public Animator Animator;

        public float fireRateInterval = 0.3f;
        private float firingTimer = 0;
        private bool triggerDown;

        public float RayDistance = 1000f;
        private List<Transform> _bones = new List<Transform>();
        private Transform _muzzleBone;
        private Transform _sightTargetBone;
        public SteamVR_Action_Boolean ShootAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("default", "Shoot");

        void Start()
        {
            foreach (var bone in GameObject.FindGameObjectsWithTag("Bone"))
            {
                _bones.Add(bone.transform);
                if (bone.name == "Bone_Muzzle")
                    _muzzleBone = bone.transform;
                if (bone.name == "Bone_SightTarget")
                    _sightTargetBone = bone.transform;

                ShootAction.onStateDown += ShootAction_onStateDown;
            }
        }

        public void Fire()
        {
            Animator.SetTrigger("Shoot");
            var ray = new Ray(_sightTargetBone.position, _sightTargetBone.forward);
            var cast = Physics.Raycast(ray, out var rayHit, RayDistance, -1, QueryTriggerInteraction.UseGlobal);
            var rayhit = rayHit.transform?.gameObject?.GetComponents<ITarget>();

            audioSource.pitch = Random.Range(0.85f, 1.15f);
            audioSource.Play();

            if (rayhit != null && rayhit.Length > 0)
            {
                foreach (ITarget target in rayhit)
                {
                    target.Hit();
                }
            }
        }

        private void Update()
        {
            FireLogic();
        }

        private void FireLogic()
        {
            firingTimer -= Time.deltaTime;
            if (Input.GetAxis("Oculus_CrossPlatform_SecondaryIndexTrigger") > 0.2f)
            {
                if (firingTimer <= 0f && !triggerDown)
                {
                    Fire();
                    firingTimer = fireRateInterval;
                }
                triggerDown = true;
            }
            else
            {
                triggerDown = false;
            }

        }

        public void CasingParticle()
        {
            Casing.Emit(1);
        }
        public void Flash()
        {
            MuzzleFlash.Emit(1);
        }


        private void ShootAction_onStateDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
            Fire();
        }
    }
}