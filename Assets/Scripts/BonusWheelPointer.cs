using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusWheelPointer : MonoBehaviour
{
    public SoundManager soundManager;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Pin")
        {
            //Debug.Log("Bonus wheel pointer hit a pin");
            // this gets called every frame using OnCollisionStay2D vs other methods, but works as a hack to prevent the extra sounds that occur with those methods
            if (soundManager != null) { soundManager.PlayWheelPointerSound(); }
            else { Debug.Log("SoundManager variable has not been assigned in inspector"); }
        }
    }
}
