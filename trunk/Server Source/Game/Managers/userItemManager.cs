using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AQWE.Sessions;
using AQWE.Data;

namespace AQWE.Game.Managers
{
    public static class userItemManager
    {
        public static Dictionary<int, Users.Item> Items = new Dictionary<int, Users.Item>();

        public static Users.Item getInstance(int ID)
        {
            if (Items.ContainsKey(ID))
                return (Users.Item)Items[ID];
            else
                throw new Exception("User item id: " + ID + " doesn't exist");
        }

        public static Users.Item getInstanceByItemID(int ItemID)
        {
            foreach (KeyValuePair<int, Users.Item> KVP in Items)
            {
                if (KVP.Value.ItemID == ItemID)
                    return (Users.Item)KVP.Value;
            }

            throw new Exception("ItemID: " + ItemID + " doesn't exist in user items.");
        }

        public static void loadItems(int userID)
        {
            int[] itemIDs = Database.runReadColumnIntegers("SELECT id FROM user_items WHERE user_id = " + userID, 0);

            foreach (int tempID in itemIDs)
            {
                Users.Item _item = new Users.Item(tempID);
                _item.ItemID = int.Parse(Database.runRead("SELECT item_id FROM user_items WHERE id = " + tempID));
                _item.UserID = int.Parse(Database.runRead("SELECT user_id FROM user_items WHERE id = " + tempID));

                int tempEq = int.Parse(Database.runRead("SELECT equipped FROM user_items WHERE id = " + tempID));
                
                if (tempEq == 1)
                    _item.Equipped = true;
                else
                    _item.Equipped = false;
            }
        }

        public static string getEquippedItems(int userID)
        {
            string returnPacket = "";
            bool first = false;

            foreach (KeyValuePair<int, Users.Item> KVP in Items)
            {
                if (KVP.Value.Equipped && KVP.Value.UserID == userID)
                {
                    if (first)
                        returnPacket += ",";

                    Item _item = ItemManager.getInstance(KVP.Value.ItemID);

                    returnPacket += "\"" + _item.Type + "\":{\"ItemID\":\"" + _item.ID + "\",\"sFile\":\"" + _item.Filename + "\",\"sLink\":\"" + _item.Linkage + "\"}";
                    first = true;
                }
            }

            return returnPacket;
        }

        public static void Add(int ID, Users.Item ItemClass)
        {
            if (!Items.ContainsKey(ID))
                Items.Add(ID, ItemClass);
        }

        public static void Remove(int ID)
        {
            if (Items.ContainsKey(ID))
                Items.Remove(ID);
        }
    }
}
