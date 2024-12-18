using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float runSpeed = 5;
    public float jumpHeight = 10;
    public float turnSpeed = 5;
    public Animator animator;
    private Rigidbody rb;
    public bool isGrounded;
    public bool isAttacking = false;
    public bool isDead = false;
    
    private SoundEffectsManager sfxManager;
    
    public float attackRange = 20.0f;
    private CapsuleCollider capsuleCollider;
    private EnemyController enemy;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        sfxManager = GetComponent<SoundEffectsManager>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        float zSpd = Input.GetAxis("Vertical") * runSpeed;
        float rot = Input.GetAxis("Horizontal") * turnSpeed;
        if(!isAttacking) {
            animator.SetFloat("Speed", Mathf.Abs(zSpd)); 
            transform.Translate(new Vector3(0, 0, zSpd) * Time.deltaTime, Space.Self);
            transform.Rotate(new Vector3(0, rot, 0) * Time.deltaTime, Space.Self);
        }
        
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            Jump();
        }
        
        if(Input.GetMouseButtonDown(0) && !isAttacking) {
            Attack();
        }   

    }

    void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Ground")) {
            animator.SetBool("isJumping", false);
            animator.SetBool("isInAir", false);
            isGrounded = true;
        }    
    }
    
    void OnTriggerEnter(Collider other) {
        EnemyController enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
        if(other.gameObject.CompareTag("Arrow") && !enemy.isDead) {
            isDead = true;
            animator.SetTrigger("Die");
        }    
    }
    
    void Jump() {
        rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        animator.SetBool("isJumping", true);
        animator.SetBool("isInAir", true);
        isGrounded = false;
    }
    
    void Attack() {
        int randAttack = Random.Range(1, 4);
        
        animator.SetTrigger("Attack");
        animator.SetInteger("AttackAnim", randAttack);
        sfxManager.PlayAttackSound();
        isAttacking = true;
        float distanceToEnemy = (transform.position - enemy.transform.position).magnitude;
        if(distanceToEnemy < attackRange)
            enemy.TakeDamage();
    } 

    public void StopAttack() {
        isAttacking = false;
        animator.SetInteger("AttackAnim", 0);
    }
}
