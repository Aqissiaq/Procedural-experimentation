using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VoxelTree0 : MonoBehaviour {

    public GameObject trunkVoxel;
    public GameObject leafVoxel;

    public int minHeight;
    public int maxHeight;

    public int trunkDiverge;

    public int minBranches;
    public int maxBranches;

    public int branchDiverge;

    public float minLeaves;
    public float maxLeaves;

    public int leafDensity;


    List<Vector3> endLeaves = new List<Vector3>();


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TrunkGrower(transform.position);
            endLeaves.Clear();
            transform.Translate(new Vector3(25, 0, 0));
        }
    }

    public void TrunkGrower(Vector3 startPos)
    {
        //randomize parameters
        int trunkHeight = Random.Range(minHeight, maxHeight + 1);
        int zDisp = Random.Range(-trunkDiverge, trunkDiverge);
        int xDisp = Random.Range(-trunkDiverge, trunkDiverge);

        //define trunk vector
        Vector3 trunkVector = new Vector3(xDisp, trunkHeight, zDisp);

        //grow trunk
        for (int i = 0; i < trunkVector.magnitude; i++)
        {
            Vector3 drawPos = startPos + trunkVector * i / trunkVector.magnitude;
            Instantiate(trunkVoxel, drawPos, Quaternion.identity);
        }
        //add trunk end to list of branch ends
        endLeaves.Add(startPos + trunkVector);

        //grow branches
        BranchGrower(trunkVector, startPos);
    }


    public void BranchGrower(Vector3 trunkVector, Vector3 trunkPos)
    {
        //randomize parameters
        int branchNumber = Random.Range(minBranches, maxBranches + 1);
        List<Vector3> branchLocations = new List<Vector3>();

        //find random locations for branches (may need weighting)
        for (int i = 0; i < branchNumber; i++)
        {
            float randomizer = Random.Range(.3f, 1);
            Vector3 branchLocation = trunkPos + trunkVector * randomizer;
            branchLocations.Add(branchLocation);
        }

        //for each branchlocation, grow a branch
        for (int i = 0; i < branchLocations.Count; i++)
        {
            int xDisp = Random.Range(-branchDiverge, branchDiverge);
            int yDisp = Random.Range(0, branchDiverge);
            int zDisp = Random.Range(-branchDiverge, branchDiverge);

            Vector3 branchEnd = branchLocations[i] + new Vector3(xDisp, yDisp, zDisp);
            Vector3 branchVector = (branchEnd - branchLocations[i]);

            //add branchEnds to leaf list
            endLeaves.Add(branchEnd);

            for (int ii = 0; ii < branchVector.magnitude; ii++)
            {
                Vector3 drawPos = branchLocations[i] + branchVector * ii / branchVector.magnitude;
                Instantiate(trunkVoxel, drawPos, Quaternion.identity);
            }
        }

        //grow leaves
        LeafGrower(endLeaves);
    }

    public void LeafGrower(List<Vector3> leafLocations)
    {
        for (int i = 0; i < leafLocations.Count; i++)
        {
            FillSphere(leafLocations[i], leafVoxel, Random.Range(minLeaves, maxLeaves), leafDensity);
        }
    }

    //terrible voxel "sphere"
    void FillSphere(Vector3 origin, GameObject voxel, float radius, int fill)
    {
        float r = radius * radius;
        for (int i = 0; i < fill; i++)
        {
            //create a bunch of random points
            float randX = Random.Range(-radius, radius);
            float randY = Random.Range(-radius, radius);
            float randZ = Random.Range(-radius, radius);

            Vector3 randPos = origin + new Vector3(randX, randY, randZ);

            //if the point is within the radius, draw a voxel
            if ((origin - randPos).sqrMagnitude <= r)
            {
                Instantiate(voxel, randPos, Quaternion.identity);
            }
        }
    }
}