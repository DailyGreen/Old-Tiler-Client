using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 버튼 정렬이랑 키고 끄기
/// </summary>

/* 버튼 배열
 * 0. 일꾼생성
 * 1. 이동
 * 2. 건설
 * 3. 공격
 * 4. 취소
 */

public class ActiveBControl : MonoBehaviour
{
    public GameObject[] Buttons = new GameObject[5];

    [SerializeField]
    RectTransform[] ButtonsRect = new RectTransform[5];

    Vector3 TempTrans = new Vector3(0, 0, 0);

    [SerializeField]
    int nBtnCount = 0;
    float Result = 0f;
    E_Active e_active;

    private void Start()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            ButtonsRect[i] = Buttons[i].GetComponent<RectTransform>();
        }
    }

    public void CancelAction()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].SetActive(false);
        }
    }

    /* 건물 클릭시 나타나는 버튼 */
    public void BuiltAction(E_CustomCode code)
    {
        switch (code)
        {
            case E_CustomCode.E_BUILDING:

                break;
            case E_CustomCode.E_CASTLE:
                Buttons[0].SetActive(true);
                Buttons[4].SetActive(true);
                break;
            default:

                break;
        }

        //Buttons[Buttons.Length - 1].SetActive(true);
        //for (int i = 0; i < Buttons.Length - 1; i++)
        //{
        //    if (GameMng.I.GetCode.Equals(1))
        //    {
        //        if (i.Equals(0)) { Buttons[i].SetActive(true); }
        //        else { Buttons[i].SetActive(false); }
        //    }
        //}
    }

    /* 유닛 클릭시 나타나는 버튼 */
    public  void UnitsAction()
    {
        Buttons[Buttons.Length - 1].SetActive(true);
        for (int i = 0; i < Buttons.Length; i++)
        {
            if (GameMng.I.GetCode.Equals(1))
            {
                if (!i.Equals(0) && !i.Equals(3))
                {
                    Buttons[i].SetActive(true);
                }
                else { Buttons[i].SetActive(false); }
            }
        }
    }

    public void CancelBtnPostion()
    {
        nBtnCount = 0;
        for (int i = 0; i < Buttons.Length; i++)
        {
            if (Buttons[i].activeSelf) { nBtnCount++; }
        }
        TempTrans = ButtonsRect[Buttons.Length - 1].localPosition;

        Result = (100 * (nBtnCount - 1));
        ButtonsRect[Buttons.Length - 1].localPosition = new Vector3(Result, 50, 0);

        for (int i = Buttons.Length - 1; i > -1; i--)
        {
            if (Buttons[i].activeSelf)
            {
                Result -= 100;
                ButtonsRect[i].localPosition = new Vector3(Result, 50, 0);
            }
        }
    }


    public void MakeWorkman()
    {
        if (GameMng.I.Minerals >= 50)
        {
            GameMng.I.Minerals -= 50;
            //GameMng.I.produceworkman.BuildInit();
            //GameMng.I.
            GameMng.I.e_btnActive = E_Active.E_WORKMAN;
        }
    }
    public void MoveBtn()
    {
        //GM.GameMng.I.e_active = E_Active.E_MOVE;
    }
    public void BuildBtn()
    {
        GameMng.I.e_btnActive = E_Active.E_BUILD;
    }
    public void AttackBtn()
    {
        //GM.GameMng.I.e_active = E_Active.E_ATTACK;
    }
    public void CancelBtn()
    {
        GameMng.I.e_btnActive = E_Active.E_NONE;
        CancelAction();
        //GM.GameMng.I.e_active = E_Active.E_CANCEL;
    }
}
