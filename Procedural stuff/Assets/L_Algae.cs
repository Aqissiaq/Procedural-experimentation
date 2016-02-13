using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class L_Algae : MonoBehaviour {

    public enum Alphabet { X, F, min, plus, push, pop };
    public float voxelSize = 1;
    public float angleChange = 25;
    public int iterations;
    public List<Alphabet> globalInput;
    public GameObject voxel;

    int counter = 0;


    void Start()
    {
        voxel.transform.localScale = new Vector3(voxelSize, voxelSize, voxelSize);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Growth(globalInput, transform.position);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            for (int i = 0; i < iterations; i++)
            {
                LSystemChange(globalInput);
            }
        }
    }


    //algorithm for algae
    // X → F−[[X]+X]+F[+FX]−X), (F → FF) where F is draw forward, + and - correspond to rotation and ][ stores and restores variables


 public void Growth(List<Alphabet> input, Vector3 inPos)
    {
        Vector3 currentLocation = inPos;
        Vector3 pushLocation = Vector3.zero;
        Quaternion currentRotation = Quaternion.identity;
        Quaternion pushRotation = Quaternion.identity;
        for (int i = 0; i < input.Count; i++)
        {
            switch (input[i])
            {
                case (Alphabet.F):
                    //draw forward
                    GameObject o = GameObject.Instantiate(voxel);
                    o.transform.position = pushLocation;
                    o.transform.rotation = pushRotation;
                    currentLocation += o.transform.up * voxelSize;
                    break;

                case (Alphabet.X):
                    //currentLocation += Vector3.up * voxelSize;
                    break;

                case (Alphabet.min):
                    //increase angle
                        currentRotation = Quaternion.AngleAxis(angleChange, Vector3.right) * currentRotation;
                    break;

                case (Alphabet.plus):
                    //decrease angle
                        currentRotation = Quaternion.AngleAxis(-angleChange, Vector3.right) * currentRotation;
                    break;

                case (Alphabet.push):
                    pushRotation = currentRotation;
                    pushLocation = currentLocation;
                    break;

                case (Alphabet.pop):
                    currentLocation = pushLocation;
                    currentRotation = pushRotation;
                    break;


                default:
                    Debug.Log("No?");
                    break;
            }
        }
    }


    /*public IEnumerator Growth(List<Alphabet> input, Vector3 inPos)
    {
        int count = input.Count;
        Vector3 currentLocation = inPos;
        Vector3 pushLocation = Vector3.zero;
        Quaternion currentRotation = Quaternion.identity;
        Quaternion pushRotation = Quaternion.identity;
        while (counter < count)
        {
            counter++;
            switch (input[counter])
            {
                case (Alphabet.F):
                    //draw forward
                    GameObject o = GameObject.Instantiate(voxel);
                    o.transform.position = pushLocation;
                    o.transform.rotation = pushRotation;
                    currentLocation += o.transform.up * voxelSize;
                    break;

                case (Alphabet.X):
                    //currentLocation += Vector3.up * voxelSize;
                    break;

                case (Alphabet.min):
                    //increase angle
                    int choice = Random.Range(0, 1);
                    if (choice == 0)
                    {
                        currentRotation = Quaternion.AngleAxis(angleChange, Vector3.right) * currentRotation;
                    }
                    else if (choice == 1)
                    {
                        currentRotation = Quaternion.AngleAxis(angleChange, Vector3.back) * currentRotation;
                    }
                    break;

                case (Alphabet.plus):
                    //decrease angle
                    choice = Random.Range(0, 1);
                    if (choice == 0)
                    {
                        currentRotation = Quaternion.AngleAxis(-angleChange, Vector3.right) * currentRotation;
                    }
                    else if (choice == 1)
                    {
                        currentRotation = Quaternion.AngleAxis(-angleChange, Vector3.back) * currentRotation;
                    }
                    break;

                case (Alphabet.push):
                    pushRotation = currentRotation;
                    pushLocation = currentLocation;
                    break;

                case (Alphabet.pop):
                    currentLocation = pushLocation;
                    currentRotation = pushRotation;
                    break;


                default:
                    Debug.Log("No?");
                    break;
            }
            yield return null;
        }
    }*/


    public void LSystemChange(List<Alphabet> input)
    {
        List<Alphabet> replaceString = new List<Alphabet>();
        replaceString.Clear();
        for (int i = 0; i < input.Count; i++)
        {
            switch (input[i])
            {
                case (Alphabet.X):
                    //change string
                    replaceString.Add(Alphabet.F);
                    replaceString.Add(Alphabet.min);
                    replaceString.Add(Alphabet.push);
                    replaceString.Add(Alphabet.push);
                    replaceString.Add(Alphabet.X);
                    replaceString.Add(Alphabet.pop);
                    replaceString.Add(Alphabet.plus);
                    replaceString.Add(Alphabet.X);
                    replaceString.Add(Alphabet.pop);
                    replaceString.Add(Alphabet.plus);
                    replaceString.Add(Alphabet.F);
                    replaceString.Add(Alphabet.push);
                    replaceString.Add(Alphabet.plus);
                    replaceString.Add(Alphabet.F);
                    replaceString.Add(Alphabet.X);
                    replaceString.Add(Alphabet.pop);
                    replaceString.Add(Alphabet.min);
                    replaceString.Add(Alphabet.X);


                    break;

                case (Alphabet.F):
                    //change string
                    replaceString.Add(Alphabet.F);
                    replaceString.Add(Alphabet.F);
                    break;
                default:
                    break;
            }
        }
        globalInput = replaceString;
    }
}
