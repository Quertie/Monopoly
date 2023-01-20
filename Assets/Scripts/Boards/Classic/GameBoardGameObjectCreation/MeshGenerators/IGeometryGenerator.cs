using UnityEngine;

namespace Boards.Classic.GameBoardGameObjectCreation.MeshGenerators
{
    public interface IGeometryGenerator
    {
        Mesh GetMesh();
        Vector3 GetTokenPositionMarker();
    }
}
