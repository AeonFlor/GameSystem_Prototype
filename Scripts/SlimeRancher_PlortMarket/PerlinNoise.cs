using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerlinNoise : MonoBehaviour
{
    // 게임 내 오브젝트
    private Button updateButton;
    private Text priceText;
    private Image statusImage;

    // 스크립트 내 변수
    public int price;
    private int beforePrice, afterPrice;
    private float perlinNoise, perlinNoiseLerp;
    public float currentPosition = 0f;
    public float variance;

    private void Start()
    {
        updateButton = GameObject.Find("Update Button").GetComponent<Button>();
        priceText = this.transform.Find("Price").gameObject.GetComponent<Text>();
        statusImage = this.transform.Find("Status").gameObject.GetComponent<Image>();

        // 기본 가격
        beforePrice = price;

        updateButton.onClick.AddListener(UpdatePrice);
    }

    private void UpdatePrice()
    {
        // 여러 아이템이 다르게 가격이 설정되도록 update마다 랜덤하게 variance 설정  
        variance = Random.Range(0.01f, 0.2f);
        currentPosition += variance;


        perlinNoise = Mathf.PerlinNoise(currentPosition, 0);

        // 0~1사이 perlinNoise 값을 +-30%로 선형 보간 처리함.
        perlinNoiseLerp = Mathf.Lerp(0.7f, 1.3f, perlinNoise);

        afterPrice = Mathf.RoundToInt(price * perlinNoiseLerp);

        Debug.Log(currentPosition + " : " + perlinNoiseLerp);


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
}
