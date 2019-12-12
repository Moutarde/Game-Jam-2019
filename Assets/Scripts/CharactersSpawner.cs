using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersSpawner : MonoBehaviour
{
    public class Node
    {
        public Item.ItemType m_itemType;
        public Item m_item;
        public int m_depth;
        public Node m_node1;
        public Node m_node2;

        public Node(List<Item.ItemType> _availableTypes, ItemsDatabase _itemsDatabase)
        {
            m_itemType = Item.ItemType.Top; // random, do not use it
            m_item = null;
            m_depth = 0;

            CreateChildrenNodes(_availableTypes, _itemsDatabase);
        }

        public Node(Item.ItemType _itemType, Item _item, bool _hasChildren, int _depth, List<Item.ItemType> _availableTypes, ItemsDatabase _itemsDatabase)
        {
            Debug.Log("depth: " + m_depth);
            Debug.Log("_availableTypes: " + _availableTypes.Count);
            if (m_depth > 3)
            {
                _hasChildren = false;
            }

            if (_hasChildren)
            {
                m_itemType = _itemType;
                m_item = _item;
                m_depth = _depth;

                CreateChildrenNodes(_availableTypes, _itemsDatabase);
            }
            else
            {
                m_node1 = null;
                m_node2 = null;
            }   
        }

        public void CreateChildrenNodes(List<Item.ItemType> _availableTypes, ItemsDatabase _itemsDatabase)
        {
            int randomTypeIndex = Random.Range(0, _availableTypes.Count);
            Item.ItemType childrenItemType = _availableTypes[randomTypeIndex];
            _availableTypes.RemoveAt(randomTypeIndex);

            List<Item> items = _itemsDatabase.GetItems(childrenItemType);
            int itemIndex = Random.Range(0, items.Count - 1);
            int item0 = 0;
            int item1 = 0;

            int randomNodeIndex = Random.Range(0, 2);
            if (randomNodeIndex == 0)
            {
                item0 = itemIndex;
                item1 = itemIndex + 1;
            }
            else
            {
                item0 = itemIndex + 1;
                item1 = itemIndex;
            }

            Debug.Log(childrenItemType);
            Debug.Log(item0);
            Debug.Log(items.Count);
            Debug.Log(m_depth);

            m_node1 = new Node(childrenItemType, items[item0], true, m_depth + 1, _availableTypes, _itemsDatabase);
            m_node2 = new Node(childrenItemType, items[item1], false, m_depth + 1, _availableTypes, _itemsDatabase);
        }

        public void DisplayNode()
        {
            if (m_depth == 0)
            {
                Debug.Log("Root");
            }
            else
            {
                Debug.Log("Node type " + m_itemType + " item " + m_item);
            }

            Debug.Log("node 1");
            if (m_node1 == null)
            {
                Debug.Log("null");
            }
            else
            {
                m_node1.DisplayNode();
            }

            Debug.Log("node 2");
            if (m_node2 == null)
            {
                Debug.Log("null");
            }
            else
            {
                m_node2.DisplayNode();
            }
        }
    }


    public GameObject m_characterPrefab;
    public float m_xMin;
    public float m_xMax;
    public float m_yMin;
    public float m_yMax;
    public ZManager m_zManager;

    private ItemsDatabase m_itemsDatabase;

    private List<GameObject> m_characters = new List<GameObject>();

    private void AssignItem(Item.ItemType _itemType, GameObject _character)
    {
        List<Item> itemsList = m_itemsDatabase.GetItems(_itemType);
        int itemIndex = Random.Range(0, itemsList.Count);
        SimpleItem item = (SimpleItem)itemsList[itemIndex];
        _character.GetComponent<Character>().AssignHead(item);
    }

    private void AssignHead(Item.ItemType _itemType, GameObject _character)
    {
        List<Item> itemsList = m_itemsDatabase.GetItems(_itemType);
        int itemIndex = Random.Range(0, itemsList.Count);
        SimpleItem item = (SimpleItem)itemsList[itemIndex];
        _character.GetComponent<Character>().AssignHead(item);
    }

    private void AssignFace(Item.ItemType _itemType, GameObject _character)
    {
        List<Item> itemsList = m_itemsDatabase.GetItems(_itemType);
        int itemIndex = Random.Range(0, itemsList.Count);
        SimpleItem item = (SimpleItem)itemsList[itemIndex];
        _character.GetComponent<Character>().AssignFace(item);
    }

    private void AssignHeadAccessory(Item.ItemType _itemType, GameObject _character)
    {
        List<Item> itemsList = m_itemsDatabase.GetItems(_itemType);
        int itemIndex = Random.Range(0, itemsList.Count);
        SimpleItem item = (SimpleItem)itemsList[itemIndex];
        _character.GetComponent<Character>().AssignHeadAccessory(item);
    }

    private void AssignFaceAccessory(Item.ItemType _itemType, GameObject _character)
    {
        List<Item> itemsList = m_itemsDatabase.GetItems(_itemType);
        int itemIndex = Random.Range(0, itemsList.Count);
        SimpleItem item = (SimpleItem)itemsList[itemIndex];
        _character.GetComponent<Character>().AssignFaceAccessory(item);
    }

    private void SpawnCharacter()
    {
        GameObject newCharacter = Instantiate(m_characterPrefab);
        newCharacter.transform.parent = transform;
        float randomX = Random.Range(m_xMin, m_xMax);
        float randomY = Random.Range(m_yMin, m_yMax);
        newCharacter.transform.localPosition = new Vector3(randomX, randomY, 0.0f);

        List<Item> topList = m_itemsDatabase.GetItems(Item.ItemType.Top);
        int topIndex = Random.Range(0, topList.Count);
        TopItem top = (TopItem)topList[topIndex];
        newCharacter.GetComponent<Character>().AssignTop(top);

        List<Item> bottomList = m_itemsDatabase.GetItems(Item.ItemType.Bottom);
        int bottomIndex = Random.Range(0, bottomList.Count);
        BottomItem bottom = (BottomItem)bottomList[bottomIndex];
        newCharacter.GetComponent<Character>().AssignBottom(bottom);

        AssignHead(Item.ItemType.Head, newCharacter);
        AssignFace(Item.ItemType.Face, newCharacter);
        AssignHeadAccessory(Item.ItemType.HeadAccessory, newCharacter);
        AssignFaceAccessory(Item.ItemType.FaceAccessory, newCharacter);

        newCharacter.GetComponent<Character>().InitOrders();

        m_characters.Add(newCharacter);
        m_zManager.m_playersAndObstacles.Add(newCharacter);
    }

    public List<ItemClue> SpawnCharacters(int _charactersCount)
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
        /*List<Item.ItemType> availableTypes = new List<Item.ItemType> { Item.ItemType.Top, Item.ItemType.Bottom, Item.ItemType.Face, Item.ItemType.FaceAccessory, Item.ItemType.HeadAccessory };
        Node root = new Node(availableTypes, m_itemsDatabase);
        root.DisplayNode();*/
    }

    // Start is called before the first frame update
    void Start()
    {
        m_itemsDatabase = new ItemsDatabase();
        m_itemsDatabase.Init();
        CreateTree();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset()
    {
        foreach (GameObject c in m_characters)
        {
            Destroy(c);
        }

        m_characters.Clear();
        m_zManager.RemoveAllCharacters();
    }
}
