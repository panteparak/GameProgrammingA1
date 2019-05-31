using System;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    [SerializeField] private Text text;
    private void Start()
    {
        text.text = String.Format("You Scored {0} points", Storage.Instance.getScore());
    }
}