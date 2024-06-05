using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowScript : MonoBehaviour
{
    [SerializeField] private GameObject bow;
    [SerializeField] private GameObject sword;
    [SerializeField] private BaseArrow normalArrowPrefab;
    [SerializeField] private BaseArrow fireArrowPrefab;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform arrowSpawnPoint;
    [SerializeField] private KeyCode shootArrow = KeyCode.Mouse0;
    [SerializeField] private KeyCode aimBow = KeyCode.Mouse1;
    [SerializeField] private KeyCode switchArrowType = KeyCode.R;
    [SerializeField] private float arrowCooldown;
    [SerializeField] private float arrowShootSpeed;

    public bool bowEquipped = false;
    public bool readyToShoot = true;
    private BaseArrow currentArrowPrefab;

    SwordAttackScript swordAttackScript;

    private void Start()
    {
        swordAttackScript = FindObjectOfType<SwordAttackScript>();

        sword.SetActive(true);
        bow.SetActive(false);

        bowEquipped = false;
        readyToShoot = true;
        currentArrowPrefab = normalArrowPrefab; 
    }

    private void Update()
    {
        if (Input.GetKey(aimBow))
        {
            swordAttackScript.isAimingBow = true;
            swordAttackScript.canRecieveInput = false;

            sword.SetActive(false);
            bow.SetActive(true);

            if (Input.GetKey(shootArrow) && bowEquipped && readyToShoot)
            {
                readyToShoot = false;
                ShootArrow();
            }
        }
        else
        {
            swordAttackScript.isAimingBow = false;

            sword.SetActive(true);
            bow.SetActive(false);
            swordAttackScript.StartCooldownRoutine();
        }

        if (Input.GetKeyDown(switchArrowType))
        {
            SwitchArrowType();
        }
    }

    private void SwitchArrowType()
    {
        if (currentArrowPrefab == normalArrowPrefab)
        {
            currentArrowPrefab = fireArrowPrefab;
        }
        else
        {
            currentArrowPrefab = normalArrowPrefab;
        }
    }

    private void ShootArrow()
    {
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(1000);
        }

        Vector3 direction = (targetPoint - arrowSpawnPoint.position).normalized;

        BaseArrow arrow = Instantiate(currentArrowPrefab, arrowSpawnPoint.position, Quaternion.LookRotation(direction));
        arrow.Shoot(direction);

        StartCoroutine(ArrowCooldownRoutine());
    }

    IEnumerator ArrowCooldownRoutine()
    {
        yield return new WaitForSeconds(arrowCooldown);
        readyToShoot = true;
    }
}
