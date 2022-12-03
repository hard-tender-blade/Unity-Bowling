using UnityEngine;

public class HitCube : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Edge") 
        {
            GameObject.FindGameObjectWithTag("Logic").GetComponent<Logic>().ChangeProgresInfo();
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Sounds_Manager>().PLayHitCubeFallDown();
            Destroy(gameObject);
        } 
    }
}