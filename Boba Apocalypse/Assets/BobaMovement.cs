using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class BobaMovement : MonoBehaviour
{
    public GameObject[] objectsToTrack;
    public GameObject[] otherObjectsToTrack;// assign in Inspector
    public GameObject[] stuffToAppear;
    private Vector3[] initialPositions;
    private Vector3[] initialPositions2;
    public float duration = 2f;
    [SerializeField] public Transform directionPointer;
    [SerializeField] public Transform pressurePointer;
    float directionPointerX;
    float directionPointerY;
    float pressureSpeed;
    Vector3 iniPos;
    // Start is called before the first frame update
    void Start()
    {
        iniPos = GameObject.Find("BobaCupThrow").transform.position;
        initialPositions = new Vector3[objectsToTrack.Length];
        for (int i = 0; i < objectsToTrack.Length; i++)
            initialPositions[i] = objectsToTrack[i].transform.position;
        initialPositions2 = new Vector3[otherObjectsToTrack.Length];
        for (int i = 0; i < otherObjectsToTrack.Length; i++)
            initialPositions2[i] = otherObjectsToTrack[i].transform.position;
        for (int i = 0; i < stuffToAppear.Length; i++)
        {
            stuffToAppear[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PressurePointer.finalStop)
        { 
            directionPointerX = directionPointer.position.x;
            directionPointerY = directionPointer.position.y;
            pressureSpeed = (pressurePointer.position.y + 4.22f) / 8.44f;
            float finalX = 0;
            BoxCollider2D bc = GetComponent<BoxCollider2D>();
            bc.isTrigger = false;
            
            if (directionPointerX < 0)
            {
                finalX = directionPointerX - Math.Abs(directionPointerY * (3 * pressureSpeed)) * (float)Math.Tan(0.174);
            }
            else
            {
                finalX = Math.Abs(directionPointerY * (3 * pressureSpeed)) * (float)Math.Tan(0.174) + directionPointerX;
            }

            float finalY = Math.Abs(directionPointerY * (3 * pressureSpeed) - directionPointerY);
            Vector3 finalPos = new Vector3(finalX, finalY, 0);
            StartCoroutine(MoveBobaAndReturn(finalPos));
        }
    }

    IEnumerator MoveBobaAndReturn(Vector3 finalPos)
    {
        transform.position = Vector3.MoveTowards(transform.position, finalPos, pressureSpeed * 30f * Time.deltaTime);
        yield return new WaitForSeconds(1);
        while (Vector3.Distance(transform.position, iniPos) > 0.01f)
        {
            transform.position = iniPos;
            MoveTableUp();
            MoveTableDown();
            yield return null;
        }
        yield return new WaitForSeconds(1);
        PressurePointer.finalStop = false;
        PressurePointer.moving = false;
        DirectionPointer.moving = true;
    }

    void MoveTableUp()
    {
        for (int i = 0; i < objectsToTrack.Length; i++)
        {
            var obj = objectsToTrack[i];
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, initialPositions[i], 30f * Time.deltaTime);
        }
        objectsToTrack[4].GetComponent<ChangeCup>().ResetBoba();
        GetComponent<ChangeCup>().ResetBoba();
        BoxCollider2D bc = GetComponent<BoxCollider2D>();
        bc.isTrigger = true;

    }

    void MoveTableDown()
    {
        for (int i = 0; i < otherObjectsToTrack.Length; i++)
        {
            var obj = otherObjectsToTrack[i];
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, initialPositions2[i], 40f * Time.deltaTime);
        }
        for (int i = 0; i < stuffToAppear.Length; i++)
        {
            stuffToAppear[i].SetActive(false);
        }
    }
}

