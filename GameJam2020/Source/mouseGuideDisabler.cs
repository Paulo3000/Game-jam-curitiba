using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseGuideDisabler : MonoBehaviour
{
    public GameObject mouseGuide;
    public SpriteRenderer mouseGuideRenderer;
    public Color mouseGuideColor;
    public Color transparent;

    float t;

    private void OnEnable()
    {
        mouseGuideRenderer = mouseGuide.GetComponent<SpriteRenderer>();
        mouseGuideColor = mouseGuideRenderer.color;
        transparent = new Color(mouseGuideColor.r, mouseGuideColor.g, mouseGuideColor.b, 0f);
        t = 0f;
    }

    private void Update()
    {
        t += Time.deltaTime * 1f;
        mouseGuideRenderer.color = Color.Lerp(mouseGuideColor, transparent, t);
    }
}
