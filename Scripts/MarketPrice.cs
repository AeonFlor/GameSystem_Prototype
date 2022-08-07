using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketPrice : MonoBehaviour
{
    // ���� �� ������Ʈ
    private Button updateButton, addItemButton;
    private List<GameObject> plortItemList;
    private ScrollRect itemScrollRect;
    public GameObject itemPrefab;

    // ��ũ��Ʈ �� ����
    public float minPrice, maxPrice;
    private float marketPower, afterMarketPower;
    private float perlinNoise, perlinNoiseLerp;
    private float currentPosition = 0f;
    private float variance;
    private int itemCount = 0;

    private void Start()
    {
        // �⺻ ���� ����
        marketPower = 1f;
        // ���� ������ �󸶳� �ް��ϰ� ��ȭ�� �� �����ϴ� ����
        variance = 0.1f;

        updateButton = GameObject.Find("Update Button").GetComponent<Button>();
        updateButton.onClick.AddListener(UpdateMarket);

        addItemButton = GameObject.Find("Add Item Button").GetComponent<Button>();
        addItemButton.onClick.AddListener(AddItem);

        itemScrollRect = GameObject.Find("ItemList").GetComponent<ScrollRect>();

        plortItemList = new List<GameObject>();
    }

    private void UpdateMarket()
    {
        // ���� �������� �ٸ��� ������ �����ǵ��� update���� �����ϰ� variance ����  
        variance = Random.Range(0.1f, 0.2f);
        currentPosition += variance;

        perlinNoise = Mathf.PerlinNoise(currentPosition, 0);

        // 0~1���� perlinNoise ���� +-30%�� ���� ���� ó����.
        perlinNoiseLerp = Mathf.Lerp(0.7f, 1.3f, perlinNoise);

        afterMarketPower = marketPower * perlinNoiseLerp;

        Debug.LogFormat("=======[���� ���� : {0:0.00}]=======", afterMarketPower);

        foreach (var plortItem in plortItemList)
        {
            plortItem.GetComponent<PlortPrice>().Updateplort(afterMarketPower);
        }
    }

    private void AddItem()
    {
        GameObject item = Instantiate(itemPrefab, itemScrollRect.content.transform);

        item.GetComponent<PlortPrice>().SetPlortItem(++itemCount, minPrice, maxPrice);

        plortItemList.Add(item);
    }
}
