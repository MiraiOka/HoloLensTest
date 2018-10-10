using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.WSA.Input;


public class PositionController : MonoBehaviour {

    [SerializeField] Text text;

    private void Start()
    {
        text.text = "ヨッシーをクリックしてください";
        InteractionManager.InteractionSourcePressed += InteractionSourcePressed;
    }


    void InteractionSourcePressed(InteractionSourcePressedEventArgs ev)
    {
        text.text = "tap!!!";
    }
}
