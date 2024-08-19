using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ghast : MonoBehaviour
{
    [SerializeField] private float speed = 50f;
    private RectTransform rectTransform;
    private Image ghastImage;

    private bool movingRight = true;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        ghastImage = GetComponent<Image>();
    }

    void Update()
    {
        MoveGhast();
    }

    private void MoveGhast()
    {
        //determine the side of movement
        float direction = movingRight ? 1 : -1;

        rectTransform.anchoredPosition += new Vector2(speed * direction * Time.deltaTime, 0);

        // checking for touching the edge of the screen
        if (rectTransform.anchoredPosition.x >= Screen.width / 2 && movingRight)
        {
            movingRight = false;
            FlipGhast();
        }
        else if (rectTransform.anchoredPosition.x <= -Screen.width / 2 && !movingRight)
        {
            movingRight = true;
            FlipGhast();
        }
    }

    private void FlipGhast()
    {
        // flip the image horizontally
        ghastImage.transform.localScale = new Vector3(-ghastImage.transform.localScale.x, ghastImage.transform.localScale.y, ghastImage.transform.localScale.z);
    }
}