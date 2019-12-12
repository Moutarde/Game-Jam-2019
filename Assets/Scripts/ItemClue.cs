using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemClue
{
    public bool m_truthness; // true if the character to find wear the object described by m_text, false otherwise
    public string m_text;

    public ItemClue(bool _truthness, string _text)
    {
        m_truthness = _truthness;
        m_text = _text;
    }
}
