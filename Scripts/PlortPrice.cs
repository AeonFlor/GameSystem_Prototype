using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlortPrice : MonoBehaviour
{
    // ���� �� ������Ʈ
    private Text plortNameText, priceText;
    private Image statusImage;

    // ��ũ��Ʈ �� ����
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

        // �⺻ ����
        beforePrice = basePrice;
    }

    public void Updateplort(float marketPower)
    {
        // ���� �������� �ٸ��� ������ �����ǵ��� update���� �����ϰ� variance ����  
        variance = Random.Range(0.01f, 0.2f);
        currentPosition += variance;

        perlinNoise = Mathf.PerlinNoise(currentPosition, 0);

        // 0~1���� perlinNoise ���� +-30%�� ���� ���� ó����.
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

        plortNameText.text = "�÷�Ʈ " + itemCount.ToString();
        priceText.text = basePrice.ToString();
        statusImage.sprite = Resources.Load<Sprite>("keep");
    }
}
