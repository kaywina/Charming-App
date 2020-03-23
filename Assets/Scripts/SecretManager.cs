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
    public GameObject hideSecretButton;

    private int lastIndex = 0;

    private void Start()
    {
        hideSecretButton.SetActive(false);
        canvasSecret.SetActive(false);
    }

    public void WhenShowSecret(int i)
    {
        canvasInteractable.SetActive(false);
        canvasNoninteractable.SetActive(false);
        worldSpaceSecrets.SetActive(true);
        secrets[i].Show();
        canvasSecret.SetActive(true);
        lastIndex = i;
        hideSecretButton.SetActive(true);
    }

    public void WhenHideSecret()
    {
        canvasSecret.SetActive(false);
        canvasInteractable.SetActive(true);
        canvasNoninteractable.SetActive(true);
        worldSpaceSecrets.SetActive(false);
        secrets[lastIndex].Hide();
        hideSecretButton.SetActive(false);
    }
}
