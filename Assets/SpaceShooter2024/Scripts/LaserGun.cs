using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour
{

    [SerializeField] private Animator laserAnimator;
    [SerializeField] private AudioClip laserSFX;
    [SerializeField] private Transform raycastOringin;

    private AudioSource laserAudioSource;

    private RaycastHit hit;

    private void Awake()
    {
        if(GetComponent<AudioSource>() != null)
        {
            laserAudioSource = GetComponent<AudioSource>();
        }
    }
    public void LaserGunFired()
    {
        // animate the gun
        laserAnimator.SetTrigger("Fire");

        // play laser gun SFX
        laserAudioSource.PlayOneShot(laserSFX);

        // raycast
        if(Physics.Raycast(raycastOringin.position, raycastOringin.forward, out hit, 800f))
        {
            if (hit.transform.GetComponent<AsteroidHit>() != null)
            {
                hit.transform.GetComponent<AsteroidHit>().AsteroidDestroyed();
            }
            else if(hit.transform.GetComponent<IRaycastInterface>() != null)
            {
                hit.transform.GetComponent<IRaycastInterface>().HitByRaycast();
            }
        }
    }
}
