using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;

    [Header("Variaveis")]
    [SerializeField] private float jumpForce = 16f;
    [SerializeField] private float speed = 8f;
    
    [Header("Referencias")] 
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Rigidbody2D rigidbody;
    
    [Header("Booleans")]
    [SerializeField] private bool isOnFloor;

    [Header("Checks")]
    [SerializeField] private Transform floorCheck;
    [SerializeField] private LayerMask floorLayer;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && isOnFloor && IsFloor())
        {
            rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            isOnFloor = false;
            animator.SetBool("taPulando", true);
        }
        
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            this.gameObject.transform.position += Vector3.left * speed * Time.deltaTime;
            sprite.flipX = true;
            animator.SetBool("taCorrendo", true);
        }
        
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            this.gameObject.transform.position += Vector3.right * speed * Time.deltaTime;
            sprite.flipX = false;
            animator.SetBool("taCorrendo", true);
        }
        else
        {
            animator.SetBool("taCorrendo", false);
        }
    }

    private bool IsFloor()
    {
        return Physics2D.OverlapCircle(floorCheck.position, 0.2f, floorLayer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isOnFloor = true;
            animator.SetBool("taPulando", false);
        }
    }
  
    

}
