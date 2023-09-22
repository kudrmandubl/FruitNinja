using UnityEngine;

public class Heart : MonoBehaviour
{
    public GameObject SliceParticles;

    public int HealthForHeart = 1;
    
    public void ShowSliceParticles()
    {
        Instantiate(SliceParticles, transform.position, transform.rotation);
    }

}
