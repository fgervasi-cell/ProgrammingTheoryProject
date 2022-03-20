using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class Pickup : MonoBehaviour
{
    [SerializeField] private AudioClip pickupSound;
    private AudioSource audioSrc;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioSrc.PlayOneShot(pickupSound);
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            ReleaseEffect();
            Destroy(gameObject, pickupSound.length);
        }
    }

    protected abstract void ReleaseEffect();
}
