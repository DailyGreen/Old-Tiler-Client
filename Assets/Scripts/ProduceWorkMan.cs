using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProduceWorkMan : SObject
{
    private void Start()
    {
        setactive = false;
        e_code = E_CustomCode.E_BWORKMAN;
        nCount = 1;
    }
    void Update()
    {
        if(GameMng.I.GetCode.Equals(-1))
        {
            BuildInit();
        }
    }

    public void BuildInit()
    {
        setactive = true;
        DelegateSetting();
    }

    public void DelegateSetting()
    {
       if (!setactive) { nCount = 1;
        GameMng.I.AddDelegate(Calc); 
       GameMng.I.CreateBuilt(E_CustomCode.E_BUILDING); }
    }

    public void Calc()
    {
        nCount--;
       if (nCount.Equals(0)) {
            GameMng.I.RemoveDelegate(Calc);
            Debug.Log("생성완료");
            GameMng.I.CreateBuilt(e_code);
            GameMng.I.hexgrid.GetCell(this.transform.position).CustomCode = (int)e_code;
        }

    }
}