using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CantorDust : MonoBehaviour {

    public enum Alphabet { A, B };
    public float voxelSize = 1;
    public float angleChange = 90;
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

    //algorithm for Cantor dust
    // (A → ABA), (B → BBB) where A means draw forward and B means move forward


    public void Growth(List<Alphabet> input, Vector3 inPos)
    {
        Vector3 currentLocation = inPos;
        for (int i = 0; i < input.Count; i++)
        {
            switch (input[i])
            {
                case (Alphabet.A):
                    //draw forward
                    GameObject o = GameObject.Instantiate(voxel);
                    o.transform.position = currentLocation;
                    currentLocation += Vector3.up * voxelSize;
                    break;

                case (Alphabet.B):
                    //move forward
                    currentLocation += Vector3.up * voxelSize;
                    break;

                default:
                    Debug.Log("Neither?");
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
                case (Alphabet.A):
                    //change string
                    replaceString.Add(Alphabet.A);
                    replaceString.Add(Alphabet.B);
                    replaceString.Add(Alphabet.A);
                    break;

                case (Alphabet.B):
                    //change string
                    replaceString.Add(Alphabet.B);
                    replaceString.Add(Alphabet.B);
                    replaceString.Add(Alphabet.B);
                    break;
                default:
                    break;
            }
        }
        globalInput = replaceString;
    }
}
