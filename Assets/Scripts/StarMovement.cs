using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StarMovement : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = transform.position;
        int chance = Random.Range(1, 3);

        if (chance == 1) pos.x = 3f;
        else if (chance == 2) pos.x = -3f;

        transform.position = pos;
        pos.x *= -1f;

        float durationRand = Random.Range(1f, 3f);
        transform.DOMove(pos, durationRand).SetLoops(-1, LoopType.Yoyo);   
    }
}
