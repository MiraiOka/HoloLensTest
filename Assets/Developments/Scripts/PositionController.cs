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
    [SerializeField] int length = 10; //会場のスケール
    int size;
    [SerializeField] GameObject panel;
    [SerializeField] GameObject firstObj;
    [SerializeField] GameObject secondObj;
    [SerializeField] GameObject thirdObj;
    Vector3[] firstRayPos;
    Vector3[] secondRayPos;
    Vector3[] thirdRayPos;
    float distance12; //firstとsecondの距離を格納する変数
    float distance23;
    float distance31;

    int[] point = new int[2];
    List<int[]> points = new List<int[]>();


    public enum Target
    {
        first,
        second,
        third,
        fin
    }
    Target target = Target.first;

    private void Start()
    {
        text.text = "ヨッシーをクリックしてください";
        InteractionManager.InteractionSourcePressed += InteractionSourcePressed;
        distance12 = Vector3.Distance(firstObj.transform.position, secondObj.transform.position);
        distance23 = Vector3.Distance(secondObj.transform.position, thirdObj.transform.position);
        distance31 = Vector3.Distance(thirdObj.transform.position, firstObj.transform.position);
        size = length * 10;
        firstRayPos = new Vector3[size];
        secondRayPos = new Vector3[size];
        thirdRayPos = new Vector3[size];
        point = new int[size];
        
    }

    void InteractionSourcePressed(InteractionSourcePressedEventArgs ev)
    {
        Vector3 headPos = holoCamera.transform.position;
        Vector3 direction = holoCamera.transform.forward;

        Debug.DrawRay(headPos, direction, Color.yellow, 100, false);
        

        

        switch(target)
        {
            case Target.first:
                for (int i = 0; i < size; i++)
                {
                    raySpheres.transform.position = headPos;
                    firstRayPos[i] = raySpheres.transform.position + direction * (i / 10.0f);
                    GameObject sphere = Instantiate(spherePrefab, firstRayPos[i], Quaternion.identity);
                    sphere.transform.parent = raySpheres.transform;
                }

                text.text = "ムックをクリックしてください。";
                target = Target.second;
                break;
            case Target.second:
                for (int i = 0; i < size; i++)
                {
                    secondRayPos[i] = raySpheres.transform.position + direction * (i / 10.0f);
                    GameObject sphere = Instantiate(spherePrefab, secondRayPos[i], Quaternion.identity);
                    sphere.transform.parent = raySpheres.transform;
                    for (int j = 0; j < size; j++)
                    {
                        float tmpDis12 = Vector3.Distance(firstRayPos[j], secondRayPos[i]);
                        
                        if (Mathf.Abs(tmpDis12 - distance12) < 0.1f) //それぞれのRayの距離がオブジェクト間の距離と近かったら
                        {
                            //Debug.Log(Mathf.Abs(tmpDis12 - distance12));
                            print("12");
                            point[0] = j;
                            point[1] = i;
                            points.Add(point);
                            //print("(" + j + ", " + i + ")");
                            /*GameObject sphere1 = Instantiate(spherePrefab, firstRayPos[j], Quaternion.identity);
                            GameObject sphere2 = Instantiate(spherePrefab, secondRayPos[i], Quaternion.identity);
                            sphere1.transform.parent = raySpheres.transform;
                            sphere2.transform.parent = raySpheres.transform;*/
                        }
                    }
                }

                text.text = "ガチャピンをクリックしてください。";
                target = Target.third;
                break;
            case Target.third:
                bool isFinish = false;
                for(int i = 0; i < size; i++)
                {
                    thirdRayPos[i] = raySpheres.transform.position + direction * (i / 10.0f);
                    //GameObject sphere = Instantiate(spherePrefab, thirdRayPos[i], Quaternion.identity);
                    //sphere.transform.parent = raySpheres.transform;
                    for (int j = 0; j < points.Count; j++)
                    {
                        
                        float tmpDis23 = Vector3.Distance(secondRayPos[points[j][1]], thirdRayPos[i]);
                        if (Mathf.Abs(tmpDis23 - distance23) < 0.1f)
                        {
                            print("23");
                            float tmpDis31 = Vector3.Distance(thirdRayPos[i], firstRayPos[points[j][0]]);

                            /*GameObject sphere2 = Instantiate(spherePrefab, secondRayPos[points[j][1]], Quaternion.identity);
                            GameObject sphere3 = Instantiate(spherePrefab, thirdRayPos[i], Quaternion.identity);
                            sphere2.transform.parent = raySpheres.transform;
                            sphere3.transform.parent = raySpheres.transform;*/
                            
                            if (Mathf.Abs(tmpDis31 - distance31) < 0.1f)
                            {
                                print("(" + points[j][0] + ", " + points[j][1] + ", " + i + ")");
                                print("31");
                                Instantiate(spherePrefab, firstRayPos[points[j][0]], Quaternion.identity);
                                Instantiate(spherePrefab, secondRayPos[points[j][1]], Quaternion.identity);
                                Instantiate(spherePrefab, thirdRayPos[i], Quaternion.identity);
                                isFinish = true;
                            }
                        }
                        if (isFinish) break;
                    }
                    if (isFinish) break;
                }

                Destroy(panel);
                break;
            default:
                break;
        }
    }
}
