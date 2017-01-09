using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
public class CylinderShell : MonoBehaviour {

    [SerializeField]
    protected float height = 1;
    [SerializeField]
    protected float radius = 1;
    [SerializeField]
    protected int numFaces = 10;

    protected MeshFilter meshFilter;

    void Start () {
        //create a cylindrical shell mesh
        meshFilter = GetComponent<MeshFilter>();

        Mesh cylinderMesh = new Mesh();
        Assert.IsTrue(numFaces > 3);
        Vector3[] verticies = new Vector3[2 * (numFaces + 1)]; //+1 for the seam
        Vector2[] uvs = new Vector2[verticies.Length];
        int[] triangles = new int[3 * 2 * numFaces];

        //Populate vertices
        //Even vertices are bottom ring, Odd vertices are the corresponding point on the top ring.
        for (int i = 0; i < numFaces; i++) {
            int vertexIndex = 2 * i;

            float lerpValue = (float)i / numFaces;
            float vertexAngle = lerpValue * 2 * Mathf.PI;
            Vector2 xzPos = radius * vertexAngle.RadToVector2();

            //set vertex values
            verticies[vertexIndex] = new Vector3(xzPos.x, 0, xzPos.y);
            verticies[vertexIndex + 1] = new Vector3(xzPos.x, height, xzPos.y);

            //set UV values
            uvs[vertexIndex] = new Vector2(lerpValue, 0);
            uvs[vertexIndex + 1] = new Vector2(lerpValue, 1);
        }

        //create seam of duplicated vertices, with different UVs.
        verticies[verticies.Length - 2] = verticies[0];
        verticies[verticies.Length - 1] = verticies[1];

        uvs[verticies.Length - 2] = Vector2.right;
        uvs[verticies.Length - 1] = Vector2.one;

        //Connect triangles

        /* 1---3
         * | / |
         * 0---2
         */
        for (int i = 0; i < numFaces; i++) {
            int triangleIndex = 6 * i;
            int vertexIndex = 2 * i;

            //first triangle
            triangles[triangleIndex + 0] = vertexIndex + 0;
            triangles[triangleIndex + 1] = vertexIndex + 3;
            triangles[triangleIndex + 2] = vertexIndex + 2;

            //second triangle
            triangles[triangleIndex + 3] = vertexIndex + 0;
            triangles[triangleIndex + 4] = vertexIndex + 1;
            triangles[triangleIndex + 5] = vertexIndex + 3;
        }

        meshFilter.mesh = cylinderMesh;
        cylinderMesh.vertices = verticies;
        cylinderMesh.uv = uvs;
        cylinderMesh.triangles = triangles;
        //TODO: normals, tangents?
    }
}
