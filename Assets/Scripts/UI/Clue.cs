using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Clue : MonoBehaviour
{

    public TextMeshProUGUI m_textMesh;

    [SerializeField]
    Sprite m_tickSprite;
    [SerializeField]
    Sprite m_crossSprite;
    [SerializeField]
    Image m_icon;

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
