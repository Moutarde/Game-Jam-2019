using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item/SimpleItem", order = 1)]
public class SimpleItem : Item
{
    public Sprite m_sprite;

    public bool IsSameItem(SimpleItem _simpleItem)
    {
        return m_sprite == _simpleItem.m_sprite;
    }
}
