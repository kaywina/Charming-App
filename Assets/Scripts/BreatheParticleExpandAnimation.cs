using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BreatheParticleExpandAnimation : MonoBehaviour
{ 
    public BreatheControl breatheControl;
    public ParticleSystem particles;

    private int fpsLimit = 90;

    private float frameTime;
    private float increment;

    public float minScaleValue = 1f;
    public float maxScaleValue = 3f;

    private void OnEnable()
    {
        frameTime = 0;
        increment = 0;

        BreatheControl.OnSliderChanged += UpdateFrameTime;
        frameTime = breatheControl.GetBreatheInOutSeconds() / fpsLimit;
        increment = (maxScaleValue - minScaleValue) / fpsLimit;

        InvokeRepeating("NextFrame", 0f, frameTime);

        ParticleSystemShapeType circleShape = ParticleSystemShapeType.Circle;
        var shape = particles.shape;
        shape.shapeType = circleShape;
        shape.radius = minScaleValue;
    }

    private void OnDisable()
    {
        CancelInvoke("NextFrame");
        ParticleSystemShapeType circleShape = ParticleSystemShapeType.Circle;
        var shape = particles.shape;
        shape.shapeType = circleShape;
        shape.radius = minScaleValue;
    }

    void UpdateFrameTime()
    {
        frameTime = breatheControl.GetBreatheInOutSeconds() / fpsLimit;
        CancelInvoke("NextFrame");
        InvokeRepeating("NextFrame", 0f, frameTime);
        //Debug.Log("Update frame time");
    }

    private void NextFrame()
    {
        // do the animation
        ParticleSystemShapeType circleShape = ParticleSystemShapeType.Circle;
        var shape = particles.shape;
        shape.shapeType = circleShape;

        if (breatheControl.GetBreatheInOutFlag()) // get bigger if breathing in
        { 
            shape.radius = shape.radius + increment;
            //transform.localScale = new Vector3(transform.localScale.x + increment, transform.localScale.y + increment, transform.localScale.z + increment);
        }
        else // get smaller if breathing out
        {
            shape.radius = shape.radius - increment;
            //transform.localScale = new Vector3(transform.localScale.x - increment, transform.localScale.y - increment, transform.localScale.z - increment);
        }

        // check if finished breathing in or out
        if (shape.radius >= maxScaleValue)
        {
            breatheControl.Breathe(false);
        }
        else if (shape.radius <= minScaleValue)
        {
            breatheControl.Breathe(true);
        }
    }
}
