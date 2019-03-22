using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Camera mycam;
    public GameObject pointer;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = mycam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform != null)
                {
                    if (hit.transform.tag.Equals("Tile_Yield"))
                        pointer.transform.position = new Vector3(hit.transform.position.x, 0.85f, hit.transform.position.z);
                    else if (hit.transform.tag.Equals("Tile_Water"))
                        pointer.transform.position = new Vector3(hit.transform.position.x, 0.42f, hit.transform.position.z);
                }
            }
        }
    }
}
