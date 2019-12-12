using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersSpawner : MonoBehaviour
{
    public class Node
    {
        public Item.ItemType m_itemType;
        public int m_depth;
        public Node m_node1;
        public Node m_node2;

        public Node(Item.ItemType _itemType, Item _item0, Item _item1, int _depth, List<Item.ItemType> _availableTypes)
        {
            /*if (m_depth > 3)
            {
                m_node1 = null;
                m_node2 = null;
            }
            else
            {
                int randomNodeIndex = Random.Range(0, 2);
                if (randomNodeIndex == 0)
                {
                    m_node1 = new Node(_availableTypes, m_depth + 1);
                    m_node2 = null;
                }
                else
                {
                    m_node1 = null;
                    m_node2 = new Node(_availableTypes, m_depth + 1);
                }
            }

            int randomTypeIndex = Random.Range(0, _availableTypes.Count);
            m_itemType = _availableTypes[randomTypeIndex];
            _availableTypes.RemoveAt(randomTypeIndex);
            */
            
        }

        void CreateRoot(List<Item.ItemType> _availableTypes, ItemsDatabase _itemsDatabase)
        {
            m_itemType = Item.ItemType.Top; // random, do not use it
            m_depth = 0;

            int randomTypeIndex = Random.Range(0, _availableTypes.Count);
            Item.ItemType childrenItemType = _availableTypes[randomTypeIndex];
            _availableTypes.RemoveAt(randomTypeIndex);

            List<Item> items = _itemsDatabase.GetItems(childrenItemType);
            int itemIndex = Random.Range(0, items.Count - 1);
            int item0 = itemIndex;
            int item1 = itemIndex + 1;

            int randomNodeIndex = Random.Range(0, 2);
            if (randomNodeIndex == 0)
            {
                m_node1 = new Node(childrenItemType, items[item0], items[item1], m_depth + 1, _availableTypes);
                m_node2 = null;
            }
            else
            {
                m_node1 = null;
                m_node2 = new Node(childrenItemType, items[item0], items[item1], m_depth + 1, _availableTypes);
            }
        }
    }


    public GameObject m_characterPrefab;
    public float m_xMin;
    public float m_xMax;
    public float m_yMin;
    public float m_yMax;
    public GameObject m_zManager;

    private ItemsDatabase m_itemsDatabase;

    private void SpawnCharacter()
    {
        GameObject newCharacter = Instantiate(m_characterPrefab);
        newCharacter.transform.parent = transform;
        float randomX = Random.Range(m_xMin, m_xMax);
        float randomY = Random.Range(m_yMin, m_yMax);
        newCharacter.transform.localPosition = new Vector3(randomX, randomY, 0.0f);
        m_zManager.GetComponent<ZManager>().m_playersAndObstacles.Add(newCharacter);

        List<Item> topList = m_itemsDatabase.GetItems(Item.ItemType.Top);
        int topIndex = Random.Range(0, topList.Count);
        TopItem top = (TopItem)topList[topIndex];
        newCharacter.GetComponent<Character>().AssignTop(top);

        List<Item> bottomList = m_itemsDatabase.GetItems(Item.ItemType.Bottom);
        int bottomIndex = Random.Range(0, bottomList.Count);
        BottomItem bottom = (BottomItem)bottomList[bottomIndex];
        newCharacter.GetComponent<Character>().AssignBottom(bottom);

        List<Item> headList = m_itemsDatabase.GetItems(Item.ItemType.Head);
        int headIndex = Random.Range(0, headList.Count);
        SimpleItem head = (SimpleItem)headList[headIndex];
        newCharacter.GetComponent<Character>().AssignHead(head);

        List<Item> hairList = m_itemsDatabase.GetItems(Item.ItemType.Hair);
        int hairIndex = Random.Range(0, hairList.Count);
        SimpleItem hair = (SimpleItem)hairList[hairIndex];
        newCharacter.GetComponent<Character>().AssignHair(hair);

        List<Item> faceAccessoryList = m_itemsDatabase.GetItems(Item.ItemType.FaceAccessory);
        int faceAccessoryIndex = Random.Range(0, faceAccessoryList.Count);
        SimpleItem faceAccessory = (SimpleItem)faceAccessoryList[faceAccessoryIndex];
        newCharacter.GetComponent<Character>().AssignFaceAccessory(faceAccessory);
    }

    private List<ItemClue> SpawnCharacters(int _charactersCount)
    {
        for (int i = 0; i < _charactersCount; ++i)
        {
            SpawnCharacter();
        }

        List<ItemClue> clues = new List<ItemClue>();
        clues.Add(new ItemClue(true, "Clue1"));
        clues.Add(new ItemClue(false, "Clue2"));
        clues.Add(new ItemClue(true, "Clue3"));
        clues.Add(new ItemClue(false, "Clue4"));
        clues.Add(new ItemClue(true, "Clue5"));
        clues.Add(new ItemClue(false, "Clue6"));
        clues.Add(new ItemClue(true, "Clue7"));
        return clues;
    }

    private void CreateTree()
    {
        List<Item.ItemType> availableTypes = new List<Item.ItemType> { Item.ItemType.Top, Item.ItemType.Bottom, Item.ItemType.Head, Item.ItemType.Face, Item.ItemType.Hair, Item.ItemType.FaceAccessory, Item.ItemType.HeadAccessory };
        //Node root = new Node(availableTypes, 0);

    }

    // Start is called before the first frame update
    void Start()
    {
        m_itemsDatabase = new ItemsDatabase();
        m_itemsDatabase.Init();

        SpawnCharacters(30);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
