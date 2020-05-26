using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleExplosion : MonoBehaviour
{

    private WaitForSeconds waitForDestroy;
    private WaitForSeconds waitForReenable;

    public void Start()
    {
        waitForDestroy = new WaitForSeconds(1.0f);
        waitForReenable = new WaitForSeconds(2.5f);
    }

    private Renderer R;
    private MeshRenderer MR;
    private SkinnedMeshRenderer SMR;
    private MeshFilter MF;
    private Collider C;
    private Mesh M;


    public void OnEnable()
    {
        R = GetComponent<Renderer>();
        MR = GetComponent<MeshRenderer>();
        SMR = GetComponent<SkinnedMeshRenderer>();
        MF = GetComponent<MeshFilter>();
        C = GetComponent<Collider>();
        M = new Mesh();
    }

    public void Explode()
    {
        StartCoroutine(SplitMesh(false));
    }

    private IEnumerator SplitMesh(bool destroy)
    {
        if (MF == null)
        {
            //Debug.Log("MeshFilter is null");
            yield return null;
        }

        if (SMR == null)
        {
            //Debug.Log("SkinnedMeshRenderer is null");
            yield return null;
        }

        if (C != null)
        {
            C.enabled = false;
        }

        if (MF != null) { M = MF.mesh; }
        else if (SMR != null) { M = SMR.sharedMesh; }

        Material[] materials = new Material[0];
        if (MR != null) { materials = MR.materials; }
        else if (SMR != null) { materials = SMR.materials; }

        Vector3[] verts = M.vertices;
        Vector3[] normals = M.normals;
        Vector2[] uvs = M.uv;
        for (int submesh = 0; submesh < M.subMeshCount; submesh++)
        {

            int[] indices = M.GetTriangles(submesh);

            for (int i = 0; i < indices.Length; i += 3)
            {
                Vector3[] newVerts = new Vector3[3];
                Vector3[] newNormals = new Vector3[3];
                Vector2[] newUvs = new Vector2[3];
                for (int n = 0; n < 3; n++)
                {
                    int index = indices[i + n];
                    newVerts[n] = verts[index];
                    newUvs[n] = uvs[index];
                    newNormals[n] = normals[index];
                }

                Mesh mesh = new Mesh();
                mesh.vertices = newVerts;
                mesh.normals = newNormals;
                mesh.uv = newUvs;

                mesh.triangles = new int[] { 0, 1, 2, 2, 1, 0 };

                GameObject GO = new GameObject("Triangle " + (i / 3));
                //GO.layer = LayerMask.NameToLayer("Particle");
                GO.transform.position = transform.position;
                GO.transform.rotation = transform.rotation;
                GO.AddComponent<MeshRenderer>().material = materials[submesh];
                GO.AddComponent<MeshFilter>().mesh = mesh;
                GO.AddComponent<BoxCollider>();
                Vector3 explosionPos = new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(0f, 0.5f), transform.position.z + Random.Range(-0.5f, 0.5f));
                GO.AddComponent<Rigidbody>().AddExplosionForce(Random.Range(300, 500), explosionPos, 5);
                GO.GetComponent<Rigidbody>().useGravity = false;
                Destroy(GO, 1 + Random.Range(0.0f, 2.0f));
            }
        }

        R.enabled = false;

        yield return waitForDestroy;
        if (destroy == true)
        {
            Destroy(gameObject);
        }
        else
        {
            yield return waitForReenable;
            R.enabled = true;
            if (C != null) { C.enabled = false; }
        }
    }
}
