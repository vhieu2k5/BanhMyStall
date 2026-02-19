using UnityEngine;

public class CameraFixedWidth : MonoBehaviour
{
    public float targetWidth = 1920f; // chiều ngang chuẩn bạn muốn
    public float targetHeight = 1080f;

    void Update()
    {
        float targetAspect = targetWidth / targetHeight;
        float windowAspect = (float)Screen.width / (float)Screen.height;
        Camera cam = GetComponent<Camera>();

        if (windowAspect >= targetAspect)
        {
            // màn hình rộng hơn → fix theo chiều cao
            cam.orthographicSize = targetHeight / 200f; 
        }
        else
        {
            // màn hình hẹp hơn → scale theo chiều ngang
            float scale = targetAspect / windowAspect;
            cam.orthographicSize = (targetHeight / 200f) * scale;
        }
    }
}
