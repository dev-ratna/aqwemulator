using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AQWE.Game.Managers
{
    public static class ItemManager
    {
        public static Dictionary<int, Item> Items = new Dictionary<int, Item>();

        public static Item getInstance(int ItemID)
        {
            if (Items.ContainsKey(ItemID))
                return (Item)Items[ItemID];
            else
                throw new Exception("Item id: " + ItemID + " doesn't exist");
        }

        public static void Add(int ItemID, Item ItemClass)
        {
            if (!Items.ContainsKey(ItemID))
                Items.Add(ItemID, ItemClass);
        }

        public static void Remove(int ItemID)
        {
            if (Items.ContainsKey(ItemID))
                Items.Remove(ItemID);
        }
    }
}
