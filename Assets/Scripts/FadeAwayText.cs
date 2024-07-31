using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FadeAwayText : MonoBehaviour
{
    private TextMeshProUGUI fadeAwayText;
    private NumberFormatter numberFormatter = new NumberFormatter();
    private RectTransform transform;
    private float updatedPositionY;

    [SerializeField] private float fadeTime;
    [SerializeField] private float alfaValue;
    [SerializeField] private float fadeAwayPerSecond;

    void Start()
    {
        transform = GetComponent<RectTransform>();
        fadeAwayText = GetComponent<TextMeshProUGUI>();
        fadeAwayPerSecond = 1 / fadeTime;
        alfaValue = fadeAwayText.color.a;
        updatedPositionY = -24.97f;
    }

    void Update()
    {
        if (transform.localPosition.y == -24.97f) //if text got higher DO NOT update
        {
            fadeAwayText.text = "+" + numberFormatter.FormatNumber(Game.ClickPower);
        }

        updatedPositionY += 30 * Time.deltaTime;
        if (updatedPositionY > 50f)
        {
            updatedPositionY = -24.97f;
            GameObject.Destroy(this.gameObject);
        }
        transform.localPosition = new Vector3 (transform.localPosition.x, updatedPositionY, transform.localPosition.z);

        if (fadeTime > 0)
        {
            alfaValue -= fadeAwayPerSecond * Time.deltaTime;
            fadeAwayText.color = new Color(fadeAwayText.color.r, fadeAwayText.color.g, fadeAwayText.color.b, alfaValue);
            fadeTime -= Time.deltaTime;
        }
    }
}
