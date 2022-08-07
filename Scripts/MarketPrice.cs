using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketPrice : MonoBehaviour
{
    // 게임 내 오브젝트
    private Button updateButton, addItemButton;
    private List<GameObject> plortItemList;
    private ScrollRect itemScrollRect;
    public GameObject itemPrefab;

    // 스크립트 내 변수
    public float minPrice, maxPrice;
    private float marketPower, afterMarketPower;
    private float perlinNoise, perlinNoiseLerp;
    private float currentPosition = 0f;
    private float variance;
    private int itemCount = 0;

    private void Start()
    {
        // 기본 시장 배율
        marketPower = 1f;
        // 시장 배율이 얼마나 급격하게 변화할 지 설정하는 변수
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
        // 여러 아이템이 다르게 가격이 설정되도록 update마다 랜덤하게 variance 설정  
        variance = Random.Range(0.1f, 0.2f);
        currentPosition += variance;

        perlinNoise = Mathf.PerlinNoise(currentPosition, 0);

        // 0~1사이 perlinNoise 값을 +-30%로 선형 보간 처리함.
        perlinNoiseLerp = Mathf.Lerp(0.7f, 1.3f, perlinNoise);

        afterMarketPower = marketPower * perlinNoiseLerp;

        Debug.LogFormat("=======[마켓 배율 : {0:0.00}]=======", afterMarketPower);

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
