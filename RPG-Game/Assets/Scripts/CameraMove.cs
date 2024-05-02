using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Camera cam;
    private Vector3 playerPos;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        playerPos = cam.WorldToScreenPoint(player.position);
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = cam.WorldToScreenPoint(player.position);
        if (playerPos.x <= 0)
        {
            this.transform.position = new Vector3(this.transform.position.x - 15.925f, this.transform.position.y, this.transform.position.z);
        }   
        else if (playerPos.x >= Screen.width)
        {
            this.transform.position = new Vector3(this.transform.position.x + 15.925f, this.transform.position.y, this.transform.position.z);
        }
        if (playerPos.y <= 0)
        {
            Debug.Log(playerPos);
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 8.96f, this.transform.position.z);
        }
        else if (playerPos.y >= Screen.height)
        {
            Debug.Log(playerPos);
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 8.96f, this.transform.position.z);
        }
    }
}