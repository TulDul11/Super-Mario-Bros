using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform player;
    [SerializeField] Transform endLimit;
    
    // Private
    float offset;
    float startX;
    float endX;
    float viewportHalfwidth;

    void Start()
    {
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        viewportHalfwidth = Math.Abs(bottomLeft.x - this.transform.position.x);
        offset = this.transform.position.x - player.position.x;
        startX = this.transform.position.x;
        endX = endLimit.transform.position.x - viewportHalfwidth;
    }

    void Update()
    {
        float desiredX =  player.position.x  +  offset;
        if (desiredX > startX && desiredX < endX)
        {
            this.transform.position = new Vector3(desiredX, this.transform.position.y, this.transform.position.z);
        }
        
    }
}
