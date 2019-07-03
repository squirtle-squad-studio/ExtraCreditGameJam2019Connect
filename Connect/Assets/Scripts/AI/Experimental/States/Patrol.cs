﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : BaseState
{
    public Vector2 patrolLoc_left;
    public Vector2 patrolLoc_right;

    private bool isPatrolToTheRight;

    private AIInput aiInput;

    public Patrol(GameObject obj, AIInput aiInput, Vector2 left, Vector2 right) : base(obj)
    {
        patrolLoc_left = left;
        patrolLoc_right = right;
        this.aiInput = aiInput;
    }
    public Patrol(GameObject obj, AIInput aiInput, Vector2 left, Vector2 right, bool isPatrolToTheRight) : base(obj)
    {
        patrolLoc_left = left;
        patrolLoc_right = right;
        this.isPatrolToTheRight = isPatrolToTheRight;
        this.aiInput = aiInput;
    }

    public override void Tick()
    {
        PatrolAroundLoc();
    }
    private void PatrolAroundLoc()
    {
        if (isPatrolToTheRight)
        {
            PatrolTo(patrolLoc_right);
        }
        else
        {
            PatrolTo(patrolLoc_left);
        }
    }

    private void PatrolTo(Vector2 loc)
    {
        aiInput.aiControls.ResetKeyInputs();
        if (loc.x - obj.transform.position.x < 0.1 && loc.x - obj.transform.position.x > -0.1)
        {
            TurnAround();
        }
        else if (loc.x - obj.transform.position.x >= 0.1)
        {
            aiInput.aiControls.right = true;
        }
        else
        {
            aiInput.aiControls.left = true;
        }
    }

    private void TurnAround()
    {
        isPatrolToTheRight = !isPatrolToTheRight;
    }
}
