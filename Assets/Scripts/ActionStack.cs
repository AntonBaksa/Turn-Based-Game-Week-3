using System.Collections.Generic;
using UnityEngine;

public class ActionStack : MonoBehaviour
{
    public interface IAction
    {
        void OnBegin(bool bFirstTime);
        void OnUpdate();
        void OnEnd();
        bool IsDone();
    }

    public abstract class Action : IAction
    {
        public virtual void OnBegin(bool bFirstTime) { }
        public virtual void OnUpdate() { }
        public virtual void OnEnd() { }
        public virtual bool IsDone() {  return true; }
    }

    private List<IAction> actionStack = new List<IAction>();
    private HashSet<IAction> firstTimeActions = new HashSet<IAction>();

    private IAction currentAction;

    private static ActionStack main;

    public bool IsEmpty => currentAction == null && actionStack.Count == 0;

    public void PushAction(IAction action)
    {
        if (action != null)
        {
            // remove duplicates
            actionStack.RemoveAll(a => a == action);

            // insert action at 0
            actionStack.Insert(0, action);

            // reset current action
            if (currentAction != null && currentAction != action)
            {
                currentAction = null;
            }
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        UpdateActions();
    }

    void UpdateActions()
    {
        if (IsEmpty) return;

        while (currentAction == null && actionStack.Count > 0)
        {
            currentAction = actionStack[0];

            //is it first time?
            bool bFirstTime = !firstTimeActions.Contains(currentAction);
            firstTimeActions.Add(currentAction);

            currentAction.OnBegin(bFirstTime);

            //did it push another action?
            if (currentAction != null && actionStack.Count > 0 && currentAction != actionStack[0])
            {
                currentAction = null;
                UpdateActions();
                return;
            }
        }

        //update action
        if(currentAction != null)
        {
            currentAction.OnUpdate();

            //still the current action?
            if(actionStack.Count > 0 && currentAction == actionStack[0])
            {
                //is it done?
                if (currentAction.IsDone())
                {
                    actionStack.RemoveAt(0);
                    currentAction.OnEnd();
                    firstTimeActions.Remove(currentAction);
                    currentAction = null;
                }
            }
            else
            {
                currentAction = null;
            }
        }
    }
}
