using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReactiveOnInteractable : MonoBehaviour
{
    public Button becomesInteractable;
    public ActiveUntilDeactivated[] toReactivate;
    private bool reactivate = false;

    // Update is called once per frame
    void Update()
    {
        if (reactivate && becomesInteractable.interactable)
        {
            for(int i = 0; i < toReactivate.Length; i++)
            {
                toReactivate[i].Reactivate();
                reactivate = false;
            }
        }
        else if (!reactivate && !becomesInteractable.interactable)
        {
            reactivate = true;
        }
    }
}
