using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] int FPS;

    void Start()
    {
        Cursor.visible = false;
        Application.targetFrameRate = FPS;
    }
}