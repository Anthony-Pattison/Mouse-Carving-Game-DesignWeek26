using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace Paper
{
    public static class Meshing
    {
        /// <summary>Copy vertices and indices to a MeshDataArray</summary>
        public static void CopyMeshUInt32(ref Mesh.MeshDataArray meshDataArray, ref NativeList<uint> indices, ref NativeList<Vertex> vertices, ref Bounds bounds)
        {
            // We only need 1 mesh so grab the first one
            Mesh.MeshData meshData = meshDataArray[0];

            // Set Vertex data
            var layout = Vertex.VertexLayout(Allocator.Persistent);
            meshData.SetVertexBufferParams(vertices.Length, layout);
            layout.Dispose();
            meshData.GetVertexData<Vertex>().CopyFrom(vertices.AsArray());

            // Set Indice data
            meshData.SetIndexBufferParams(indices.Length, IndexFormat.UInt32);
            meshData.GetIndexData<uint>().CopyFrom(indices.AsArray());

            // Set submesh
            meshData.subMeshCount = 1;
            meshData.SetSubMesh(0, new SubMeshDescriptor(0, indices.Length)
            {
                bounds = bounds,
                vertexCount = vertices.Length,
            }, MeshUpdateFlags.DontRecalculateBounds);
        }

        public static void CreateQuadUInt32(NativeList<Vertex> vertices, NativeList<uint> indices, int side)
        {
            // Indices (triangles)
            indices.Add((uint)vertices.Length + 0);
            indices.Add((uint)vertices.Length + 1);
            indices.Add((uint)vertices.Length + 2);
            indices.Add((uint)vertices.Length + 2);
            indices.Add((uint)vertices.Length + 1);
            indices.Add((uint)vertices.Length + 3);

            // Vertices in lookup tables
            vertices.Add(new Vertex
            {
                position = Tables.Vertices[Tables.QuadVertices[side][0]],
                normal = Tables.Normals[side],
            });
            vertices.Add(new Vertex
            {
                position = Tables.Vertices[Tables.QuadVertices[side][1]],
                normal = Tables.Normals[side],
            });
            vertices.Add(new Vertex
            {
                position = Tables.Vertices[Tables.QuadVertices[side][2]],
                normal = Tables.Normals[side],
            });
            vertices.Add(new Vertex
            {
                position = Tables.Vertices[Tables.QuadVertices[side][3]],
                normal = Tables.Normals[side],
            });
        }
        public static void CreateCubeUInt32(Mesh.MeshDataArray meshDataArray, Allocator allocator)
        {
            // (1)
            var vertices = new NativeList<Vertex>(allocator);
            var indices = new NativeList<uint>(allocator);

            // (2)
            var bounds = new Bounds(Vector3.one / 2, Vector3.one);

            // (3) 6 sides of cube
            for (int side = 0; side < 6; side++)
            {
                CreateQuadUInt32(vertices, indices, side);
            }

            // (4)
            // bind to mesh data array
            CopyMeshUInt32(ref meshDataArray, ref indices, ref vertices, ref bounds);

            // (5)
            vertices.Dispose();
            indices.Dispose();
        }
    }
}
