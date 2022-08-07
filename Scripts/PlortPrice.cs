using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlortPrice : MonoBehaviour
{
    // 게임 내 오브젝트
    private Text plortNameText, priceText;
    private Image statusImage;

    // 스크립트 내 변수
    private int basePrice = 0;
    private int beforePrice, afterPrice;
    private float perlinNoise, perlinNoiseLerp;
    private float currentPosition = 0f;
    private float variance;

    private void Awake()
    {
        plortNameText = this.transform.Find("Name").gameObject.GetComponent<Text>();
        priceText = this.transform.Find("Price").gameObject.GetComponent<Text>();
        statusImage = this.transform.Find("Status").gameObject.GetComponent<Image>();

        // 기본 가격
        beforePrice = basePrice;
    }

    public void Updateplort(float marketPower)
    {
        // 여러 아이템이 다르게 가격이 설정되도록 update마다 랜덤하게 variance 설정  
        variance = Random.Range(0.01f, 0.2f);
        currentPosition += variance;

        perlinNoise = Mathf.PerlinNoise(currentPosition, 0);

        // 0~1사이 perlinNoise 값을 +-30%로 선형 보간 처리함.
        perlinNoiseLerp = Mathf.Lerp(0.7f, 1.3f, perlinNoise);

        afterPrice = Mathf.RoundToInt(basePrice * perlinNoiseLerp * marketPower);

        priceText.text = afterPrice.ToString();

        if (afterPrice > beforePrice)
        {
            statusImage.sprite = Resources.Load<Sprite>("raise");
        }

        else if (afterPrice < beforePrice)
        {
            statusImage.sprite = Resources.Load<Sprite>("fall");
        }

        else
        {
            statusImage.sprite = Resources.Load<Sprite>("keep");
        }

        beforePrice = afterPrice;
    }

    public void SetPlortItem(int itemCount, float minPrice, float maxPrice)
    {
        basePrice = Mathf.RoundToInt(Random.Range(minPrice, maxPrice));

        plortNameText.text = "플로트 " + itemCount.ToString();
        priceText.text = basePrice.ToString();
        statusImage.sprite = Resources.Load<Sprite>("keep");
    }
}
