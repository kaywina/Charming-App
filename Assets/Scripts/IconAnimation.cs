using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconAnimation : MonoBehaviour
{
    public GameObject targetGameObject;

    public GameObject objectToMove;

    private Vector3 initialScenePosition;
    private float speed = 500;

    private bool animating;

    private void Awake()
    {
        initialScenePosition = objectToMove.transform.position;
    }

    private void OnEnable()
    {
        StartCoroutine("AnimateIcon");
    }

    private void OnDisable()
    {
        StopCoroutine("AnimateIcon");
        objectToMove.transform.position = initialScenePosition;
    }

    private IEnumerator AnimateIcon()
    {
        //Debug.Log("Start animation coroutine");
        objectToMove.SetActive(true);
        bool animating = true;
        yield return new WaitForSeconds(0.5f); // if you want a delay before start of animation
        while (animating)
        {
            //Debug.Log("Start animating");
            while (Vector3.Distance(objectToMove.transform.position, targetGameObject.transform.position) > 0)
            {
                //Debug.Log("Animation in progress");
                objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, targetGameObject.transform.position, Time.deltaTime * speed);
                yield return new WaitForSeconds(0.02f);
            }

            //Debug.Log("Stop Animating");
            animating = false;
            yield return null;
        }
    }
}
