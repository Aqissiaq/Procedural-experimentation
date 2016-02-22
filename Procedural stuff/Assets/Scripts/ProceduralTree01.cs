using UnityEngine;
using System.Collections;

public class ProceduralTree01 : MonoBehaviour {

    void Start()
    {
        StartCoroutine(TrunkGrower(gameObject.transform.position));
    }

    public IEnumerator TrunkGrower(Vector3 pos)
    {
        GameObject trunk = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        trunk.transform.localScale = new Vector3(Random.Range(2, 5), Random.Range(10, 25), Random.Range(2, 5));
        transform.position = pos + new Vector3(0, transform.localScale.y / 2, 0);
        yield return StartCoroutine(BranchGrower(trunk.transform.position, transform.localScale.y));
    }

    public IEnumerator BranchGrower(Vector3 pos, float height)
    {
        int branchNumber = Random.Range(1, 5);

        for (int i = 0; i < branchNumber; i++)
        {
            GameObject branch = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            branch.transform.localScale = new Vector3(Random.Range(.1f, 2), Random.Range(3, 10), Random.Range(.1f, 2));
            branch.transform.position = pos + new Vector3(0, Random.Range(5, height), 0);
            yield return new WaitForSeconds(2);
            branch.transform.Rotate(new Vector3(Random.Range(45, 90), Random.Range(0, 360), 0));
            branch.transform.Translate(branch.transform.up * branch.transform.localScale.y / 2);
            yield return null;
        }
    }
}
