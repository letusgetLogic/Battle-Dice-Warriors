using System.Collections.Generic;

public static class KeyType
{
    public enum MovementKey
    {
        Move_Tile_X_D1_6_Any,
        Move_Tile_X_D1_6_Orthogonal,
        Move_Tile_X_D1_6_Diagonal,

        Move_Tile_X_D1_3_Any,
        Move_Tile_X_D1_3_Orthogonal,
        Move_Tile_X_D1_4_Orthogonal,
        Move_Tile_X_D1_3_Diagonal,
        Move_Tile_X_D1_4_Diagonal = 12,

        Move_Tile_1_D1_6_Any = 10,
        Move_Tile_1_D4_6_Any = 7,
        Move_Tile_1_D4_6_Orthogonal = 8,
        Move_Tile_1_D1_6_Diagonal = 11,
        Move_Tile_1_D4_6_Diagonal = 9,
    }

    public static readonly Dictionary<MovementKey, string> MoveInfos = new Dictionary<MovementKey, string>
        {
            { MovementKey.Move_Tile_X_D1_6_Any, "Move in any direction X Tiles" },
            { MovementKey.Move_Tile_X_D1_6_Orthogonal, "Move orthogonally X Tiles" },
            { MovementKey.Move_Tile_X_D1_6_Diagonal, "Move diagonally X Tiles" },

            { MovementKey.Move_Tile_X_D1_3_Any, $"Move in any direction X Tiles,\\n with Dice 1-3" },
            { MovementKey.Move_Tile_X_D1_3_Orthogonal, $"Move orthogonally X Tiles,\\n with Dice 1-3" },
            { MovementKey.Move_Tile_X_D1_4_Orthogonal, $"Move orthogonally X Tiles,\\n with Dice 1-4" },
            { MovementKey.Move_Tile_X_D1_3_Diagonal, $"Move diagonally X Tiles,\\n with Dice 1-3" },
            { MovementKey.Move_Tile_X_D1_4_Diagonal, $"Move diagonally X Tiles,\\n with Dice 1-4" },

            { MovementKey.Move_Tile_1_D1_6_Any, $"Move in any direction 1 Tile,\\n with Dice 1-6" },
            { MovementKey.Move_Tile_1_D4_6_Any, $"Move in any direction 1 Tile,\\n with Dice 4-6" },
            { MovementKey.Move_Tile_1_D4_6_Orthogonal, $"Move orthogonally 1 Tile,\\n with Dice 4-6" },
            { MovementKey.Move_Tile_1_D1_6_Diagonal, $"Move diagonally 1 Tile,\\n with Dice 1-6" },
            { MovementKey.Move_Tile_1_D4_6_Diagonal, $"Move diagonally 1 Tile,\\n with Dice 4-6" },
        };
}
