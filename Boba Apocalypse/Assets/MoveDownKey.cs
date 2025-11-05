using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class MoveDownKey : MonoBehaviour
{
    public GameObject[] objectsToTrack;
    public GameObject BobaCup;
    public GameObject BobaCupThrow;
    public GameObject[] otherObjectsToTrack;// assign in Inspector
    private Vector3[] initialPositions;
    private Vector3[] initialPositions2;
    public GameObject[] stuffToAppear;

    public float distance = 20f;
    public float duration = 2f;

    void Start()
{
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

    void Update()
    {
        // Example: press space to move all down relative to initial position
       if (Input.GetKeyDown(KeyCode.DownArrow))
        {
           
            for (int i = 0; i < objectsToTrack.Length; i++)
            {
                var obj = objectsToTrack[i];

                Vector3 startPos = obj.transform.position; // current pos
                Vector3 peakPos = startPos + Vector3.up * 0.2f;
                Vector3 endPos = initialPositions[i] + Vector3.down * 4; // always relative to start
                Sequence seq = DOTween.Sequence();
                seq.Append(obj.transform.DOMove(peakPos, duration * 0.2f).SetEase(Ease.OutQuad));
                seq.Append(obj.transform.DOMove(endPos, duration * 0.8f).SetEase(Ease.InQuad));
            }

            for (int i = 0; i < otherObjectsToTrack.Length; i++)
            {
                var obj = otherObjectsToTrack[i];

                Vector3 startPos = obj.transform.position; // current pos
                Vector3 peakPos = startPos + Vector3.down * 0.2f;
                Vector3 endPos = initialPositions2[i] + Vector3.up * 7.2f;
                Sequence seq = DOTween.Sequence();
                seq.Append(obj.transform.DOMove(peakPos, duration * 0.2f).SetEase(Ease.OutQuad));
                seq.Append(obj.transform.DOMove(endPos, duration * 0.8f).SetEase(Ease.InQuad));

            }
            for (int i = 0; i < stuffToAppear.Length; i++)
            {
                stuffToAppear[i].SetActive(true);
            }
            
             SpriteRenderer sourceRenderer = BobaCup.GetComponent<SpriteRenderer>();
            SpriteRenderer myRenderer = BobaCupThrow.GetComponent<SpriteRenderer>();
            myRenderer.sprite = sourceRenderer.sprite;


        }
    
        // Example: press R to reset to initial positions
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            for (int i = 0; i < objectsToTrack.Length; i++)
            {
                var obj = objectsToTrack[i];

                Vector3 peakPos = initialPositions[i] + Vector3.up * 0.2f;
                Vector3 endPos = initialPositions[i]; // always relative to start
                Sequence seq = DOTween.Sequence();
                seq.Append(obj.transform.DOMove(peakPos, duration).SetEase(Ease.OutQuad));
                seq.Append(obj.transform.DOMove(endPos, duration * 0.8f).SetEase(Ease.InQuad));
            }
            for (int i = 0; i < otherObjectsToTrack.Length; i++)
           {
            var obj = otherObjectsToTrack[i];

                Vector3 peakPos = initialPositions2[i] + Vector3.down * 0.2f;
                Vector3 endPos = initialPositions2[i]; // always relative to start
                Sequence seq = DOTween.Sequence();
                seq.Append(obj.transform.DOMove(peakPos, duration).SetEase(Ease.OutQuad));
                seq.Append(obj.transform.DOMove(endPos, duration * 0.8f).SetEase(Ease.InQuad));
                }
            for (int i = 0; i < stuffToAppear.Length; i++)
            {
                stuffToAppear[i].SetActive(false);
            }
        }
    }
}