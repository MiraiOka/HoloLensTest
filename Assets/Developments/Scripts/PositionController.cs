using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.WSA.Input;


public class PositionController : MonoBehaviour {

    [SerializeField] Text text;
    [SerializeField] Camera holoCamera;

    private void Start()
    {
        text.text = "ヨッシーをクリックしてください";
        InteractionManager.InteractionSourcePressed += InteractionSourcePressed;
    }


    void InteractionSourcePressed(InteractionSourcePressedEventArgs ev)
    {
        text.text = "tap!!!";

        //Vector3 headPos = Camera.main.transform.position;
        //Vector3 direction = Camera.main.transform.forward;
        Vector3 headPos = holoCamera.transform.position;
        Vector3 direction = holoCamera.transform.forward;


        //Ray ray = new Ray(headPos, direction);
        Debug.DrawRay(headPos, direction, Color.yellow, 100, false);
        Debug.Log(headPos);
        Debug.Log(direction);
    }
}
