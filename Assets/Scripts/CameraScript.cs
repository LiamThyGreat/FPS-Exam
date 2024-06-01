using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject crossHair;
    [SerializeField] Transform oriantation;
    [SerializeField] Transform player;
    [SerializeField] Transform playerObj;
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform combatLookAt;
    [SerializeField] GameObject thirdPersonCam;
    [SerializeField] GameObject combatCam;

    [Header("KeyCodes")]
    [SerializeField] KeyCode equipSword = KeyCode.Alpha1;
    [SerializeField] KeyCode equipBow = KeyCode.Alpha2;
    [SerializeField] KeyCode changeToCombatCam = KeyCode.C;
    [SerializeField] KeyCode changeToBasicCam = KeyCode.B;

    [SerializeField] float rotationSpeed;

    public CameraStyle currentStyle;

    BowScript bowScript;

    public enum CameraStyle
    {
        Basic,
        Combat
    }
    private void Start()
    {
        bowScript = FindObjectOfType<BowScript>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        crossHair.SetActive(false);
        ChangeCamStyle(CameraStyle.Basic);
    }
    private void Update()
    {
        if (Input.GetKey(changeToCombatCam))
        {
            ChangeCamStyle(CameraStyle.Combat);
            crossHair.SetActive(true);
        }
        if (Input.GetKey(changeToBasicCam))
        {
            ChangeCamStyle(CameraStyle.Basic);
            crossHair.SetActive(false);
            bowScript.bowEquipped = false;
        }
        if (Input.GetKey(equipSword))
        {
            ChangeCamStyle(CameraStyle.Basic);
            crossHair.SetActive(false);
            bowScript.bowEquipped = false;
        }
        if (Input.GetKey(equipBow))
        {
            ChangeCamStyle(CameraStyle.Combat);
            crossHair.SetActive(true);
            bowScript.bowEquipped = true;
        }

        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        oriantation.forward = viewDir.normalized;

        if (currentStyle == CameraStyle.Basic)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 inputDir = oriantation.forward * verticalInput + oriantation.right * horizontalInput;

            if (inputDir != Vector3.zero)
            {
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
            }
        }
        else
        {
            Vector3 dirCombatLookAt = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
            oriantation.forward = dirCombatLookAt.normalized;

            playerObj.forward = dirCombatLookAt.normalized;
        }
    }

    void ChangeCamStyle(CameraStyle newStyle)
    {
        combatCam.SetActive(false);
        thirdPersonCam.SetActive(false);

        if (newStyle == CameraStyle.Basic)
        {
            thirdPersonCam.SetActive(true);
        }
        if (newStyle == CameraStyle.Combat)
        {
            combatCam.SetActive(true);
        }
        currentStyle = newStyle;
    }
}
