using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Логика
    public float triggerLenght = 1; // радиус в котором противник замечает игрока
    public float chaseLength = 1; // радиус преследования монстром игрока от начальной позиции монстра
    private bool chasing; // преследование
    private bool collidingWithPlayer; 

    private BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    private RaycastHit2D hit;
    private AudioSource audioSource;

    [SerializeField] private float ySpeed = 0.5f; // 0.75f
    [SerializeField] private float xSpeed = 0.5f; // 1.0f

    public Transform playerTransform;
    private Vector3 startingPosition;

    public ContactFilter2D filter;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        startingPosition = transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    // Метод, отвечающий за передвижение игрока
    private void UpdateMotor(Vector3 input)
    {
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);

        if (moveDelta.x < 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (moveDelta.x > 0)
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

    private void FixedUpdate()
    {
        if(Vector3.Distance(playerTransform.position, startingPosition) < chaseLength) // Проверка находится ли игрок в зоне преследования противника
        {
            if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLenght)
            {
                chasing = true; // Активация преследования игрока врагом
            }

            if(chasing)
            {
                if(!collidingWithPlayer)
                {
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                }
            } 
            else
            {
                UpdateMotor(startingPosition - transform.position); // Враг и перестаёт преследовать и возвращается на начальную точку
            }
        }
        else
        {
            UpdateMotor(startingPosition - transform.position);
            chasing = false; // Враг перестаёт преследовать игрока 
        }
    }
}
