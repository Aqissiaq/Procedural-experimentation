using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProceduralTree00 : MonoBehaviour {

    public float sideLength;
    public float segmentHeight;
    public int segmentNumber;

    Vector3[] vertices;
    int[] triangles;
    List<Vector3> normals;
    Mesh mesh;
    void Start()
    {
        normals = new List<Vector3>();
        triangles = new int[((segmentNumber * 6) * 4)];
        vertices = new Vector3[segmentNumber * 6];
        mesh = GetComponent<MeshFilter>().mesh;
        ConstructTrunk(sideLength, transform.position);
    }

    public void ConstructTrunk(float length, Vector3 pos)
    {
        float c = length;
        float a = c / 2;
        float b = Mathf.Sin(Mathf.Deg2Rad * 60) * c;
        gameObject.transform.position = new Vector3(0, 0, b);

        //vertices
        for (int i = 0; i < segmentNumber; i++)
        {
            vertices[0 + (6 * i)] = pos + new Vector3(0, segmentHeight * i, b);
            vertices[1 + (6 * i)] = pos + new Vector3(a, segmentHeight * i, 0);
            vertices[2 + (6 * i)] = pos + new Vector3(a + c, segmentHeight * i, 0);
            vertices[3 + (6 * i)] = pos + new Vector3(2 * c, segmentHeight * i, b);
            vertices[4 + (6 * i)] = pos + new Vector3(a + c, segmentHeight * i, 2 * b);
            vertices[5 + (6 * i)] = pos + new Vector3(a, segmentHeight * i, 2 * b);
        }

        //vertex normals
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertexNorm = Vector3.Normalize(vertices[i] - new Vector3(transform.position.z, vertices[i].y, transform.position.z));
            normals.Add(vertexNorm);
        }

        //for vertex visualization
        for (int i = 0; i < vertices.Length; i++)
        {
            GameObject spawnedObject = GameObject.CreatePrimitive(PrimitiveType.Sphere) as GameObject;
            spawnedObject.transform.position = transform.position + vertices[i];
            spawnedObject.transform.localScale = new Vector3(.1f, .1f, .1f);
        }
        
        //triangles
        for (int i = 0; i < vertices.Length; i++)
        {
            if (i < vertices.Length - 6)
            {
                if (i % 2 != 0 && i > 5)
                {
                    triangles[i + (3 * i)] = i;
                    triangles[i + 1 + (3 * i)] = i - 1;
                    triangles[i + 2 + (3 * i)] = i - 6;
                }
                else
                {
                    triangles[i + (3 * i)] = i;
                    triangles[i + 1 + (3 * i)] = i + 1;
                    triangles[i + 2 + (3 * i)] = i + 6;
                }

            }
        }

        List<Vector3> vertexList = new List<Vector3>(vertices);
        mesh.SetVertices(vertexList);
        mesh.SetNormals(normals);
        mesh.SetTriangles(triangles, 0);
        mesh.Optimize();

    }

    void Update()
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            Debug.DrawRay(transform.position + vertices[i], normals[i]);
        }
    }
}
