using UnityEngine;

public class Board : MonoBehaviour
{
    public const int size = 3;
    public const float tileSize = 3;
    public static Board Instance {  get; private set; }

    private GamePiece[,] pieces = new GamePiece[size, size];

    [SerializeField] private LayerMask pieceMask;

  
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ActionStack.Main.PushAction(new PlayerInputAction(pieceMask, Team.Red));
    }

    public static Vector3 TileToWorld(Vector2Int tile)
    {
        return new Vector3(tile.x * tileSize, 0.0f, tile.y * tileSize);
    }

    public static Vector2Int ToXZ(Vector3 v)
    {
        return new Vector2Int(Mathf.RoundToInt(v.x / tileSize), Mathf.RoundToInt(v.z / tileSize));
    }

    public bool IsInBounds(Vector2Int tile)
    {
        return tile.x >= 0 && tile.x < size && tile.y >= 0 && tile.y < size;
    }

    public bool IsOccupied(Vector2Int t)
    {
        return IsInBounds(t) && pieces[t.x, t.y] != null;
    }

    public void Move(GamePiece piece, Vector2Int to)
    {
        if (piece.Tile.HasValue)
        {
            pieces[piece.Tile.Value.x, piece.Tile.Value.y] = null;
        }

        pieces[to.x, to.y] = piece;
        piece.Tile = to;
        piece.transform.position = TileToWorld(to) + Vector3.up * 0.5f;
    }

    private bool TilePiece(int x, int y, Team team)
    {
        GamePiece piece = pieces[x, y];
        return piece != null && piece.Team == team;
    }

    public bool HasLine(Team team)
    {
        // rows and columns
        for (int i = 0; i < size; i++)
        {
            if (TilePiece(0, i, team) && TilePiece(1, i, team) && TilePiece(2, i, team)) return true;  // row
            if (TilePiece(i, 0, team) && TilePiece(i, 1, team) && TilePiece(i, 2, team)) return true;  // column
        }

        // diagonals
        if (TilePiece(0, 0, team) && TilePiece(1, 1, team) && TilePiece(2, 2, team)) return true;
        if (TilePiece(0, 2, team) && TilePiece(1, 1, team) && TilePiece(2, 0, team)) return true;

        return false;
    }
}
