using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private TopItem m_top;
    public GameObject m_trunkAnchor;
    public GameObject m_topLAnchor;
    public GameObject m_topRAnchor;
    public GameObject m_botLAnchor;
    public GameObject m_botRAnchor;
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
    public GameObject m_bottomLegTopLAnchor;
    public GameObject m_bottomLegTopRAnchor;
    public GameObject m_bottomLegBotLAnchor;
    public GameObject m_bottomLegBotRAnchor;
    public GameObject m_shoesLAnchor;
    public GameObject m_shoesRAnchor;
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
    public void AssignHead(SimpleItem _head)
    {
        m_head = _head;
        m_headAnchor.GetComponent<SpriteRenderer>().sprite = m_head.m_sprite;
    }

    private SimpleItem m_face;
    public GameObject m_faceAnchor;
    public void AssignFace(SimpleItem _face)
    {
        m_face = _face;
        m_faceAnchor.GetComponent<SpriteRenderer>().sprite = m_face.m_sprite;
    }

    private SimpleItem m_headAccessory;
    public GameObject m_headAccessoryAnchor;
    public void AssignHeadAccessory(SimpleItem _headAccessory)
    {
        m_headAccessory = _headAccessory;
        m_headAccessoryAnchor.GetComponent<SpriteRenderer>().sprite = m_headAccessory.m_sprite;
    }

    private SimpleItem m_faceAccessory;
    public GameObject m_faceAccessoryAnchor;
    public void AssignFaceAccessory(SimpleItem _faceAccessory)
    {
        m_faceAccessory = _faceAccessory;
        m_faceAccessoryAnchor.GetComponent<SpriteRenderer>().sprite = m_faceAccessory.m_sprite;
    }
}
