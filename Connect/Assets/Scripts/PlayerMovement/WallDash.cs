﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDash : MonoBehaviour
{
    [SerializeField] private InputControllerData playerControlKeys;
    public float dashVelocity;

    [Header("Ground/Wall Detectors")]
    public LayerMask groundLayerMask;
    public float collisionRadius;
    public float bottomOffset;
    public float leftOffset;
    public float rightOffset;

    [Header("Debug - Ground/Wall Detectors")]
    [SerializeField] private bool showDetector;
    [SerializeField] private Color debugCollisionColor = Color.red;

    [Header("Debug Purpose")]
    [SerializeField] private bool canWallDash;
    [SerializeField] private bool onGround;
    [SerializeField] private bool onWall;
    [SerializeField] private bool onLeftWall;
    [SerializeField] private bool onRightWall;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    { 

        if (groundLayerMask == 0) groundLayerMask = LayerMask.GetMask("Ground");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //--------------------------------------------------------------
        // Updates player condition
        //--------------------------------------------------------------
        onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + Vector2.right * leftOffset, collisionRadius, groundLayerMask);
        onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + Vector2.right * rightOffset, collisionRadius, groundLayerMask);
        onWall = onLeftWall || onRightWall;
        UpdateCanDash(!onGround && onWall);

        //--------------------------------------------------------------
        // Execute action based on conditions
        //--------------------------------------------------------------
        if (Input.GetKey(playerControlKeys.jump))
        {
            if (canWallDash)
            {
                Vector2 dir;
                if (onLeftWall)
                {
                    dir = new Vector2(1, 1);
                }
                else
                {
                    dir = new Vector2(-1, 1);
                }
                DoWallDash(dir);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if(showDetector)
        {
            Gizmos.color = debugCollisionColor;

            Gizmos.DrawWireSphere((Vector2)transform.position + Vector2.up * bottomOffset, collisionRadius);
            Gizmos.DrawWireSphere((Vector2)transform.position + Vector2.right * leftOffset, collisionRadius);
            Gizmos.DrawWireSphere((Vector2)transform.position + Vector2.right * rightOffset, collisionRadius);
        }
    }

    public void UpdateCanDash(bool input)
    {
        canWallDash = input;
    }
    private void DoWallDash(Vector2 dir)
    {
        Vector2 v = dir.normalized * dashVelocity;
        rb.velocity = new Vector2(v.x, v.y);
    }
}