using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;

    public float speed = 6.0f;
    public float jumpSpeed = 12.0f;
    public float gravity = 20.0f;
    public float maxHealth = 50f;
    public float health;

    private Vector3 moveDirection = Vector3.zero;

    [HideInInspector]
    public Animator myAnimator;
    public Animator swordAnimator;

    public AudioSource audioSource;

    public GameObject spriteHolder;

    private Vector3 lookPos;
    private Vector3 spriteHolderStartScale;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        audioSource = GetComponent<AudioSource>();

        health = maxHealth;
        GameController.playerHealth = health;
        GameController.maxHealth = maxHealth;

        characterController = GetComponent<CharacterController>();
        myAnimator = GetComponent<Animator>();
        spriteHolderStartScale = spriteHolder.transform.localScale;
    }

    void Update()
    {
        GameController.playerHealth = health;
        spriteHolder.transform.eulerAngles = new Vector3(spriteHolder.transform.rotation.x, 0f, spriteHolder.transform.rotation.z);

        if (Input.GetAxis("Horizontal") > 0.05f)
        {
            Vector3 invertedSpriteHolderScale = new Vector3(spriteHolderStartScale.x * -1f, spriteHolderStartScale.y, spriteHolderStartScale.z);
            spriteHolder.transform.localScale = invertedSpriteHolderScale;
        } else if (Input.GetAxis("Horizontal") < -0.05f)
        {
            spriteHolder.transform.localScale = spriteHolderStartScale;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }

        if (characterController.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection *= speed;

            myAnimator.SetFloat(
                "walking", 
                Mathf.Abs(Input.GetAxis("Horizontal")) + Mathf.Abs(Input.GetAxis("Vertical"))
                );

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        else
        {
            moveDirection.x = Input.GetAxis("Horizontal") * speed;
            moveDirection.z = Input.GetAxis("Vertical") * speed;
        }

        moveDirection.y -= gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);

        if (Input.GetButtonDown("Fire1") && swordAnimator.GetBool("shouldStrike") == false)
        {
            swordAnimator.SetBool("shouldStrike", true);
        }

        if (transform.position.y < -160f)
        {
            transform.position = startPosition;
        }

        if (health <= 0f)
        {
            Die();
        }
    }

    public float rotSpeed;

    void FixedUpdate()
    {
        // Generate a plane that intersects the transform's position with an upwards normal.
        Plane playerPlane = new Plane(Vector3.up, transform.position);

        // Generate a ray from the cursor position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Determine the point where the cursor ray intersects the plane.
        // This will be the point that the object must look towards to be looking at the mouse.
        // Raycasting to a Plane object only gives us a distance, so we'll have to take the distance,
        //   then find the point along that ray that meets that distance.  This will be the point
        //   to look at.
        float hitdist = 0.0f;
        // If the ray is parallel to the plane, Raycast will return false.
        if (playerPlane.Raycast(ray, out hitdist))
        {
            // Get the point along the ray that hits the calculated distance.
            Vector3 targetPoint = ray.GetPoint(hitdist);

            // Determine the target rotation.  This is the rotation if the transform looks at the target point.
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        audioSource.Play();
    }

    public void Die()
    {
        SceneManager.LoadScene("DeathScreen");
    }
}
