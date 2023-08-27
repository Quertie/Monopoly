using System.Collections.Generic;
using UnityEngine;

namespace Boards.Classic.GameBoardGameObjectCreation.MeshGenerators
{
    public interface IGeometryGenerator
    {
        Mesh GetMesh();
        List<Vector3> GetTokenPositionMarkers(int numberOfPlayersOnSquare);
    }
}
