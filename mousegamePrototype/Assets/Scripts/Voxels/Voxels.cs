using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class Voxels : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MeshFilter filter = gameObject.GetComponent<MeshFilter>();

        Mesh mesh = new Mesh();

        mesh.vertices = new Vector3[]
        {
            new Vector3(-1,-1,0),
            new Vector3(-1,1,0),
            new Vector3(1,-1,0),
            new Vector3(1,1,0)
        };

        mesh.triangles = new int[] {
            0, 1, 2,
            1, 3, 2
        };

        mesh.RecalculateNormals();

        filter.mesh = mesh;
    }

  
}
