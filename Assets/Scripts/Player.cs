using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Для игрока
    private BoxCollider2D boxCollider;
   
    private SpriteRenderer sp;

    [SerializeField] Animator animator;

    [SerializeField] Sprite openDoor;
    [SerializeField] Sprite emptyChest;
    [SerializeField] FloatingTextManager floating;
    [SerializeField] Font font;

    private Vector3 moveDelta;
    private RaycastHit2D hit;
    public static int globalMoneyAmount = 0;
    private AudioSource audioSorce;

    public static int currentIndex;
    public int moneyAmount = 5;
    public Text moneyText;
    public Text levelText;
    internal bool isCollectGold = false;

    public static int countOfOpenDoors = 0; // количество открытых дверей 

    private void Start()
    {
        currentIndex = SceneManager.GetActiveScene().buildIndex;
        boxCollider = GetComponent<BoxCollider2D>();
        animator.Play("FadeOut");
    }

    private void Update()
    {
        moneyText.text = globalMoneyAmount + " coins";
        levelText.text = "Level " + (currentIndex);
    }

    private IEnumerator Death()
    {
        animator.Play("FadeIn");
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene("DeathScreen");
    }

    // Метод, отвечающий за коллизию игрока с чем-либо
    private void OnTriggerStay2D(Collider2D other)
    {
        // Смерть игрока
        if (other.CompareTag("Enemy")) 
        {
            StartCoroutine("Death");
            // При смерти игрок появляется на том уровне, до которого дошёл (чекпоинт)
            globalMoneyAmount = 0; // Обнуление количества монет при смерти
            countOfOpenDoors = 0; // Обнуление количества открытых дверей при смерти
        }

        // Открытие игроком двери
        if (other.CompareTag("Door")) 
        {
            sp = other.gameObject.GetComponentInChildren<SpriteRenderer>();
            CircleCollider2D bx = other.gameObject.GetComponentInChildren<CircleCollider2D>();
            audioSorce = other.gameObject.GetComponent<AudioSource>();

            if (Input.GetKeyDown(KeyCode.E) && globalMoneyAmount >= 30 && sp.sprite != openDoor)
            {
                sp.sprite = openDoor;
                bx.enabled = false;
                globalMoneyAmount -= 30;
                countOfOpenDoors += 1;
                audioSorce.Play();
                floating.Show("Door is open!", 70, FontStyle.BoldAndItalic, Color.HSVToRGB(0.3f, 1f, 1f), transform.position, Vector3.up * 100, 1.5f, font);
            }
            else if(Input.GetKeyDown(KeyCode.E) && globalMoneyAmount < 30 && sp.sprite != openDoor)
            {
                floating.Show("Need " + (30 - globalMoneyAmount) + " coins!", 70, FontStyle.BoldAndItalic, Color.red, transform.position, Vector3.up * 100, 1.5f, font);
            }
        }

        // Телепортация игрока
        if (other.CompareTag("Portal"))
        {
            if (Input.GetKeyDown(KeyCode.E) && globalMoneyAmount >= 50)
            {
                globalMoneyAmount -= 50;
                SceneManager.LoadScene(currentIndex + 1);
            }
            else if (Input.GetKeyDown(KeyCode.E) && globalMoneyAmount < 50)
            {
                floating.Show("Need " + (50 - globalMoneyAmount) + " coins!", 70, FontStyle.BoldAndItalic, Color.red, transform.position, Vector3.up * 100, 1.5f, font);
            }
        }

        // Сбор монет из сундука
        if (other.CompareTag("Chest"))
        {
            sp= other.gameObject.GetComponent<SpriteRenderer>();
            audioSorce = other.gameObject.GetComponent<AudioSource>();

            if (Input.GetKeyDown(KeyCode.E) && sp.sprite != emptyChest)
            {
                sp.sprite = emptyChest;
                floating.Show("+" + moneyAmount + " coins!", 90, FontStyle.BoldAndItalic, Color.yellow, transform.position, Vector3.up * 100, 1.5f, font);
                globalMoneyAmount += moneyAmount;
                moneyText.text = globalMoneyAmount + " coins";
                audioSorce.Play();
            }
        }
    }

    // Управление игроком
    private void FixedUpdate()
    {
        moveDelta = Vector3.zero;
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        moveDelta = new Vector3(x, y, 0);

        if (moveDelta.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }
}
