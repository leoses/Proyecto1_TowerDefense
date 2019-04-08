using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {

    public AudioClip loopMusic;
    public AudioClip youWin;
    public AudioClip youLose;
    AudioSource source;

    private void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        source.clip = loopMusic;
        source.loop = true;
        source.Play();
    }

    void Update()
    {
        if (Time.timeScale < 1)
            source.Pause();

        if (!source.isPlaying && Time.timeScale > 0)
            source.Play();
    }

    public void PlayMusic(bool win)
    {
        source.Stop();

        if (win)
            source.clip = youWin;

        else
            source.clip = youLose;

        source.Play();
    }
}
