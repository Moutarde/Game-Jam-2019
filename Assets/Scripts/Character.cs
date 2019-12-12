using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private TopItem m_top;
    public GameObject m_trunkAnchor;
    private int m_trunkOrder;
    public GameObject m_topLAnchor;
    private int m_topLOrder;
    public GameObject m_topRAnchor;
    private int m_topROrder;
    public GameObject m_botLAnchor;
    private int m_botLOrder;
    public GameObject m_botRAnchor;
    private int m_botROrder;

    public void AssignTop(TopItem _top)
    {
        m_top = _top;
        m_trunkAnchor.GetComponent<SpriteRenderer>().sprite = m_top.m_trunkSprite;
        m_topLAnchor.GetComponent<SpriteRenderer>().sprite = m_top.m_topLsprite;
        m_topRAnchor.GetComponent<SpriteRenderer>().sprite = m_top.m_topRsprite;
        m_botLAnchor.GetComponent<SpriteRenderer>().sprite = m_top.m_botLsprite;
        m_botRAnchor.GetComponent<SpriteRenderer>().sprite = m_top.m_botRsprite;
    }

    private BottomItem m_bottom;
    public GameObject m_hipsAnchor;
    private int m_hipsOrder;
    public GameObject m_bottomLegTopLAnchor;
    private int m_bottomLegTopOrder;
    public GameObject m_bottomLegTopRAnchor;
    private int m_bottomLegTopROrder;
    public GameObject m_bottomLegBotLAnchor;
    private int m_bottomLegBotLOrder;
    public GameObject m_bottomLegBotRAnchor;
    private int m_bottomLegBotROrder;
    public GameObject m_shoesLAnchor;
    private int m_shoesLOrder;
    public GameObject m_shoesRAnchor;
    private int m_shoesROrder;
    public void AssignBottom(BottomItem _bottom)
    {
        m_bottom = _bottom;
        m_hipsAnchor.GetComponent<SpriteRenderer>().sprite = m_bottom.m_hipsSprite;
        m_bottomLegTopLAnchor.GetComponent<SpriteRenderer>().sprite = m_bottom.m_bottomLegTopLSprite;
        m_bottomLegTopRAnchor.GetComponent<SpriteRenderer>().sprite = m_bottom.m_bottomLegTopRSprite;
        m_bottomLegBotLAnchor.GetComponent<SpriteRenderer>().sprite = m_bottom.m_bottomLegBotLSprite;
        m_bottomLegBotRAnchor.GetComponent<SpriteRenderer>().sprite = m_bottom.m_bottomLegBotRSprite;
        m_shoesLAnchor.GetComponent<SpriteRenderer>().sprite = m_bottom.m_shoesLSprite;
        m_shoesRAnchor.GetComponent<SpriteRenderer>().sprite = m_bottom.m_shoesRSprite;
    }

    private SimpleItem m_head;
    public GameObject m_headAnchor;
    private int m_headOrder;
    public void AssignHead(SimpleItem _head)
    {
        m_head = _head;
        m_headAnchor.GetComponent<SpriteRenderer>().sprite = m_head.m_sprite;
    }

    private SimpleItem m_face;
    public GameObject m_faceAnchor;
    private int m_faceOrder;
    public void AssignFace(SimpleItem _face)
    {
        m_face = _face;
        m_faceAnchor.GetComponent<SpriteRenderer>().sprite = m_face.m_sprite;
    }

    private SimpleItem m_headAccessory;
    public GameObject m_headAccessoryAnchor;
    private int m_headAccessoryOrder;
    public void AssignHeadAccessory(SimpleItem _headAccessory)
    {
        m_headAccessory = _headAccessory;
        m_headAccessoryAnchor.GetComponent<SpriteRenderer>().sprite = m_headAccessory.m_sprite;
    }

    private SimpleItem m_faceAccessory;
    public GameObject m_faceAccessoryAnchor;
    private int m_faceAccessoryOrder;
    public void AssignFaceAccessory(SimpleItem _faceAccessory)
    {
        m_faceAccessory = _faceAccessory;
        m_faceAccessoryAnchor.GetComponent<SpriteRenderer>().sprite = m_faceAccessory.m_sprite;
    }

    public void InitOrders()
    {
        m_trunkOrder = m_trunkAnchor.GetComponent<SpriteRenderer>().sortingOrder;
        m_topLOrder = m_topLAnchor.GetComponent<SpriteRenderer>().sortingOrder;
        m_topROrder = m_topRAnchor.GetComponent<SpriteRenderer>().sortingOrder;
        m_botLOrder = m_botLAnchor.GetComponent<SpriteRenderer>().sortingOrder;
        m_botROrder = m_botRAnchor.GetComponent<SpriteRenderer>().sortingOrder;
        
        m_hipsOrder = m_hipsAnchor.GetComponent<SpriteRenderer>().sortingOrder;
        m_bottomLegTopOrder = m_bottomLegTopLAnchor.GetComponent<SpriteRenderer>().sortingOrder;
        m_bottomLegTopROrder = m_bottomLegTopRAnchor.GetComponent<SpriteRenderer>().sortingOrder;
        m_bottomLegBotLOrder = m_bottomLegBotLAnchor.GetComponent<SpriteRenderer>().sortingOrder;
        m_bottomLegBotROrder = m_bottomLegBotRAnchor.GetComponent<SpriteRenderer>().sortingOrder;
        m_shoesLOrder = m_shoesLAnchor.GetComponent<SpriteRenderer>().sortingOrder;
        m_shoesROrder = m_shoesRAnchor.GetComponent<SpriteRenderer>().sortingOrder;

        m_headOrder = m_headAnchor.GetComponent<SpriteRenderer>().sortingOrder;
        m_faceOrder = m_faceAnchor.GetComponent<SpriteRenderer>().sortingOrder;
        m_headAccessoryOrder = m_headAccessoryAnchor.GetComponent<SpriteRenderer>().sortingOrder;
        m_faceAccessoryOrder = m_faceAccessoryAnchor.GetComponent<SpriteRenderer>().sortingOrder;
    }

    public void SetSpritesOrderInLayer(int _order)
    {
        m_trunkAnchor.GetComponent<SpriteRenderer>().sortingOrder = m_trunkOrder + _order;
        m_topLAnchor.GetComponent<SpriteRenderer>().sortingOrder = m_topLOrder + _order;
        m_topRAnchor.GetComponent<SpriteRenderer>().sortingOrder = m_topROrder + _order;
        m_botLAnchor.GetComponent<SpriteRenderer>().sortingOrder = m_botLOrder + _order;
        m_botRAnchor.GetComponent<SpriteRenderer>().sortingOrder = m_botROrder + _order;

        m_hipsAnchor.GetComponent<SpriteRenderer>().sortingOrder = m_hipsOrder + _order;
        m_bottomLegTopLAnchor.GetComponent<SpriteRenderer>().sortingOrder = m_bottomLegTopOrder + _order;
        m_bottomLegTopRAnchor.GetComponent<SpriteRenderer>().sortingOrder = m_bottomLegTopROrder + _order;
        m_bottomLegBotLAnchor.GetComponent<SpriteRenderer>().sortingOrder = m_bottomLegBotLOrder + _order;
        m_bottomLegBotRAnchor.GetComponent<SpriteRenderer>().sortingOrder = m_bottomLegBotROrder + _order;
        m_shoesLAnchor.GetComponent<SpriteRenderer>().sortingOrder = m_shoesLOrder + _order;
        m_shoesRAnchor.GetComponent<SpriteRenderer>().sortingOrder = m_shoesROrder + _order;

        m_headAnchor.GetComponent<SpriteRenderer>().sortingOrder = m_headOrder + _order;
        m_faceAnchor.GetComponent<SpriteRenderer>().sortingOrder = m_faceOrder + _order;
        m_headAccessoryAnchor.GetComponent<SpriteRenderer>().sortingOrder = m_headAccessoryOrder + _order;
        m_faceAccessoryAnchor.GetComponent<SpriteRenderer>().sortingOrder = m_faceAccessoryOrder + _order;
    }

}
