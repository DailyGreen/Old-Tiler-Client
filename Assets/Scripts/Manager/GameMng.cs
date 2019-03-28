using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameMng : MonoBehaviour
{
    public delegate void CountTurn();
    public CountTurn countDel;
    private static GameMng _Instance = null;

    //public bool wantToBuilt = false;

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
    public E_CustomCode GetCode
    {
        get
        {
            if (GetCellUnderCursor().Equals(null))
                return 0;
            if (GetCellUnderCursor().Unit)
                return GetCellUnderCursor().Unit.code;
            return 0;
        }
    }
    public HexCell GetCellUnderCursor()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
            return hexgrid.GetCell(Camera.main.ScreenPointToRay(Input.mousePosition));
        return null;
    }

    public E_Active e_btnActive;
    public int Minerals = 450;

    //public ProduceWorkMan produceworkman;

    // 턴 세기
    public void AddDelegate(CountTurn Method)
    {
        this.countDel += Method;
    }
    public void RemoveDelegate(CountTurn Method)
    {
        this.countDel -= Method;
    }

    [SerializeField]
    HexUnit[] unitsPrefab;
    [SerializeField]
    HexUnit[] builtsPrefab;

    public void CreateBuilt(E_CustomCode e_custom)
    {
        HexCell cell = GetCellUnderCursor();
        if (cell && !cell.Unit)
        {
            /*if ((int)e_custom < 15)
            {
                cell.CustomCode = -1;     // 건설중인 코드(보류)
            }*/
            hexgrid.AddBuilt(Instantiate(builtsPrefab[(int)e_custom - 1]), cell, 0, e_custom);
        }
    }

    public void CreateUnit(E_CustomCode e_custom)
    {
        HexCell cell = GetCellUnderCursor();
        if (cell && !cell.Unit)
        {
            hexgrid.AddUnit(Instantiate(unitsPrefab[(int)e_custom - (int)E_CustomCode.E_NOW_CHARACTER - 1]), cell, 0, e_custom);
        }
    }
}