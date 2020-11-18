using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParticles : MonoBehaviour
{

    public GameObject[] particleSystems;
    public string playerPrefName = "BackgroundParticleSystemIndex";
    public EmissionRateSlider emitSlider;

    public GameObject tornadoParent;

    // Start is called before the first frame update
    void Start()
    {
        LoadTornadoTransformValues();
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
            if (n == index) {
                if (particleSystems[n] != null) { particleSystems[n].SetActive(true); }
            }
            else {
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

    public void ResetTornado()
    {
        tornadoParent.transform.localPosition = new Vector3(0, 0, 0);
        tornadoParent.transform.localRotation = new Quaternion(0, 0, 0, 0);
    }

    /*
     * This Save function is used by the 'Set as Background' and back button in the secrets panel, for data persistence on the adjustments made to Tornado particles
     * */
    private string posXPrefName = "TornadoPosX";
    private string posYPrefName = "TornadoPosY";
    private string rotXPrefName = "TornadoRotX";
    private string rotZPrefName = "TornadoRotZ";

    public void SaveTornadoTransformValues()
    {
        Debug.Log("Save transform values for Tornado particles");
        PlayerPrefs.SetFloat(posXPrefName, tornadoParent.transform.localPosition.x);
        PlayerPrefs.SetFloat(posYPrefName, tornadoParent.transform.localPosition.y);

        PlayerPrefs.SetFloat(rotXPrefName, tornadoParent.transform.localRotation.x);
        PlayerPrefs.SetFloat(rotZPrefName, tornadoParent.transform.localRotation.z);
    }

    public void LoadTornadoTransformValues()
    {
        Debug.Log("Load transform values for Tornado particles");
        float posX = PlayerPrefs.GetFloat(posXPrefName);
        float posY = PlayerPrefs.GetFloat(posYPrefName);
        float rotX = PlayerPrefs.GetFloat(rotXPrefName);
        float rotZ = PlayerPrefs.GetFloat(rotZPrefName);

        // set the loaded position
        tornadoParent.transform.localPosition = new Vector3(posX, posY, 0);

        // set the loaded rotation
        Quaternion savedRotation = Quaternion.Euler(rotX, 0, rotZ);
        tornadoParent.transform.localRotation = savedRotation;


    }
}
