using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Clue : MonoBehaviour
{
    TextMeshProUGUI m_textMesh;

    [SerializeField]
    Sprite m_tickSprite;
    [SerializeField]
    Sprite m_crossSprite;
    [SerializeField]
    Image m_icon;

    // Start is called before the first frame update
    void Start()
    {
        m_textMesh = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetValue(string value, bool isPositive)
    {
        m_textMesh.text = value;
        m_icon.sprite = isPositive ? m_tickSprite : m_crossSprite;
    }

    public void ShowClue(bool value)
    {
        gameObject.SetActive(value);
    }
}
