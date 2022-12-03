using UnityEngine;

public class Sounds_Manager : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip Win;
    [SerializeField] AudioClip HitCubeFallDown;
    [SerializeField] AudioClip BallRespawnSound;
    [SerializeField] float Volume;

    public void PLayWin()
    {
        source.PlayOneShot(Win);
    }
    public void PLayHitCubeFallDown()
    {
        source.PlayOneShot(HitCubeFallDown);
    }
    public void PLayBallRespawnSound()
    {
        source.PlayOneShot(BallRespawnSound);
    }

}