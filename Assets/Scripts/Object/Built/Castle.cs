using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 일꾼 생산 건물
public class Castle : SObject
{
    private void Start()
    {
        setactive = false;
        code = E_CustomCode.E_CASTLE;
        nCount = 1;
        DelegateSetting();
    }
    //void Update()
    //{
    //    if(GameMng.I.GetCode.Equals(-1))
    //    {
    //        BuildInit();
    //    }
    //}

    //public void BuildInit()
    //{
    //    setactive = true;
    //    DelegateSetting();
    //}

    public void DelegateSetting()
    {
        nCount = 1;
        GameMng.I.AddDelegate(Calc);
        //GameMng.I.CreateBuilt(E_CustomCode.E_BUILDING);
    }

    public void Calc()
    {
        nCount--;
       if (nCount.Equals(0)) {
            GameMng.I.RemoveDelegate(Calc);
            Debug.Log("생성완료");
            GameMng.I.CreateBuilt(code);
            GameMng.I.hexgrid.GetCell(this.transform.position).Unit.code = code;
        }

    }
}