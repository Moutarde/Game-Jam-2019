using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZManager : MonoBehaviour
{
    public List<GameObject> m_playersAndObstacles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RemoveAllCharacters()
    {
        m_playersAndObstacles.RemoveAll(obstacle => obstacle.CompareTag("Character"));
    }

    int SortByZ(GameObject _go1, GameObject _go2)
    {
        Vector3 localPosition1 = _go1.transform.localPosition;
        Vector2 colliderOffset1 = _go1.GetComponent<BoxCollider2D>().offset;
        Vector3 colliderPosition1 = localPosition1 + new Vector3(colliderOffset1.x, colliderOffset1.y, 0.0f);

        Vector3 localPosition2 = _go2.transform.localPosition;
        Vector2 colliderOffset2 = _go2.GetComponent<BoxCollider2D>().offset;
        Vector3 colliderPosition2 = localPosition2 + new Vector3(colliderOffset2.x, colliderOffset2.y, 0.0f);

        return colliderPosition1.y.CompareTo(colliderPosition2.y);
    }

    void SetSpritesOrderInLayer(GameObject _gameObject, int _order)
    {
        SpriteRenderer[] spriteRenderers = _gameObject.GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; i < spriteRenderers.Length; ++i)
        {
            spriteRenderers[i].sortingOrder = _order;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_playersAndObstacles.Count > 0)
        {
            m_playersAndObstacles.Sort(SortByZ);
        }

        /*Debug.Log("List");
        for (int i = 0; i < m_playersAndObstacles.Count; ++i)
        {
            Vector3 localPosition1 = m_playersAndObstacles[i].transform.localPosition;
            Vector2 colliderOffset1 = m_playersAndObstacles[i].GetComponent<BoxCollider2D>().offset;
            Vector3 colliderPosition1 = localPosition1 + new Vector3(colliderOffset1.x, colliderOffset1.y, 0.0f);
            Debug.Log(m_playersAndObstacles[i]);
            Debug.Log(colliderPosition1.y);
        }*/

        /*float z = -50;
        for (int i = 0; i < m_playersAndObstacles.Count; ++i)
        {
            Vector3 localPosition = m_playersAndObstacles[i].transform.localPosition;
            m_playersAndObstacles[i].transform.localPosition = new Vector3(localPosition.x, localPosition.y, z);
            //Debug.Log(m_playersAndObstacles[i].transform.localPosition);
            ++z;
        }*/

        int order = 100;
        for (int i = 0; i < m_playersAndObstacles.Count; ++i)
        {
            SetSpritesOrderInLayer(m_playersAndObstacles[i], order);
            --order;
        }
    }
}
