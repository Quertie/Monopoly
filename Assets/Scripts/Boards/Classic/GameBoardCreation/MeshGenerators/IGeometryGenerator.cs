using UnityEngine;

namespace Boards.Classic.GameBoardCreation.MeshGenerators
{
    public interface IGeometryGenerator
    {
        Mesh GetMesh();
        Vector3 GetTokenPositionMarker();
    }
}
