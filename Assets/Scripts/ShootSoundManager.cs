using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSoundManager : MonoBehaviour
{
    public List<AudioClip> m_shootSounds;

    public void PlayShootSound()
    {
        int index = Random.Range(0, m_shootSounds.Count);
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = m_shootSounds[index];
        audioSource.Play();
    }
}
