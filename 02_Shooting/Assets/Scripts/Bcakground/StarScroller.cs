using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class StarScroller : Scroller
{

    SpriteRenderer[] spriteRenderers;


    protected override void Awake()
    {
        base.Awake();

        spriteRenderers= GetComponentsInChildren<SpriteRenderer>();
    }

    protected override void MoveRightEnd(int index)
    {
        base.MoveRightEnd(index);

        //bool odd = Random.Range(0.0f, 0.1f) > 0.5f;

        int rand = Random.Range(0, 4); // 0(0b_00), 1(0b_01), 2(0b_10) 3(0b_11)

        spriteRenderers[index].flipX = ((rand & 0b_01) != 0); //첫번째 비트가 1이면 true, 아니면 false
        spriteRenderers[index].flipY = ((rand & 0b_10) != 0); //두번째 비트가 1이먄 true, 아니면 false

    }


}
