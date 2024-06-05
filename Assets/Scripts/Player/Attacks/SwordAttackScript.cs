using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttackScript : MonoBehaviour
{
    public static SwordAttackScript instance;

    [SerializeField] GameObject hitParticle;
    [SerializeField] KeyCode attack = KeyCode.Mouse0;
    [SerializeField] KeyCode block = KeyCode.Q;
    [SerializeField] float attackDamage;

    public bool isGrounded;

    public bool canRecieveInput;
    public bool inputRecieved = false;
    public bool airInputRecieved = false;

    public bool isAimingBow = false;
    public bool isAttacking = false;

    BoxCollider bc;
    public Rigidbody rb;
    public PlayerMovement movement;
    PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = GetComponentInParent<PlayerHealth>();
        bc = GetComponent<BoxCollider>();
        rb = GetComponentInParent<Rigidbody>();
        movement = GetComponentInParent<PlayerMovement>();

        canRecieveInput = true;
        instance = this;
    }

    private void Update()
    {
        if(Input.GetKey(block) && !isAimingBow && !inputRecieved)
        {
            playerHealth.isBlocking = true;
        }
        else
        {
            playerHealth.isBlocking = false;
        }
        
        if (Input.GetKey(attack) && !isAimingBow && canRecieveInput && isGrounded)
        {
            inputRecieved = true;
            canRecieveInput = false;
        }
        if(Input.GetKey(attack) && !isAimingBow && canRecieveInput && !isGrounded)
        {
            inputRecieved = true;
            canRecieveInput = false;
            movement.enabled = false;
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (isAttacking)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Enemy hit: " + collision.gameObject.name);

                collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
            }
            else if (collision.gameObject.CompareTag("Dummy"))
            {
                Debug.Log("Dummy hit: " + collision.gameObject.name);

                collision.gameObject.GetComponent<DummyScript>().Damaged(attackDamage);
            }

            Instantiate(hitParticle, collision.transform.position, Quaternion.identity);
        }
    }

    public void AttackDone()
    {
        isAttacking = false;
    }

    public void InputManager()
    {
        if (canRecieveInput)
        {
            canRecieveInput = false;
        }
        else if (!canRecieveInput)
        {
            canRecieveInput = true;
        }
    }

    public void RemoveInputrecieved()
    {
        inputRecieved = false;
    }

    public void EnableCollider()
    {
        bc.enabled = true;
    }
    public void RemoveCollider()
    {
        bc.enabled = false;
    }

    public void StartCooldownRoutine()
    {
        StartCoroutine(AttackCooldownAfterBow());
    }
    IEnumerator AttackCooldownAfterBow()
    {
        yield return new WaitForSeconds(1);

        canRecieveInput = true;
    }
}
