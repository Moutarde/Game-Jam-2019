using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    public enum ItemType
    {
        Top,
        Bottom,
        Head,
        Face,
        HeadAccessory,
        FaceAccessory
    };

    public ItemType m_itemType;
    public List<ItemClue> m_clues;

    public Item()
    {
        m_itemType = ItemType.Top;
        m_clues = new List<ItemClue>();
    }

    public string GetClue(bool _truthness)
    {
        int index = 0;
        while (m_clues[index].m_truthness != _truthness)
        {
            ++index;
        }
        return m_clues[index].m_text;
    }

    public bool ContainClue(List<string> _clues)
    {
        string clue = GetClue(true);

        for (int i = 0; i < _clues.Count; ++i)
        {
            if (_clues[i] == clue)
            {
                _clues.RemoveAt(i);
                return true;
            }
        }

        return false;
    }

    public void AddClues(List<ItemClue> _clues)
    {
        for (int i = 0; i < m_clues.Count; ++i)
        {
            _clues.Add(m_clues[i]);
        }
    }
}
