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
        FacialHair,
        Hair,
        FaceAccessory,
        HeadAccessory
    };

    public ItemType m_itemType;
}
