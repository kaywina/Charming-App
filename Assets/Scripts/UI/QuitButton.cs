using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public Charms charmManager;
    public ParticleSystem particles;

    public GameObject exitText;
    public GameObject yesButton;
    public GameObject noButton;

    public GameObject hearty;
    public GameObject starry;

    // Animation component references for four quadrants of the icon
    public Animation topLeftAnim;
    public Animation topRightAnim;
    public Animation bottomLeftAnim;
    public Animation bottomRightAnim;

    private float quitFunctionDelay = 1.5f;

    public void Quit()
    {
        exitText.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);
        hearty.SetActive(false);
        starry.SetActive(false);

        particles.Play();

        topLeftAnim.Play();
        topRightAnim.Play();
        bottomLeftAnim.Play();
        bottomRightAnim.Play();

        Invoke("CallQuitFunction", quitFunctionDelay); 
    }

    private void CallQuitFunction()
    {
        charmManager.Quit();
    }
}
