using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RankLine : MonoBehaviour
{
    TextMeshProUGUI nameText;
    TextMeshProUGUI recordText;

    private void Awake()
    {
        Transform child = transform.GetChild(1);
        nameText = child.GetComponent<TextMeshProUGUI>();
        child = transform.GetChild(2);
        recordText = child.GetComponent<TextMeshProUGUI>();
    }


    /// <summary>
    /// 데엍 세팅하는 함수
    /// </summary>
    /// <param name="rankerName"></param>
    /// <param name="record"></param>
    public void SetData(string rankerName, int record)
    {

        nameText.text = rankerName;
        recordText.text = record.ToString("N0");

    }


}
