using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField, Range(0, 1)] float speed = 0.5f;

    Vector3 direction;

    readonly Vector3 P_DIRECTION = new Vector3(1, 0, 0);
    readonly Vector3 N_DIRECTION = new Vector3(-1, 0, 0);
    const float P_END = 4.0f;
    const float N_END = -4.0f;

    void Start()
    {
        direction = P_DIRECTION;
    }

    void Update()
    {
        float x = transform.position.x;

        //ENDを越えたら進行方向を変える
        if(x > P_END)
        {
            direction = N_DIRECTION;
        }
        else if(x < N_END)
        {
            direction = P_DIRECTION;
        }

        Vector3 nextPos = transform.position + direction * speed * 0.01f;

        //移動
        transform.position = nextPos;
    }
}
