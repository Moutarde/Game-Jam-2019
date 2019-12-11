using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsDatabase : MonoBehaviour
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

    void FillDatabaseWithComplexItems(Item.ItemType _itemType, string _folderName)
    {
        m_database[_itemType] = new List<Item>();
        object[] items = Resources.LoadAll("Items/" + _folderName, typeof(Item));
        for (int i = 0; i < items.Length; ++i)
        {
            m_database[_itemType].Add((ComplexItem)items[i]);
        }
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

    // Start is called before the first frame update
    void Start()
    {
        m_database = new Dictionary<Item.ItemType, List<Item>>();
        FillDatabaseWithSimpleItems(Item.ItemType.Type0, "ItemType0");
        FillDatabaseWithSimpleItems(Item.ItemType.Type1, "ItemType1");
        FillDatabaseWithComplexItems(Item.ItemType.Type2, "ItemType2");

        DisplayItemsList(Item.ItemType.Type0, "ItemType0");
        DisplayItemsList(Item.ItemType.Type1, "ItemType1");
        DisplayItemsList(Item.ItemType.Type2, "ItemType2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
