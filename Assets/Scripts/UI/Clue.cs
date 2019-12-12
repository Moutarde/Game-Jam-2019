﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clue : MonoBehaviour
{
    TextMeshProUGUI m_textMesh;

    // Start is called before the first frame update
    void Start()
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

    public void ShowClue(bool value)
    {
        gameObject.SetActive(value);
    }
}