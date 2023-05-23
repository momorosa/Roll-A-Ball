using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement Control")]
    [SerializeField] float speed = 1f;

    [Header("Audio Clips")]
    [SerializeField] AudioClip pickUpSound;
    [SerializeField] AudioClip winSound;

    [Header("UI Control")]
    [SerializeField] TextMeshProUGUI counterText;
    public GameObject winTextObject;


    Rigidbody rb;
    AudioSource audioSource;
    float movementX;
    float movementY;

    int count;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    public void OnPlayerMove( InputValue value )
    {
        Vector2 movementVector = value.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce( movement * speed );
  
    }

    void OnTriggerEnter(Collider other)
    {
        if ( other.gameObject.CompareTag("PickUp") )
        {
            other.gameObject.SetActive(false);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(pickUpSound);
            }
            else
            {
                audioSource.Stop();
            }
            count += 1;
            SetCountText();
        }
    }

    void SetCountText()
    {
        counterText.text = "Count: " + count.ToString() + " / " + 15;

        if (count >= 15)
        {
            winTextObject.SetActive( true );
            audioSource.PlayOneShot(winSound);
        }
    }

}

