using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    //ConfigParams
    [SerializeField] float minX = 1f;
    [SerializeField] float maxX = 15f;

    [SerializeField] float screenWidthInUnits = 16f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mousePosInUnits = Input.mousePosition.x / Screen.width * screenWidthInUnits;
        Vector3 paddlePosition = new Vector3(transform.position.x, transform.position.y, 1);
        paddlePosition.x = Mathf.Clamp(mousePosInUnits, minX, maxX);
        gameObject.transform.position = paddlePosition;
    }
}
