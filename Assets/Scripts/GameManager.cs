using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public Camera mycam;
    public GameObject pointer;
    [SerializeField]
    GameObject[] pointerChild;

    int[,] tiles = new int[,] {
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
    };
    //int[,] tiles = new int[,] {
    //    { 0, 0, 0, 2, -2, -2, -2, -3, -3, 3, 3, 5, 4, 4 },
    //    { 0, 0, 0, 2, -1, -1, -2, 1, 1, 3, 3, 5, 4, 4 },
    //    { 0, 0, 0, 2, 1, 1, -1, 1, 1, 3, 3, 5, 4, 4 },
    //    { 0, 0, 0, 2, 1, 1, 1, 1, 1, 3, 3, 5, 4, 4 },
    //    { 0, 0, 0, 2, 1, 1, 1, 1, 1, 3, 3, 5, 4, 4 },
    //    { 0, 0, 0, 2, 1, 1, 1, 1, 1, 3, 3, 5, 4, 4 },
    //    { 0, 0, 0, 2, 1, 1, 1, 1, 1, 3, 3, 5, 4, 4 },
    //    { 0, 0, 0, 2, 1, 1, 1, 1, 1, 3, 3, 5, 4, 4 },
    //    { 0, 0, 0, 2, 1, 1, 1, 1, 1, 3, 3, 5, 4, 4 },
    //    { 0, 0, 0, 2, 1, 1, 1, 1, 1, 3, 3, 5, 4, 4 },
    //    { 0, 0, 0, 2, 1, 1, 1, 1, 1, 3, 3, 5, 4, 4 }
    //};

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
            pointer.SetActive(false);

        if (Input.GetMouseButtonDown(0))
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("FFFFFFF");
                RaycastHit hit;
                Ray ray = mycam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, 300.0f))
                    if (hit.transform != null)
                        if (hit.transform.tag.Equals("Tile"))
                        {
                            int posY = hit.transform.GetComponent<Tile>().posY;
                            int posX = hit.transform.GetComponent<Tile>().posX;
                            int code = hit.transform.GetComponent<Tile>().code;
                            Debug.Log(posY + " : " + posX + " : " + code);

                            if (code >= 0)
                            {
                                pointer.transform.position = new Vector3(hit.transform.position.x, 1.6f, hit.transform.position.z);
                            }
                            else
                            {
                                pointer.transform.position = new Vector3(hit.transform.position.x, 0.21f, hit.transform.position.z);
                            }
                            int[,] arr = new int[6, 2] { { 1, 0 }, { 1, 1 }, { 0, 1 }, { -1, 1 }, { -1, 0 }, { 0, -1 } };
                            if (posY % 2 == 0)
                                arr = new int[6, 2] { { 1, -1 }, { 1, 0 }, { 0, 1 }, { -1, 0 }, { -1, -1 }, { 0, -1 } };
                            for (int i = 0; i < 6; i++)
                            {
                                if (posY + arr[i, 0] >= 0 && posY + arr[i, 0] < 7 &&
                                    posX + arr[i, 1] >= 0 && posX + arr[i, 1] < 14)
                                {
                                    if (code >= 0)
                                    {
                                        pointerChild[i].transform.localPosition = new Vector3(
                                            pointerChild[i].transform.localPosition.x,
                                            (tiles[posY + arr[i, 0], posX + arr[i, 1]]) >= 0 ? 0 : -0.64f,
                                            pointerChild[i].transform.localPosition.z);
                                    }
                                    else
                                    {
                                        pointerChild[i].transform.localPosition = new Vector3(
                                            pointerChild[i].transform.localPosition.x,
                                            (tiles[posY + arr[i, 0], posX + arr[i, 1]]) >= 0 ? 0.64f : 0,
                                            pointerChild[i].transform.localPosition.z);
                                    }
                                    pointerChild[i].gameObject.SetActive(true);
                                }
                                else
                                {
                                    pointerChild[i].gameObject.SetActive(false);
                                }
                            }
                            pointer.SetActive(true);
                        }
            }
    }
}
