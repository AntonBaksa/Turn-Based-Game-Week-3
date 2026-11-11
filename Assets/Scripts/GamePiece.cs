using UnityEngine;

public abstract class GamePiece : ActionStack.Action
{
    public abstract class Action : ActionStack.Action
    {
        private GamePiece piece;

        public GamePiece GamePiece => piece;
    }
}
