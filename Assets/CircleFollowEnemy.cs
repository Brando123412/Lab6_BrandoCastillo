using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleFollowEnemy : MonoBehaviour
{
    [SerializeField] Transform transformEnemy;

    // Update is called once per frame
    void Update()
    {
        transform.position=transformEnemy.position;
    }
}
