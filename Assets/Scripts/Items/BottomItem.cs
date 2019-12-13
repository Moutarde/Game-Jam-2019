using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item/Bottom", order = 2)]
public class BottomItem : Item
{
    public Sprite m_hipsSprite;
    public Sprite m_bottomLegTopLSprite;
    public Sprite m_bottomLegTopRSprite;
    public Sprite m_bottomLegBotLSprite;
    public Sprite m_bottomLegBotRSprite;
    public Sprite m_shoesLSprite;
    public Sprite m_shoesRSprite;

    public bool IsSameItem(BottomItem _bottomItem)
    {
        return m_hipsSprite == _bottomItem.m_hipsSprite;
    }
}
