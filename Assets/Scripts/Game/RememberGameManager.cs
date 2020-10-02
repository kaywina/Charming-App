using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RememberGameManager : MonoBehaviour
{
    private string hasSelectedButtonsPlayerPref = "HasSelectedButtons";
    public GameRemember gameRemember;
    public GameObject rememberComeBack;
    public GameObject instructions;

    private void OnEnable()
    {
        if (PlayerPrefs.GetString(hasSelectedButtonsPlayerPref) == "True")
        {

            gameRemember.DisableControls();
            rememberComeBack.SetActive(true);
            instructions.SetActive(false);
        }
        else
        {
            instructions.SetActive(true);
            rememberComeBack.SetActive(false);
        }
    }

    public bool SetupButtonSelect()
    {
        if (PlayerPrefs.GetString(hasSelectedButtonsPlayerPref) == "True")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetHasSelectedButtonsPlayerPref(bool set)
    {
        if (set)
        {
            PlayerPrefs.SetString(hasSelectedButtonsPlayerPref, "True");
        }
        else
        {
            PlayerPrefs.SetString(hasSelectedButtonsPlayerPref, "False");
        }
    }


}
