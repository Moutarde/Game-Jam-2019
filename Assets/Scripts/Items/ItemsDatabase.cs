using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsDatabase
{
    public Dictionary<Item.ItemType, List<Item>> m_database;

    void FillDatabaseWithSimpleItems(Item.ItemType _itemType, string _folderName)
    {
        m_database[_itemType] = new List<Item>();
        object[] items = Resources.LoadAll("Items/" + _folderName, typeof(Item));
        for (int i = 0; i < items.Length; ++i)
        {
            m_database[_itemType].Add((SimpleItem)items[i]);
        }
    }

    void FillDatabaseWithTopItems(string _folderName)
    {
        m_database[Item.ItemType.Top] = new List<Item>();
        object[] items = Resources.LoadAll("Items/" + _folderName, typeof(Item));
        for (int i = 0; i < items.Length; ++i)
        {
            m_database[Item.ItemType.Top].Add((TopItem)items[i]);
        }
    }

    void FillDatabaseWithBottomItems(string _folderName)
    {
        m_database[Item.ItemType.Bottom] = new List<Item>();
        object[] items = Resources.LoadAll("Items/" + _folderName, typeof(Item));
        for (int i = 0; i < items.Length; ++i)
        {
            m_database[Item.ItemType.Bottom].Add((BottomItem)items[i]);
        }
    }

    public List<Item> GetItems(Item.ItemType _itemType)
    {
        return m_database[_itemType];
    }

    // Debug
    void DisplayItemsList(Item.ItemType _itemType, string _itemTypeName)
    {
        List<Item> items = m_database[_itemType];
        Debug.Log(_itemTypeName + " : " + items.Count);
        for (int i = 0; i < items.Count; ++i)
        {
            Debug.Log(items[i]);
        }
    }

    public void Init()
    {
        m_database = new Dictionary<Item.ItemType, List<Item>>();

        FillDatabaseWithTopItems("Top");
        FillDatabaseWithBottomItems("Bottom");
        FillDatabaseWithSimpleItems(Item.ItemType.Head, "Head");
        FillDatabaseWithSimpleItems(Item.ItemType.Hair, "Hair");
        FillDatabaseWithSimpleItems(Item.ItemType.FacialHair, "HairFace");
    }
}
