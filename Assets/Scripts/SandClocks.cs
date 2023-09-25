using UnityEngine;

public class SandClocks : MonoBehaviour
{
    public GameObject SliceParticles;

    public float SlowDuration = 3f;

    public void ShowSliceParticles()
    {
        Instantiate(SliceParticles, transform.position, transform.rotation);
    }
}
