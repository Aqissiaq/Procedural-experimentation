using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VoxelTree0 : MonoBehaviour {

    public GameObject trunkVoxel;
    public GameObject leafVoxel;

    public int minHeight;
    public int maxHeight;

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
            LeafGrower(endLeaves);
            endLeaves.Clear();
            transform.Translate(new Vector3(20, 0, 0));
        }
    }

    public void TrunkGrower(Vector3 startPos)
    {
        //randomize parameters
        int trunkHeight = Random.Range(minHeight, maxHeight + 1);

        //grow trunk
        for (int i = 0; i < trunkHeight; i++)
        {
            Vector3 drawPos = new Vector3(startPos.x, startPos.y + (i), startPos.z);
            GameObject trunkPiece = Instantiate(trunkVoxel, drawPos, Quaternion.identity) as GameObject;
            trunkPiece.transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));
        }
        //add trunk end to list of branch ends
        endLeaves.Add(startPos + Vector3.up * trunkHeight);

        //grow branches
        BranchGrower(startPos, trunkHeight);
    }


    public void BranchGrower(Vector3 trunkBase, int trunkHeight)
    {
        //randomize parameters
        int branchNumber = Random.Range(minBranches, maxBranches + 1);
        List<Vector3> branchLocations = new List<Vector3>();

        //find random locations for branches (may need weighting)
        for (int i = 0; i < branchNumber; i++)
        {
            int randomizer = Random.Range(3, trunkHeight);
            Vector3 branchLocation = trunkBase + Vector3.up * randomizer;
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
    }

    public void LeafGrower(List<Vector3> leafLocations)
    {
        for (int i = 0; i < leafLocations.Count; i++)
        {
            FillSphere(leafLocations[i], leafVoxel, Random.Range(minLeaves, maxLeaves), leafDensity);
        }
    }

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
