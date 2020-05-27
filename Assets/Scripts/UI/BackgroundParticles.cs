using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundParticles : MonoBehaviour
{

    public GameObject[] particleSystems;
    public string playerPrefName = "BackgroundParticleSystemIndex";
    public EmissionRateSlider emitSlider;

    public GameObject tornadoPosXSlider;
    public GameObject tornadoPosYSlider;
    public GameObject tornadoRotXSlider;
    public GameObject tornadoRotZSlider;

    // Start is called before the first frame update
    void Start()
    {
        emitSlider.Initialize(); // this sets the number of particles correctly from stored playerpref data

        int startIndex = PlayerPrefs.GetInt(playerPrefName, -1);
        if (startIndex < 0)
        {
            //Debug.Log("No index stored for background particle system; disabling particle systems");
            //DisableAllParticleSystemsObjects(false); // particles are disabled by default and on first run
            EnableGameObjectByIndex(0); // stars (or whatever is first in index) enabled by default
        }
        else if (startIndex < particleSystems.Length)
        {
            //Debug.Log("startIndex is " + startIndex);
            EnableGameObjectByIndex(PlayerPrefs.GetInt(playerPrefName));
        }
        else
        {
            //Debug.LogWarning("Invalid particle index on start, defaulting to no background particles");
            DisableAllParticleSystemsObjects(false);
        }
        
    }

    public void DisableAllParticleSystemsObjects(bool resetPlayerPref)
    {
        for (int i = 0; i < particleSystems.Length; i++)
        {
            particleSystems[i].SetActive(false);
        }
        if (resetPlayerPref) { PlayerPrefs.DeleteKey(playerPrefName); }
    }

    public void EnableGameObjectByIndex (int index)
    {
        for (int n = 0; n < particleSystems.Length; n++)
        {
            if (n == index)
            {
                if (particleSystems[n] != null)
                {
                    // special case for tornado particles that can be rotated in secrets
                    if (particleSystems[n].name == "Tornado")
                    {
                        Debug.Log("Set the position and rotation of tornado particles");
                        tornadoPosXSlider.GetComponent<SetPlayerPrefFromSlider>().SetSliderValue();
                        tornadoPosXSlider.GetComponent<SetPositionFromSlider>().SetLocalPosition();

                        tornadoPosYSlider.GetComponent<SetPlayerPrefFromSlider>().SetSliderValue();
                        tornadoPosYSlider.GetComponent<SetPositionFromSlider>().SetLocalPosition();

                        tornadoRotXSlider.GetComponent<SetPlayerPrefFromSlider>().SetSliderValue();
                        tornadoRotXSlider.GetComponent<SetRotationFromSlider>().SetLocalRotation();

                        tornadoRotZSlider.GetComponent<SetPlayerPrefFromSlider>().SetSliderValue();
                        tornadoRotZSlider.GetComponent<SetRotationFromSlider>().SetLocalRotation();
                    }
                    particleSystems[n].SetActive(true);
                }
            }
            else
            {
                if (particleSystems[n] != null) { particleSystems[n].SetActive(false); }
            }
        }
        PlayerPrefs.SetInt(playerPrefName, index);
    }

    public void SetEmissionRateMultiplier(float multiplier)
    {
        for (int i = 0; i < particleSystems.Length; i++)
        {
            var e = particleSystems[i].GetComponent<ParticleSystem>().emission;
            e.rateOverTimeMultiplier = multiplier;
            //particleSystems[i].GetComponent<ParticleSystem>().emission.rateOverTimeMultiplier = multipler;
        }
    } 

    public int GetIndex()
    {
        return PlayerPrefs.GetInt(playerPrefName);
    }

    public void SetIndex(int newIndex, bool update)
    {
        PlayerPrefs.SetInt(playerPrefName, newIndex);
        if (update) { EnableGameObjectByIndex(newIndex); }
    }
}
