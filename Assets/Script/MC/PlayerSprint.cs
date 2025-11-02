using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSprint : MonoBehaviour
{
    public float totalStamina, currentStamina, sprintSpeed;
    public float sprintCost, rechargeRate;
    public float normalSpeed;
    public Image staminaBar;
    public bool isSprinting;

    private Coroutine recharge;
    private PlayerMovement playerMovement;

    void Start()
    {
        currentStamina = totalStamina;
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetButton("Sprint") && currentStamina > 0)
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }

        if (isSprinting)
        {
            currentStamina -= sprintCost * Time.deltaTime;
            playerMovement.walkSpeed = sprintSpeed;
            if (currentStamina < 0)
            {
                currentStamina = 0;
            }
            if (recharge != null)
            {
                StopCoroutine(recharge);
            }
            recharge = StartCoroutine(RechargeStamina());

        }
        else
        {
            playerMovement.walkSpeed = normalSpeed;
        }

        staminaBar.fillAmount = currentStamina / totalStamina;
    }

    private IEnumerator RechargeStamina()
    {
        yield return new WaitForSeconds(1f);
        while (currentStamina < totalStamina)
        {
            currentStamina += rechargeRate / 10f;
            if (currentStamina > totalStamina)
            {
                currentStamina = totalStamina;
            }
            staminaBar.fillAmount = currentStamina / totalStamina;
            yield return new WaitForSeconds(0.1f);
        }
    }
}