using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteNoise : MonoBehaviour
{
    [Range(0, 3f)]
    public float noiseMultiplier = 2;

    [Range(-1, 2f)]
    public float noiseReducer = 1;

    [Range(-1f, 1f)]
    public float noiseOffset;

    System.Random rand = new System.Random();

    public AudioSource emptyAudioSource;

    private void OnEnable()
    {
        emptyAudioSource.Play();
    }

    private void OnDisable()
    {
        emptyAudioSource.Stop();
    }

    // basic static; requires an audio source (can be empty)
    void OnAudioFilterRead(float[] audioSourceData, int channels)
    {
        for (int i = 0; i < audioSourceData.Length; i++)
        {
            audioSourceData[i] = (float)(rand.NextDouble() * noiseMultiplier - noiseReducer + noiseOffset); // would it be possible to adjust this to have multiple types of white noise?
        }
    }
 }
