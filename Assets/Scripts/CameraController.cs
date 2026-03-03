using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class CameraController : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] Camera cam;
    [SerializeField] Rigidbody2D rb;

    private float horizontal;
    private float vertical;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void moveCamera(float horizontal, float vertical)
    {
        rb.linearVelocity = Vector2.zero;
        if(Math.Abs(player.transform.position.x - rb.transform.position.x) < 2f)
        {
            rb.linearVelocity = new Vector2(horizontal * player.playerSpeed, rb.linearVelocity.y);
        }
        if (Math.Abs(player.transform.position.y - rb.transform.position.y) < 2f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, vertical * player.playerSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "CameraBoundary")
        {
            Debug.Log("Camera collided with wall");
        }
    }

}
