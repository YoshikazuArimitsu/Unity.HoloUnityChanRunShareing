using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugTextController : MonoBehaviour {
    public static string DebugText_;

    Text Text_;

    // Use this for initialization
    void Start() {
        Text_ = GetComponent<Text>();
        DebugText_ = "debug";
    }

    // Update is called once per frame
    void Update() {
        Text_.text = DebugText_;
    }
}
