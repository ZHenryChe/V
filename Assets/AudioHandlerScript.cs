using UnityEngine;
using System.Collections;

public class AudioHandlerScript : MonoBehaviour
{
    public AudioSource narratingAudiosource;

    public IEnumerator NarrateAudioAndWait(string clipName)
    {
        AudioClip clip = Resources.Load<AudioClip>(clipName);
        if (clip == null) Debug.LogError("Clip not found: " + clipName);
        else Debug.Log("Playing clip: " + clipName);

        narratingAudiosource.clip = clip;
        narratingAudiosource.Play();
        // Wait while audio is playing
        yield return new WaitWhile(() => narratingAudiosource.isPlaying);
        Debug.Log("Audio finished playing!");
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
