using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMover : MonoBehaviour
{
    //왼쪽에서 오른족으로 움직이기
    //화면 밖을 벗어나면 오른쪽 화면 밖으로 움직이기
    //오른쪽 화면 밖으로 갈 때 움직이는 양은 랜덤성 가미
    //오른쪽 화면 밖으로 갈 때 위 아래로도 랜덤하게 움직이기.

    public float moveSpeed = 10.0f;

    public float minRightEnd = 20.0f;
    public float maxRightEnd = 60.0f;

    public float minHeight = -4.0f;
    public float maxHeight = -1.0f;

    float moveTriggerPosition = -16.0f;

    private void Awake()
    {
        moveTriggerPosition = transform.position.x;
    }


    private void Update()
    {
        transform.Translate(Time.deltaTime * moveSpeed * -transform.right);
        if(transform.position.x < moveTriggerPosition)
        {
            Vector3 newPos = new Vector3(
                                        Random.Range(minRightEnd, maxRightEnd),
                                        Random.Range(minHeight, maxHeight));
            transform.position = newPos;
        }
    }


}
