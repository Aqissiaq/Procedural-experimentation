using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DragonCurve : MonoBehaviour {

    public enum Alphabet { X, Y, F, plus, min };
    public float voxelSize = 1;
    public int iterations;
    public List<Alphabet> globalInput;
    public GameObject voxel;


    Quaternion currentRotation = Quaternion.identity;
    public float zAngle = 0;
    int counter = 0;


    void Start()
    {
        voxel.transform.localScale = new Vector3(voxelSize, voxelSize, voxelSize);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            StartCoroutine(Growth(globalInput, transform.position));
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            for (int i = 0; i < iterations; i++)
            {
                LSystemChange(globalInput);
            }
        }
    }


    //algorithm for dragon curve
    //  (X → X+YF+), (Y → −FX−Y) where F is draw forward, + and - rotate 90 degrees




    public IEnumerator Growth(List<Alphabet> input, Vector3 inPos)
    {
        int count = input.Count;
        Vector3 currentLocation = inPos;
        while (counter < count)
        {
            counter++;
            switch (input[counter])
            {
                case (Alphabet.F):
                    //draw forward
                    GameObject o = GameObject.Instantiate(voxel);
                    o.transform.rotation = currentRotation;
                    o.transform.position = currentLocation;
                    currentLocation += o.transform.up * voxelSize;
                    break;

                case (Alphabet.X):
                    break;

                case (Alphabet.Y):
                    break;

                case (Alphabet.min):
                    //increase angle
                    zAngle += 90;
                    currentRotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y, zAngle);
                    break;

                case (Alphabet.plus):
                    //decrease angle
                    zAngle -= 90;
                    currentRotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y, zAngle);
                    break;

                default:
                    Debug.Log("No?");
                    break;
            }
            yield return null;
        }
    }

    // (X → X+YF+), (Y → −FX−Y) where F is draw forward, + and - rotate 90 degrees
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
                    replaceString.Add(Alphabet.X);
                    replaceString.Add(Alphabet.plus);
                    replaceString.Add(Alphabet.Y);
                    replaceString.Add(Alphabet.F);
                    replaceString.Add(Alphabet.plus);
                    break;

                case (Alphabet.Y):
                    //change string
                    replaceString.Add(Alphabet.min);
                    replaceString.Add(Alphabet.F);
                    replaceString.Add(Alphabet.X);
                    replaceString.Add(Alphabet.min);
                    replaceString.Add(Alphabet.Y);
                    break;
                default:
                    break;
            }
        }
        globalInput = replaceString;
    }
}
