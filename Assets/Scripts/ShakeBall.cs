using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShakeBall : MonoBehaviour
{
    [SerializeField] private List<GameObject> shakingBalls = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ShakingNum());
        }
    }

    void FinishShaking(int i)
    {
        for (int j = 0; j < shakingBalls[i].transform.childCount; j++)
        {
            shakingBalls[i].transform.GetChild(j).parent = null;
            shakingBalls[i].transform.GetChild(j).GetComponent<Rigidbody>().isKinematic = false;
        }
        shakingBalls[i].SetActive(false);
    }
    IEnumerator ShakingNum()
    {
        for (int i = 0; i < shakingBalls.Count; i++)
        {
            Debug.Log("girdi");
            yield return new WaitForSeconds(0.1f);
            shakingBalls[i].gameObject.transform.DOShakePosition(0.3f, new Vector3(0.5f, 0.5f, 0.5f), 10, 90f,false)
                .OnComplete(() => FinishShaking(i));;
        }
    }
}