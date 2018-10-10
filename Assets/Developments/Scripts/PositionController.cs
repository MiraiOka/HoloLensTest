using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.WSA.Input;


public class PositionController : MonoBehaviour {

    [SerializeField] Text text;
    [SerializeField] Camera holoCamera;
    [SerializeField] GameObject raySpheres;
    [SerializeField] GameObject spherePrefab;
    [SerializeField] float length = 10; //会場のスケール
    [SerializeField] GameObject panel;

    public enum Target
    {
        first,
        second,
        third
    }
    Target target = Target.first;

    private void Start()
    {
        text.text = "ヨッシーをクリックしてください";
        InteractionManager.InteractionSourcePressed += InteractionSourcePressed;
    }


    void InteractionSourcePressed(InteractionSourcePressedEventArgs ev)
    {

        //Vector3 headPos = Camera.main.transform.position;
        //Vector3 direction = Camera.main.transform.forward;
        Vector3 headPos = holoCamera.transform.position;
        Vector3 direction = holoCamera.transform.forward;


        //Ray ray = new Ray(headPos, direction);
        Debug.DrawRay(headPos, direction, Color.yellow, 100, false);
        

        raySpheres.transform.position = headPos;

        switch(target)
        {
            case Target.first:
                for (int i = 0; i < length * 10; i++)
                {
                    GameObject sphere = Instantiate(spherePrefab, raySpheres.transform.position + direction * (i / 10.0f), Quaternion.identity);
                    sphere.transform.parent = raySpheres.transform;
                }

                text.text = "ムックをクリックしてください。";
                target = Target.second;
                break;
            case Target.second:




                text.text = "ガチャピンをクリックしてください。";
                target = Target.third;
                break;
            case Target.third:

                Destroy(panel);
                break;
            default:
                break;
        }
        
    }
}
