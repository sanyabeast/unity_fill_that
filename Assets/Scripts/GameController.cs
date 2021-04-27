using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : GeneralBehaviour
{
    // Start is called before the first frame update
    public event Action<int> Dropped;
    public int TotalCellsCount { get; private set; }
    private int _filledCellsCount = 0;
    private Cell _prevCell;
    private LevelSettings _levelSettings;
    private bool _currentLevelCompleted;

    void Awake(){
        _levelSettings = FindObjectOfType<LevelSettings>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_currentLevelCompleted == false){
            if (Input.GetMouseButton(0))
            {
                Cell cell = GetCellUnderPointer();
                if (cell != null)
                {
                    if (cell.Filled)
                    {
                        TryToClearCell(cell);
                    }
                    else
                    {
                        TryToFillCell(cell);
                    }
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Drop();
            }
        }
    }

    private void TryToClearCell(Cell cell)
    {
        Debug.Log($"trying to clear cell {cell.gameObject.name} with index [{cell.Column}:{cell.Row}]");
        if (ClearingIsAllowed(cell))
        {
            Cell newPrevCell = _prevCell.GetPrevCell();
            _prevCell.Toggle(false);
            _prevCell.SetPrevCell(null);
            _prevCell = newPrevCell;
            _filledCellsCount--;
            _levelSettings.PlayClearSound();
        }
    }

    private bool ClearingIsAllowed(Cell cell)
    {
        if (_prevCell == null || cell == null)
        {
            return false;
        }

        if (_prevCell.GetPrevCell() == cell)
        {
            return true;
        }

        return false;
    }

    private void TryToFillCell(Cell cell)
    {
        if (FillingIsAllowed(cell))
        {
            cell.Toggle(true);
            cell.SetPrevCell(_prevCell);
            _filledCellsCount++;
            _prevCell = cell;
            _levelSettings.PlayFillSound();
        }

        if (IsCompletelyFilled()){
            _currentLevelCompleted = true;
            OnLevelCompleted();
        }
    }

    private void OnLevelCompleted()
    {
        _levelSettings.PlayLevelCompletedSound();
        StartCoroutine(FinishLevelCompletion());
    }

    private IEnumerator FinishLevelCompletion()
    {
        yield return new WaitForSeconds(3);
        _levelSettings.GoToNextLevel();
    }

    private bool FillingIsAllowed(Cell cell)
    {
        if (cell.Filled)
        {
            return false;
        }

        if (_prevCell == null)
        {
            return true;
        }

        if (_prevCell.Column == cell.Column && Math.Abs(_prevCell.Row - cell.Row) == 1)
        {
            return true;
        }

        if (_prevCell.Row == cell.Row && Math.Abs(_prevCell.Column - cell.Column) == 1)
        {
            return true;
        }

        return false;
    }


    public void RegisterCell(Cell cell)
    {
        TotalCellsCount++;
        Debug.Log($"registered new cell. total cells count {TotalCellsCount}");
    }

    private void Drop()
    {
        _prevCell = null;
        Dropped.Invoke(0);
        _filledCellsCount = 0;
        _levelSettings.PlayDropSound();
    }

    public float GetGameProgress() => (float)_filledCellsCount / (float)TotalCellsCount;
    public bool IsCompletelyFilled() => _filledCellsCount == TotalCellsCount;


    private Cell GetCellUnderPointer()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            Cell cell = hitInfo.collider.GetComponent<Cell>();
            return cell;
        }
        return null;
    }

}
