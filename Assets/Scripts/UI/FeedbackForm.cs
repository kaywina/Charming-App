using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackForm : MonoBehaviour
{
    public GameObject submitButton;
    public GameObject thanks;

    private void OnEnable()
    {
        submitButton.SetActive(true);
        thanks.SetActive(false);
    }

    public void ShowThanks()
    {
        submitButton.SetActive(false);
        thanks.SetActive(true);
    }
}
