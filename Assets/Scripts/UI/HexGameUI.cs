using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HexGameUI : MonoBehaviour {

	public HexGrid grid;

	HexCell currentCell;

	HexUnit selectedUnit;
    public static bool wantToBuilt = false;
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
        if (Input.GetKeyDown(KeyCode.Z))
        {
            wantToBuilt = !wantToBuilt;
        }
		if (!EventSystem.current.IsPointerOverGameObject()) {
			if (Input.GetMouseButtonDown(0)) {
				DoSelection();


            }
			else if (selectedUnit)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    DoMove();
                }
                else
                {
                    DoPathfinding();
                }
            }
		}
	}

	void DoSelection () {
		grid.ClearPath();
		UpdateCurrentCell();

        GetInfo();

        if (currentCell) {
            Debug.Log("CC : " + currentCell.coordinates);
			selectedUnit = currentCell.Unit;
		}
        //currentCell.SpecialIndex = 4;
	}

    void GetInfo()
    {
        HexCell cell = grid.GetCell(Camera.main.ScreenPointToRay(Input.mousePosition));
        switch(cell.CustomCode)
        {
            case -1:
                cellinfoText.text = "건설중";
                break;
            case 1:
                cellinfoText.text = "성";
                break;
            case 2:
                cellinfoText.text = "일꾼건물";
                break;
            default:
                cellinfoText.text = "평지";
                break;
        }
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

    void DoMove () {
        //if (selectedUnit.imStatic)
        //    return;
		if (grid.HasPath) {
            if (!selectedUnit.imStatic)
                selectedUnit.Travel(grid.GetPath());
			grid.ClearPath();
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