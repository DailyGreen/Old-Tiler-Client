using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 행동 버튼들 값 넣어주기
/// </summary>

public class UIBtn : MonoBehaviour
{
    public void MakeWorkman()
    {
        if (GameMng.I.Minerals >= 50)
        {
            GameMng.I.Minerals -= 50;
            GameMng.I.produceworkman.BuildInit();
        }
    }
    public void MoveBtn()
    {
        //GM.GameMng.I.e_active = E_Active.E_MOVE;
    }
    public void BuildBtn()
    {
        //GM.GameMng.I.e_active = E_Active.E_BUILD;
    }
    public void AttackBtn()
    {
        //GM.GameMng.I.e_active = E_Active.E_ATTACK;
    }
    public void CancelBtn()
    {
        //GM.GameMng.I.e_active = E_Active.E_CANCEL;
    }
}
