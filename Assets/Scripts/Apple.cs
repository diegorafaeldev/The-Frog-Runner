using UnityEngine;


public class Apple : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;
    public GameObject collected;
    public int Score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {

            spriteRenderer.enabled = false;
            circleCollider.enabled = false;
            collected.SetActive(true);

            GameController.instance.totalScore += Score;
            GameController.instance.UpdateScoreText();

            Destroy(gameObject, 0.5f);
        }
    }
}
