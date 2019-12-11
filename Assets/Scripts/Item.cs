using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    public enum ItemType
    {
        Type0,
        Type1,
        Type2
    };

    public ItemType m_itemType;
}

[CreateAssetMenu(fileName = "Item", menuName = "Item/SimpleItem", order = 1)]
public class SimpleItem : Item
{
    public Sprite m_sprite;
}

[CreateAssetMenu(fileName = "Item", menuName = "Item/ComplexItem", order = 2)]
public class ComplexItem : Item
{
    public Sprite m_sprite0;
    public Sprite m_sprite1;
    public Sprite m_sprite2;
    public Sprite m_sprite3;
}
