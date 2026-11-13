using UnityEngine;

public class PlayerInputAction : ActionStack.Action
{
    private bool done;

    private LayerMask pieceMask;

    GamePiece selected;

    private Team team;

    public PlayerInputAction(LayerMask pieceMask, Team team) 
    { 
        this.pieceMask = pieceMask;
        this.team = team;
    }

    public override void OnBegin(bool bFirstTime)
    {
        Debug.Log($"{team}'s turn: select a piece");
    }

    // Update is called once per frame
    public override void OnUpdate()
    {
        base.OnUpdate();

        if (Input.GetMouseButtonDown(0))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(mouseRay, out RaycastHit hitCol, 100f, pieceMask))
            {
                //hit game piece?
                GamePiece piece = hitCol.collider.GetComponentInParent<GamePiece>();

                //Check if it is the right team
                if (piece == null || piece.Team != team) return;

                if (piece == selected)
                {
                    Deselect();
                    Debug.Log("piece Unselceted");
                }
                else if (selected == null)
                {
                    Select(piece);
                    Debug.Log("piece selceted");
                }

                    return;
            }

            if (selected == null) return;

            Plane ground = new Plane(Vector3.up, Vector3.zero);

            float distance;

            if (ground.Raycast(mouseRay, out distance))
            {
                Vector3 hit = mouseRay.GetPoint(distance);

                Vector2Int hitTile = Board.ToXZ(hit);

                //is it a tile and is it occupied?
                if (Board.Instance.IsInBounds(hitTile) && !Board.Instance.IsOccupied(hitTile))
                {
                    ActionStack.Main.PushAction(new MovePieceAction(selected, hitTile));
                    Debug.Log("Tile selceted");
                    done = true;
                }
                else
                {
                    Debug.Log("tile occupied");
                }
            }
        }
    }

    private void Deselect()
    {
        selected = null;
    }

    private void Select(GamePiece piece)
    {
        selected = piece;
    }

    public override bool IsDone()
    {
        return done;
    }

    public override void OnEnd()
    {
        //Check if Win
        if (Board.Instance.HasLine(team))
        {
            Debug.Log($"{team} team Won!");
        }

        //Switch team every turn
        Team nextTeam = team == Team.Red ? Team.Blue : Team.Red;
        ActionStack.Main.PushAction(new PlayerInputAction(pieceMask, nextTeam));
    }
}
