using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public class SyncTextMeshColor : MonoBehaviour
{

    public TextMesh selfMesh;
    public TextMesh targetMesh;

    private void OnEnable()
    {
        selfMesh.color = targetMesh.color;
    }
}
