using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerInputAction : GamePiece.Action
{
    private bool done;

    private HashSet<Vector2Int> tiles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void OnUpdate()
    {
        base.OnUpdate();

        if (Input.GetMouseButtonDown(0))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane ground = new Plane(Vector3.up, Vector3.zero);

            float distance;

            if (ground.Raycast(mouseRay, out distance))
            {
                Vector3 hit = mouseRay.GetPoint(distance);

                Vector2Int hitTile = Board.ToXZ(hit);

                if (tiles.Contains(hitTile))
                {
                    ActionStack.Main.PushAction(new MovePieceAction(GamePiece, hitTile));
                    done = true;
                }
            }

        }
    }

    public override bool IsDone()
    {
        return done;
    }
}
