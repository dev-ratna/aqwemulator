using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AQWE.Game.Managers
{
    public static class actionManager
    {
        public static Dictionary<int, Action> Actions = new Dictionary<int, Action>();

        public static Action getInstance(int actionID)
        {
            if (Actions.ContainsKey(actionID))
                return (Action)Actions[actionID];
            else
                throw new Exception("Action id: " + actionID + " doesn't exist.");
        }

        public static void Add(int actionID, Action actionClass)
        {
            if (Actions.ContainsKey(actionID))
                Actions.Add(actionID, actionClass);
        }

        public static void Remove(int actionID)
        {
            if (Actions.ContainsKey(actionID))
                Actions.Remove(actionID);
        }
    }
}
