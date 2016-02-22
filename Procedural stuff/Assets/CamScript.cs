using UnityEngine;
using System.Collections;

public class CamScript : MonoBehaviour {

    GameObject treeSpawner;
    public float speed;

    void Start()
    {
        treeSpawner = GameObject.Find("TreeSpawner");
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, treeSpawner.transform.position, Time.deltaTime * speed);
    }
}
