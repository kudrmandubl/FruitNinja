using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject Whole;
    public GameObject Sliced;
    public Rigidbody TopPartRigidbody;
    public Rigidbody BottomPartRigidbody;

    private Rigidbody _mainRigidbody;
    private Collider _sliceTrigger;

    public void Slice(Vector3 direction, Vector3 position, float force)
    {
        SetSliced();
        RotateBySliceDirection(direction);
        AddForce(TopPartRigidbody, direction, position, force);
        AddForce(BottomPartRigidbody, direction, position, force);
    }

    private void Start()
    {
        FillComponents();
    }

    private void FillComponents()
    {
        _mainRigidbody = GetComponent<Rigidbody>();
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

    private void AddForce(Rigidbody sliceRigidbody, Vector3 direction, Vector3 position, float force)
    {
        sliceRigidbody.velocity = _mainRigidbody.velocity;
        sliceRigidbody.angularVelocity = _mainRigidbody.angularVelocity;
        sliceRigidbody.AddForceAtPosition(direction * force, position);
    }

}
