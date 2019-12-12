using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class PlayerScore : MonoBehaviour
{
    TextMeshProUGUI m_textMesh;

    // Start is called before the first frame update
    void Awake()
    {
        m_textMesh = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetText(string value)
    {
        m_textMesh.text = value;
    }

    public void ShowScore(bool value)
    {
        gameObject.SetActive(value);
    }
}
