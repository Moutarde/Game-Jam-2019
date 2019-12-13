using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(SpriteRenderer))]
public class SuspectController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_character;

    [SerializeField]
    private CharacterMove m_mover;

    public bool IsTarget { get; set; }

    private SpriteRenderer m_spriteRenderer;
    private BoxCollider2D m_boxCollider2D;

    // Start is called before the first frame update
    void Awake()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_spriteRenderer.enabled = false;
        m_boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnKilled()
    {
        m_character.SetActive(false);
        m_spriteRenderer.enabled = true;
        m_boxCollider2D.enabled = false;

        m_mover.m_speed = 0;
        m_mover.GetComponent<BoxCollider2D>().enabled = false;
    }
}
