using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMessage : MonoBehaviour
{
    public static UIMessage Instance { private set; get; }

    private Text text;

    void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        text.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowMessage(string msg)
    {
        text.text = msg;
    }
}
