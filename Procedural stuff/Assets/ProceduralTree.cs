using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProceduralTree : MonoBehaviour {

    public float sideLength;
    public float segmentHeight = 5;
    public int segmentNumber = 10;

    Vector3[] vertices;
    public int[] triangles;
    Mesh mesh;
    void Start()
    {
        triangles = new int[(segmentNumber * 6)];
        vertices = new Vector3[segmentNumber * 6];
        mesh = GetComponent<MeshFilter>().mesh;
        ConstructTrunk(sideLength);
    }

    public void ConstructTrunk(float length)
    {
        float c = length;
        float a = c / 2;
        float b = Mathf.Sin(Mathf.Deg2Rad * 60) * c;

        for (int i = 0; i < segmentNumber; i++)
        {
            vertices[0 + 6 * i] = new Vector3(0, segmentHeight * i, b);
            vertices[1 + 6 * i] = new Vector3(a, segmentHeight * i, 0);
            vertices[2 + 6 * i] = new Vector3(a + c, segmentHeight * i, 0);
            vertices[3 + 6 * i] = new Vector3(2 * c, segmentHeight * i, b);
            vertices[4 + 6 * i] = new Vector3(a + c, segmentHeight * i, 2 * b);
            vertices[5 + 6 * i] = new Vector3(a, segmentHeight * i, 2 * b);
        }

        for (int i = 0; i < triangles.Length; i++)
        {
            triangles[i] = i;
        }

        List<Vector3> vertexList = new List<Vector3>(vertices);
        mesh.SetVertices(vertexList);
        mesh.triangles = triangles;

    }
}
