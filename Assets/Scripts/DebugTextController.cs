using HoloToolkit.Sharing.Tests;
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
        ImportExportAnchorManager ieam = ImportExportAnchorManager.Instance;

        Text_.text = string.Format("ImportExportAnchorManager: {0}\n{1}",
            ieam.StateName, DebugText_);
    }
}
