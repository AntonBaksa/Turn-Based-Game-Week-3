using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public class MovePieceAction : GamePiece.Action
{
    private Vector2Int tile;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public MovePieceAction(GamePiece piece, Vector2Int targetTile)
    {
        tile = targetTile;
    }
}
