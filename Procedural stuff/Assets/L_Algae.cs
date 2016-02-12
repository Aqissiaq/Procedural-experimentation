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
            gameObject.transform.Translate(new Vector3(voxelSize, 0, 0));
            LSystemChange(globalInput);
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
                    o.transform.position = currentLocation;
                    o.transform.rotation = currentRotation;
                    currentLocation += o.transform.forward * voxelSize;
                    break;

                case (Alphabet.X):
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
