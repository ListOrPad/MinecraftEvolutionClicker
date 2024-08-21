using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using YG;

public class Bonus : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip boomSound;
    [SerializeField] private AudioClip sssSound;

    [Header("Main")]
    private Animator animator;
    private NumberFormatter formater;
    [SerializeField] private GameObject bonusObject;
    [SerializeField] private GameObject boomObject;
    [SerializeField] private TextMeshProUGUI bonusText;

    [SerializeField] private float activateTime = 120f; // Adjust to desired time(time until activation)
    private float timeFromLastActivate;
    private bool reset;
    private bool clicked;

    private RectTransform rTransform;
    private UnityEngine.Vector3 startPosition = new UnityEngine.Vector3(-1020f, -140f, 0);
    private float moveSpeed = 500f;

    void Start()
    {
        rTransform = GetComponent<RectTransform>();
        bonusObject.SetActive(false);
        timeFromLastActivate = 0;
        animator = GetComponent<Animator>();
        formater = new NumberFormatter();

        transform.position = startPosition;
        rTransform.localPosition = startPosition;
        
    }

    void Update()
    {
        timeFromLastActivate += Time.deltaTime;

        if (!bonusObject.activeSelf && timeFromLastActivate >= activateTime)
        {
            ActivateBonus();
        }

        if (bonusObject.activeSelf)
        {
            MoveBonus();
        }
    }

    private void MoveBonus()
    {
        // Move the bonus object
        transform.position += new UnityEngine.Vector3(moveSpeed * Time.deltaTime, 0, 0);

        // Optional: Reset position if it moves off-screen
        if (transform.position.x > Screen.width)
        {
            Deactivate();
        }
    }

    public void ActivateBonus()
    {
        if (!reset)
        {
            //animator.ResetTrigger("Click"); // Ensure animation is reset
            animator.SetTrigger("ResetToCreeper");
            bonusObject.SetActive(true);
            timeFromLastActivate = 0;
            transform.position = startPosition;
            rTransform.localPosition = startPosition;
            SoundManager.Instance.PlaySound(sssSound);
            reset = true;
        }
    }

    public void BoomClick()
    {
        if (!clicked)
        {
            clicked = true;
            animator.ResetTrigger("ResetToCreeper");
            boomObject.SetActive(true);
            animator.SetTrigger("Click");
            bonusText.gameObject.SetActive(true);
            AddBonus();
            SoundManager.Instance.PlaySound(boomSound);

            // Add a short delay before deactivation to allow animation to complete
            StartCoroutine(DeactivateAfterAnimation());
        }
    }

    private IEnumerator DeactivateAfterAnimation()
    {
        // Wait for the length of the current animation state
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float animationLength = stateInfo.length;

        if (stateInfo.IsName("Explosion"))
        {
            yield return new WaitForSeconds(animationLength);

            Deactivate();
        }
    }

    public void Deactivate()
    {
        bonusText.gameObject.SetActive(false);
        bonusObject.SetActive(false);
        boomObject.SetActive(false);
        clicked = false;
        reset = false;
    }

    private void AddBonus()
    {
        BigInteger bonus;
        if (Game.ClickPower * 2 > Game.IncomePerSecond)
        {
            bonus = Game.ClickPower * 50;
            Game.CounterValue += bonus;
        }
        else
        {
            bonus = Game.IncomePerSecond * 25;
            Game.CounterValue += bonus;
        }
        if (YandexGame.lang == "en")
        {
            bonusText.text = $"BONUS +{formater.FormatNumber(bonus)} <sprite=0>";
        }
        else
        {
            bonusText.text = $"анмся +{formater.FormatNumber(bonus)} <sprite=0>";
        }
    }
}