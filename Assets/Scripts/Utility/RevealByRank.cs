using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealByRank : MonoBehaviour
{
    public int rankToReveal;
    public GameObject toReveal;
    public GameObject toHide;

    private void OnEnable()
    {
        if (RankManager.GetRank() >= rankToReveal)
        {
            toReveal.SetActive(true);
            if (toHide != null) { toHide.SetActive(false); }
        }
        else
        {
            toReveal.SetActive(false);
            if (toHide != null) { toHide.SetActive(true); }
        }
    }

}
