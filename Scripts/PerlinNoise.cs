using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerlinNoise : MonoBehaviour
{
    // ���� �� ������Ʈ
    private Button updateButton;
    private Text priceText;
    private Image statusImage;

    // ��ũ��Ʈ �� ����
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

        // �⺻ ����
        beforePrice = price;

        updateButton.onClick.AddListener(UpdatePrice);
    }

    private void UpdatePrice()
    {
        // ���� �������� �ٸ��� ������ �����ǵ��� update���� �����ϰ� variance ����  
        variance = Random.Range(0.01f, 0.2f);
        currentPosition += variance;


        perlinNoise = Mathf.PerlinNoise(currentPosition, 0);

        // 0~1���� perlinNoise ���� +-30%�� ���� ���� ó����.
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
