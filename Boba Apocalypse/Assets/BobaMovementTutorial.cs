using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using UnityEditor;

public class BobaMovementTutorial : MonoBehaviour
{
    AudioManager audioManager;
    public GameObject[] objectsToTrack;
    public GameObject BobaCup;
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
    
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
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


        if (PressurePointerTutorial.finalStop)
        {
            
            directionPointerX = directionPointer.position.x;
            directionPointerY = directionPointer.position.y;
            pressureSpeed = (pressurePointer.position.y + 4.22f) / 8.44f;
            float finalX = 0;
            BoxCollider2D bc = GetComponent<BoxCollider2D>();
            bc.isTrigger = false;

            if (directionPointerX < 0)
            {
                Debug.Log($"Direction is: {directionPointerX}");
                finalX = directionPointerX - Math.Abs(directionPointerY * (Time.deltaTime * pressureSpeed)) * (float)Math.Tan(0.174);
            }
            else
            {
                finalX = Math.Abs(directionPointerY * (3 * pressureSpeed)) * (float)Math.Tan(0.174) + directionPointerX;
            }

            float finalY = Math.Abs(directionPointerY * (3 * pressureSpeed) - directionPointerY);
            Vector3 finalPos = new Vector3(finalX, finalY, 0);
            
            transform.position = Vector3.MoveTowards(transform.position, finalPos, pressureSpeed * 30f * Time.deltaTime);
            if (Vector3.Distance(transform.position, finalPos) < 0.01f)
            {

            StartCoroutine(MoveBobaAndReturn(finalPos));

            }

        }
    }
     IEnumerator MoveBobaAndReturn(Vector3 finalPos)
    {
        transform.position = Vector3.MoveTowards(transform.position, finalPos, pressureSpeed * 30f * Time.deltaTime);
        yield return new WaitForSeconds(1);
        while (Vector3.Distance(transform.position, iniPos) > 0.01f)
        {
            transform.position = iniPos;
            MoveTableUpTween();
            MoveTableDownTween();
            yield return null;
        }
        yield return new WaitForSeconds(1);
        PressurePointer.finalStop = false;
        PressurePointer.moving = false;
        DirectionPointer.moving = true;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
{

    MoveTableUpTween();
    MoveTableDownTween();
    transform.position = iniPos;
    PressurePointer.finalStop = false;
    PressurePointer.moving = false;
    DirectionPointer.moving = true;

    
}
    
   public void MoveTableUpTween()
    {

    if (objectsToTrack == null || objectsToTrack.Length == 0) return;

    for (int i = 0; i < objectsToTrack.Length; i++)
    {
        if (objectsToTrack[i] != null)
            objectsToTrack[i].transform.DOMove(initialPositions[i], 1f);
    }

    if (objectsToTrack.Length > 4 && objectsToTrack[4] != null)
    {
        var cup = objectsToTrack[4].GetComponent<ChangeCup>();
        if (cup != null)
            cup.ResetBoba();
    }
}

public void MoveTableDownTween()
{
    for (int i = 0; i < otherObjectsToTrack.Length; i++)
        {
        
        otherObjectsToTrack[i].transform.DOMove(initialPositions2[i], 1f);
    }
    foreach (var obj in stuffToAppear)
        obj.SetActive(false);
}
}

