using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public float movementSpeed = 10;
    public float jumpForce = 1800;
    public AudioController audioController;

    private Rigidbody2D rb;
    private Animation anim;
    private bool facingLeft = true;
    private float rootDuration = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rootDuration > 0)
        {
            rootDuration -= Time.deltaTime;
            return;
        }

        var moveHor = Input.GetAxis("Horizontal");
        transform.position += new Vector3(moveHor, 0, 0) * Time.deltaTime * movementSpeed;

        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.1) {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            audioController.PlaySound("JumpSound");
            Debug.Log(transform.rotation.eulerAngles.z);    
            if (transform.rotation.eulerAngles.z > 80 && transform.rotation.eulerAngles.z < 180) transform.Rotate(0.0f, 0.0f, -80.0f);
            else if (transform.rotation.eulerAngles.z > 180 && transform.rotation.eulerAngles.z < 280) transform.Rotate(0.0f, 0.0f, 80.0f);
        }

        if ((moveHor > 0 && facingLeft) || (moveHor < 0 && !facingLeft))
        {
            facingLeft = !facingLeft;
            transform.Rotate(0, 180, 0);
        }

        if (Input.GetButtonDown("Fire1")) {
            anim.Play("SwordSwing");
            audioController.PlaySound("SwordSound");
        }

        if (Input.GetButtonDown("Fire2"))
        {
            rootDuration = 0.8f;
            anim.Play("SwordThrow");
            audioController.PlaySound("SwordSound");
        }

    }
}
