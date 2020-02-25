using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretManager : MonoBehaviour
{
    public GameObject canvasInteractable;
    public GameObject canvasNoninteractable;
    public Secret[] secrets;
    public GameObject worldSpaceSecrets;
    public GameObject canvasSecret;

    private int lastIndex = 0;

    public void WhenShowSecret(int i)
    {
        canvasInteractable.SetActive(false);
        canvasNoninteractable.SetActive(false);
        worldSpaceSecrets.SetActive(true);
        secrets[i].Show();
        canvasSecret.SetActive(true);
        lastIndex = i;
    }

    public void WhenHideSecret()
    {
        canvasSecret.SetActive(false);
        canvasInteractable.SetActive(true);
        canvasNoninteractable.SetActive(true);
        worldSpaceSecrets.SetActive(false);
        secrets[lastIndex].Hide();
    }
}
