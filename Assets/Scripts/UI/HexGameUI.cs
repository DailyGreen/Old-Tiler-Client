using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HexGameUI : MonoBehaviour {

	public HexGrid grid;

	HexCell currentCell;

	HexUnit selectedUnit;
    //public static bool wantToBuilt = false;
    public Text cellinfoText = null;
    
	public void SetEditMode (bool toggle) {
		enabled = !toggle;
		grid.ShowUI(!toggle);
		grid.ClearPath();
		if (toggle) {
			Shader.EnableKeyword("HEX_MAP_EDIT_MODE");
		}
		else {
			Shader.DisableKeyword("HEX_MAP_EDIT_MODE");
		}
	}

	void Update () {
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    wantToBuilt = !wantToBuilt;
        //}
		if (!EventSystem.current.IsPointerOverGameObject()) {
			if (Input.GetMouseButtonDown(0) && GameMng.I.e_btnActive.Equals(E_Active.E_NONE)) {
				DoSelection();

                if (currentCell.Unit)
                    Debug.Log("I SELECTE " + currentCell.Unit.code);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                if (selectedUnit)
                {
                    hideNeighbor(selectedUnit.Location);
                }
                // 모든 행동 취소 : 마우스 우클릭
                GameMng.I.e_btnActive = E_Active.E_NONE;
                bControler.CancelAction();
                grid.ClearPath();
                selectedUnit = null;
            }
			else if (selectedUnit)
            {
                // 선택한 유닛이 움직이는 유닛이라면 바로 이동 경로 찾게 해주기
                if (!selectedUnit.imStatic)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        Debug.Log("MOVE");
                        DoMove();
                    }
                    else
                    {
                        DoPathfinding();
                    }
                }
                // 선택한 유닛이 건물이라면 바로 이동 경로 찾게 안해줌
                else
                {
                    // 일꾼 생성하려고 한다면
                    if (GameMng.I.e_btnActive.Equals(E_Active.E_WORKMAN))
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            Debug.Log("일꾼 여기에 생산해 주세요!!!!!!!!!!");
                            GameMng.I.e_btnActive = E_Active.E_NONE;
                            DoBuilt();
                            bControler.CancelAction();
                        }
                        else if (Input.GetMouseButtonDown(1))
                        {
                            Debug.Log("명령을 취소했습니다.");
                            bControler.CancelAction();
                        }
                        else
                        {
                            DoPathfinding();
                        }
                    }
                }
            }
		}
	}

	void DoSelection () {
		grid.ClearPath();
		UpdateCurrentCell();

        GetInfo();

        if (currentCell) {
            //Debug.Log("CC : " + currentCell.coordinates);
			selectedUnit = currentCell.Unit;
            if (selectedUnit)
                showNeighbor();
        }
        //currentCell.SpecialIndex = 4;
	}
    [SerializeField]
    ActiveBControl bControler;

    void GetInfo()
    {
        HexCell cell = grid.GetCell(Camera.main.ScreenPointToRay(Input.mousePosition));
        if (cell.Unit)
        {
            bControler.BuiltAction(cell.Unit.code);
            switch (cell.Unit.code)
            {
                case E_CustomCode.E_BUILDING:
                    cellinfoText.text = "건설중";
                    break;
                case E_CustomCode.E_CASTLE:
                    cellinfoText.text = "성";
                    break;
                case E_CustomCode.E_WORKMAN:
                    GameMng.I.e_btnActive = E_Active.E_MOVE;
                    cellinfoText.text = "일꾼";
                    break;
                default:
                    cellinfoText.text = "평지";
                    break;
            }
        }
        else
        {
            bControler.CancelAction();
            cellinfoText.text = "아무것도 아님 (UI 끄기)";
        }
    }

    void showNeighbor()
    {
        currentCell.GetNeighbor(HexDirection.E).EnableHighlight_2(new Color32(0,0,100,100));
        currentCell.GetNeighbor(HexDirection.NE).EnableHighlight_2(new Color32(0, 0, 100, 100));
        currentCell.GetNeighbor(HexDirection.NW).EnableHighlight_2(new Color32(0, 0, 100, 100));
        currentCell.GetNeighbor(HexDirection.SE).EnableHighlight_2(new Color32(0, 0, 100, 100));
        currentCell.GetNeighbor(HexDirection.SW).EnableHighlight_2(new Color32(0, 0, 100, 100));
        currentCell.GetNeighbor(HexDirection.W).EnableHighlight_2(new Color32(0, 0, 100, 100));
    }

    void hideNeighbor(HexCell cell)
    {
        cell.GetNeighbor(HexDirection.E).DisableHighlight_2();
        cell.GetNeighbor(HexDirection.NE).DisableHighlight_2();
        cell.GetNeighbor(HexDirection.NW).DisableHighlight_2();
        cell.GetNeighbor(HexDirection.SE).DisableHighlight_2();
        cell.GetNeighbor(HexDirection.SW).DisableHighlight_2();
        cell.GetNeighbor(HexDirection.W).DisableHighlight_2();
    }

    void DoPathfinding () {
		if (UpdateCurrentCell()) {
			if (currentCell && selectedUnit.IsValidDestination(currentCell)) {
				grid.FindPath(selectedUnit.Location, currentCell, selectedUnit);
			}
			else {
				grid.ClearPath();
                grid.ClearBuiltTempObj();
			}
		}
	}

    void DoBuilt ()
    {
        if (grid.HasPath)
        {
            //Debug.Log("BUILT PATH : " + grid.GetPath().Count);
            // TODO : 
            grid.ClearPath();
            selectedUnit = null;
            GameMng.I.e_btnActive = E_Active.E_NONE;
        }
    }
    void DoMove () {
		if (grid.HasPath)
        {
            hideNeighbor(selectedUnit.Location);
            if (!selectedUnit.imStatic)
                selectedUnit.Travel(grid.GetPath());
			grid.ClearPath();
            selectedUnit = null;
            GameMng.I.e_btnActive = E_Active.E_NONE;
		}
	}

	bool UpdateCurrentCell () {
		HexCell cell =
			grid.GetCell(Camera.main.ScreenPointToRay(Input.mousePosition));
		if (cell != currentCell) {
			currentCell = cell;
			return true;
		}
		return false;
	}
}