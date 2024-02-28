using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource ballsCollidingSource;
    [SerializeField] List<AudioClip> ballsCollidingClips = new List<AudioClip>();

    [SerializeField] AudioSource scoreSource;
    [SerializeField] AudioClip scoreClips;

    [SerializeField] AudioSource pocketSource;
    [SerializeField] AudioClip pocketClips;

    [SerializeField] AudioSource gameOverSource;
    [SerializeField] AudioClip gameOverClips;

    [SerializeField] AudioSource cushionSource;
    [SerializeField] AudioClip cushionClips;

    public void SoundCollideBalls()
    {
        AudioClip clip = ballsCollidingClips[Random.Range(0, ballsCollidingClips.Count - 1)];
        ballsCollidingSource.PlayOneShot(clip);
    }

    public void SoundScoring()
    {
        scoreSource.PlayOneShot(scoreClips);
    }

    public void SoundPocket()
    {
        pocketSource.PlayOneShot(pocketClips);
    }

    public void SoundGameOver()
    {
        gameOverSource.PlayOneShot(gameOverClips);
    }

    public void SoundCushion()
    {
        cushionSource.PlayOneShot(cushionClips);
    }
}
