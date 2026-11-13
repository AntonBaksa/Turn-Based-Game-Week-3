using UnityEngine;

public class MovePieceAction : ActionStack.Action
{
    private GamePiece piece;
    private Vector2Int targetTile;

    public MovePieceAction(GamePiece piece, Vector2Int targetTile)
    {
        this.piece = piece;
        this.targetTile = targetTile;
    }

    public override void OnBegin(bool bFirstTime)
    {
        Board.Instance.Move(piece, targetTile);
    }
}
