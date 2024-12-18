using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private EnemyController enemy;
    private PlayerController player;
    private Canvas canvas;
    
    public Text enemyHealthText;
    public Text gameOverText;
    public Text winText;
    public bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        canvas = GameObject.FindObjectOfType<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        float enemyToPlayerDistance = (player.transform.position - enemy.transform.position).magnitude;
        Debug.Log(enemyToPlayerDistance);
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        Vector2 viewportPoint = Camera.main.WorldToViewportPoint(enemy.transform.position + new Vector3(0, enemyToPlayerDistance * 0.2f + 20, 0));
        enemyHealthText.rectTransform.anchorMin = viewportPoint;
        enemyHealthText.rectTransform.anchorMax = viewportPoint;
        
        enemyHealthText.text = enemy.health.ToString();
        
        if(enemy.isDead) {
            winText.gameObject.SetActive(true);
            enemyHealthText.gameObject.SetActive(false);
            gameOver = true;
        }

        if(player.isDead) {
            gameOverText.gameObject.SetActive(true);
            enemyHealthText.gameObject.SetActive(false);
            gameOver = true;
        }
        
        if(Input.GetKeyDown(KeyCode.Return) && gameOver) {
            SceneManager.LoadScene("SampleScene");
        } 
    }
}
