using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Controller : MonoBehaviour
{
    [SerializeField] InstinctFeature instinctFeature;

    [SerializeField, Range(MIN, MAX)] int filter = 0;

    const int OFFSET = 1;
    const int MAX = 100;
    const int MIN = 0;

    void Update()
    {
        //押し下げた瞬間
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("インスティンクト：オン");
        }
        //押し下がっている
        else if(Input.GetKey(KeyCode.Space))
        {
            filter += OFFSET;
            if(filter > MAX) filter = MAX;
        }
        //離した瞬間
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("インスティンクト：オフ");
        }
        //離れている
        else
        {
            filter -= OFFSET;
            if(filter < MIN) filter = MIN;
        }

        var outPut = Math.Clamp(filter / MAX, 0, 1);

        instinctFeature.SetFilter(outPut);
    }
}
