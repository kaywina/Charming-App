using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteNoise : MonoBehaviour
{
    [Range(-1f, 1f)]
    public float offset;

    System.Random rand = new System.Random();

    
    // basic static; requires an audio source (can be empty)
    void OnAudioFilterRead(float[] data, int channels)
    {
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = (float)(rand.NextDouble() * 2.0 - 1.0 + offset);
        }
    }
 }
