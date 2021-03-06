﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : BaseState
{
    protected PlayerDetector detector;
    public Chase(GameObject obj, EntityInput entityInputs, PlayerDetector detector) : base(obj, entityInputs)
    {
        this.detector = detector;
    }

    public override void Tick()
    {
        GameObject closestPlayer = GetClosestPlayerFromTheDetector(detector);

        if (closestPlayer == null)
        {
            entityInputs.ResetKeyInputs();
            return;
        }
        else
        {
            float direction = closestPlayer.transform.position.x - obj.transform.position.x;
            if (direction == 0)
            {
                return;
            }
            else if (direction > 0)
            {
                entityInputs.run = true;
                entityInputs.right = true;
            }
            else
            {
                entityInputs.run = true;
                entityInputs.left = true;
            }
        }
    }

    private GameObject GetClosestPlayerFromTheDetector(PlayerDetector detector)
    {
        GameObject closestPlayer;
        List<GameObject> listOfPlayers = detector.players;
        if (listOfPlayers.Count > 0)
        {
            closestPlayer = listOfPlayers[0];

            if (listOfPlayers.Count > 1)
            {
                float closestDistance = Vector3.Distance(listOfPlayers[0].transform.position, obj.transform.position);
                for (int i = 1; i < listOfPlayers.Count; i++)
                {
                    if (Vector3.Distance(listOfPlayers[i].transform.position, obj.transform.position) < closestDistance)
                    {
                        closestPlayer = listOfPlayers[i].gameObject;
                    }
                }
            }
        }
        else { return null; }
        return closestPlayer;
    }
}
