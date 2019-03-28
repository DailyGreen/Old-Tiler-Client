using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameMng : MonoBehaviour
{
    public delegate void CountTurn();
    public CountTurn countDel;
    private static GameMng _Instance = null;

    public static GameMng I
    {
        get
        {
            if (_Instance.Equals(null))
            {
                Debug.Log("instance is null");
            }
            return _Instance;
        }
    }

    void Awake()
    {
        _Instance = this;
    }
    public HexGrid hexgrid;
    public int GetCode
    {
        get
        {
            return GetCellUnderCursor() == null ? 0 : GetCellUnderCursor().CustomCode;
        }
    }
    public HexCell GetCellUnderCursor()
    {
        return hexgrid.GetCell(Camera.main.ScreenPointToRay(Input.mousePosition));
    }

    public E_Active e_btnActive;
    public int Minerals = 50;

    public ProduceWorkMan produceworkman;

    // 턴 세기
    public void AddDelegate(CountTurn Method)
    {
        this.countDel += Method;
    }
    public void RemoveDelegate(CountTurn Method)
    {
        this.countDel -= Method;
    }

    public void CreateBuilt(E_CustomCode e_custom)
    {
        HexCell cell = GetCellUnderCursor();
        if (cell && !cell.Unit)
        {
            if ((int)e_custom < 15)
            {
                cell.CustomCode = -1;     // 건설중인 코드(보류)
            }
            hexgrid.AddBuilt(Instantiate(HexUnit.builtPrefab), cell, 0);
        }
    }

}