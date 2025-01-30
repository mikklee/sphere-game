using UnityEngine;
using System.Collections;
using System;

public class Inventory : MonoBehaviour {
    private Item[] inventory = new Item[Enum.GetValues(typeof(Items)).Length]; //Make an array which may potentially contain all game items. 
    public GameObject stateManager;
    private stateManagerObj stMgr;

    void Awake(){
        stMgr = stateManager.GetComponent<stateManagerObj>();
        for (int i = 0; i < Enum.GetValues(typeof(Items)).Length; i++){
            inventory.SetValue(new Item(Items.Zero, 0, 0, false),i);
        }
        //fil array with empty Item objects
    }

    void FixedUpdate() {
        foreach (Item itm in inventory) {
            if (itm.active) itm.tickTime(Time.deltaTime); 
        }
    }

    public string getTime(Items itemType)
    {
        foreach (Item itm in inventory)
        {
            if (itm.itemType == itemType)
            {
                if (itm.getTimeLeft() <= 0 && itm.active)
                {
                    stMgr.setState(States.Normal);
                    itm.active = false;
                }
                return "00:" + Mathf.Abs(itm.getTimeLeft());
            }
        }
        return "00:00";
    }

    public string getCount(Items itemType)
    {
        foreach (Item itm in inventory)
        {
            if (itm.itemType == itemType)
            {
                return "" + itm.getUseCount();
            }
        }
        return "0";
    }

    public bool tryPauseItem(Items itemType)
    {
        foreach (Item itm in inventory)
        {
            if (itm.itemType == itemType && itm.active)
            {
                itm.active = false;
                return true;
            }
        }
        return false;
    }

    public bool addItem(Items itemType, float usageTime, int useCount, bool isCountOnly) {
        Item item = new Item(itemType, usageTime, useCount, isCountOnly);
        if (tryStack(item)) {
            return true;
        }
        else {
            for (int i = 0;  i < inventory.Length; i++) {
                if (inventory[i].itemType == Items.Zero) {
                    inventory.SetValue(item, i);
                    return true;
                }
            }
            //Add item if the item already isn't in the Inventory. Consume if the inventory is full.
           }
        return false; //What happened here?
    }

    //Tries to stack the item if it already exists
    private bool tryStack(Item item) {
        foreach (Item itm in inventory)
        { //Check if item already exists
            if (itm.itemType == item.itemType)
            {
                itm.addCount(); //Stack item instead of adding
                print("Stacked Item");
                return true;
            }
        }
        return false;
    }

    public bool useItemType(Items itemType)
    {
        foreach (Item itm in inventory)
        { //Check if item exists
            if (itm.itemType == itemType)//Check if item exists
            {
                if (consumeItem(itm)) return true; //Try to consume item charge
            }
        }
        return false;
    }

    private bool consumeItem(Item itm)
    {
        if (itm.getUseCount() > 0)
        {
            itm.consumeCount();
            return true;
        }
        else if (itm.getTimeLeft() > 0) {
            itm.active = true; //activate timer
            return true;
        }
        return false;
    }

}
