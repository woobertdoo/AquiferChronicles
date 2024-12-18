using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject arrow;
    private Animator animator;
    private PlayerController player;
    public int health = 2;
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        animator.SetBool("InCombat", true);
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0 && !isDead) {
           isDead = true; 
           animator.SetTrigger("Die");
        }
        
        transform.LookAt(player.transform);
    }
    
    void ShootArrow() {
        // The right hand bone is not a direct descendant of the prefab, so we need to make a large string hierarchy
        string rightHandPath = "mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/mixamorig:Spine2/mixamorig:RightShoulder/mixamorig:RightArm/mixamorig:RightForeArm/mixamorig:RightHand";
        Vector3 handPos = transform.Find(rightHandPath).position;
        // We spawn the arrow and then apply an impulse force to it when it spawns
        GameObject spawnedArrow = Instantiate(arrow, handPos, transform.rotation); 
        float distanceFromPlayer = (player.transform.position - transform.position).magnitude;
        // The impulse force is based on how far away the player is, so that as the player gets closer the arrow gets shot a shorter distance
        spawnedArrow.GetComponent<Rigidbody>().AddForce(spawnedArrow.transform.forward * distanceFromPlayer * 0.8f, ForceMode.Impulse);
        Destroy(spawnedArrow, 5.0f); 
    }
     
    public void TakeDamage() {
        health -= 1;
    }
}
