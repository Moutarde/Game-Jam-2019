using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    public enum ItemType
    {
        Top,
        Bottom,
        Head,
        Face,
        HeadAccessory,
        FaceAccessory
    };

    public ItemType m_itemType;
    public List<ItemClue> m_clues;
}
