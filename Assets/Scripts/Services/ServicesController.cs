using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServicesController : MonoBehaviour
{
    public string servicesSceneName;
    
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadSceneAsync(servicesSceneName, LoadSceneMode.Additive);
    }
}
