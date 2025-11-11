using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;


public class MoveDownKey : MonoBehaviour
{
    AudioManager audioManager;
    public GameObject[] objectsToTrack;
    public GameObject BobaCup;
    public GameObject BobaCupThrow;
    public GameObject[] otherObjectsToTrack;// assign in Inspector
    private Vector3[] initialPositions;
    private Vector3[] initialPositions2;
    public GameObject[] stuffToAppear;

    public float distance = 20f;
    public float duration = 2f;

    bool isTableDown = false;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

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
       if (Input.GetKeyDown(KeyCode.DownArrow) && !isTableDown)
        {
            audioManager.PlaySFX(audioManager.tableDown);
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
            stuffToAppear[2].transform.position = new Vector3(-7.83f, -4.22f, 0f);
            for (int i = 0; i < stuffToAppear.Length; i++)
            {
                stuffToAppear[i].SetActive(true);
            }
            
            SpriteRenderer sourceRenderer = BobaCup.GetComponent<SpriteRenderer>();
            SpriteRenderer myRenderer = BobaCupThrow.GetComponent<SpriteRenderer>();
            myRenderer.sprite = sourceRenderer.sprite;
      


        }
    
        // Example: press R to reset to initial positions
        
    }
}