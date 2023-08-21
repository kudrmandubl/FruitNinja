using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject Whole;
    public GameObject Sliced;
    public GameObject TopPart;
    public GameObject BottomPart;

    private Collider _sliceTrigger;

    public void Slice(Vector3 direction)
    {
        SetSliced();
        RotateBySliceDirection(direction);
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _sliceTrigger = GetComponent<Collider>();
    }

    private void SetSliced()
    {
        Whole.SetActive(false);
        Sliced.SetActive(true);

        _sliceTrigger.enabled = false;
    }

    private void RotateBySliceDirection(Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

}
