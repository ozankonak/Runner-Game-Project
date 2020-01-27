using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageScroller : MonoBehaviour
{
    private RawImage image;

    [SerializeField] private float backgroundYScroolSpeed = 0.05f;
    [SerializeField] private float backgroundXScroolSpeed = 0.05f;

    private float currentXValue = 0;
    private float currentYValue = 0;

    // Use this for initialization
    void Start()
    {
        image = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        currentXValue += backgroundXScroolSpeed * Time.deltaTime;
        currentYValue += backgroundYScroolSpeed * Time.deltaTime;
        image.uvRect = new Rect(currentXValue, currentYValue, 1, 1);
    }

}
