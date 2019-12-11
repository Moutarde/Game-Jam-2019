using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private TopItem m_top;
    public GameObject m_topAnchor0;
    public GameObject m_topAnchor1;
    public GameObject m_topAnchor2;
    public GameObject m_topAnchor3;
    public GameObject m_topAnchor4;
    public void AssignTop(TopItem _top)
    {
        m_top = _top;
        m_topAnchor0.GetComponent<SpriteRenderer>().sprite = m_top.m_sprite0;
        /*m_topAnchor1.GetComponent<SpriteRenderer>().sprite = m_top.m_sprite1;
        m_topAnchor2.GetComponent<SpriteRenderer>().sprite = m_top.m_sprite2;
        m_topAnchor3.GetComponent<SpriteRenderer>().sprite = m_top.m_sprite3;
        m_topAnchor4.GetComponent<SpriteRenderer>().sprite = m_top.m_sprite4;*/
    }

    private BottomItem m_bottom;
    public GameObject m_bottomAnchor0;
    public GameObject m_bottomAnchor1;
    public GameObject m_bottomAnchor2;
    public GameObject m_bottomAnchor3;
    public GameObject m_bottomAnchor4;
    public void AssignBottom(BottomItem _bottom)
    {
        m_bottom = _bottom;
        m_bottomAnchor0.GetComponent<SpriteRenderer>().sprite = m_bottom.m_sprite0;
        m_bottomAnchor1.GetComponent<SpriteRenderer>().sprite = m_bottom.m_sprite1;
        /*m_bottomAnchor2.GetComponent<SpriteRenderer>().sprite = m_bottom.m_sprite2;
        m_bottomAnchor3.GetComponent<SpriteRenderer>().sprite = m_bottom.m_sprite3;
        m_bottomAnchor4.GetComponent<SpriteRenderer>().sprite = m_bottom.m_sprite4;*/
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

    private SimpleItem m_facialHair;
    public GameObject m_facialHairAnchor;
    public void AssignFacialHair(SimpleItem _facialHair)
    {
        m_facialHair = _facialHair;
        m_facialHairAnchor.GetComponent<SpriteRenderer>().sprite = m_facialHair.m_sprite;
    }

    private SimpleItem m_hair;
    public GameObject m_hairAnchor;
    public void AssignHair(SimpleItem _hair)
    {
        m_hair = _hair;
        m_hairAnchor.GetComponent<SpriteRenderer>().sprite = m_hair.m_sprite;
    }

    private SimpleItem m_faceAccessory;
    public GameObject m_faceAccessoryAnchor;
    public void AssignFaceAccessory(SimpleItem _faceAccessory)
    {
        m_faceAccessory = _faceAccessory;
        m_faceAccessoryAnchor.GetComponent<SpriteRenderer>().sprite = m_faceAccessory.m_sprite;
    }

    private SimpleItem m_headAccessory;
    public GameObject m_headAccessoryAnchor;
    public void AssignHeadAccessory(SimpleItem _headAccessory)
    {
        m_headAccessory = _headAccessory;
        m_headAccessoryAnchor.GetComponent<SpriteRenderer>().sprite = m_headAccessory.m_sprite;
    }
}
