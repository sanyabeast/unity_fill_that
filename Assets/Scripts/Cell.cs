using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : GeneralBehaviour
{
    private float _fillSpeed;
    private Color _fillColor = Color.red;
    private Vector3 _fillScale = Vector3.one;
    // Start is called before the first frame update

    private float _cellSize = 1;
    private Renderer _renderer;
    private GameController _gameController;
    private CellAlignment _cellAlignment;
    public int Column { get; private set; }
    public int Row { get; private set; }

    private LevelSettings _levelSettings;

    public bool Filled { get; private set; }

    private Cell _prevCell;
    private Color _initialColor;
    private Vector3 _initialScale;
    private Color _currentTargetColor = Color.white;
    private Vector3 _currentDesiredScale = Vector3.one;
    

    void OnEnable()
    {
        Column = (int)RoundToNearest(transform.position.x, _cellSize);
        Row = (int)RoundToNearest(transform.position.y, _cellSize);

        _levelSettings = FindObjectOfType<LevelSettings>();
        _renderer = GetComponent<Renderer>();
        _gameController = FindObjectOfType<GameController>();
        _cellAlignment = GetComponent<CellAlignment>();
        _cellSize = _cellAlignment.cellSize;
        _gameController.RegisterCell(this);
        _gameController.Dropped += HandleGameDrop;
        _initialScale = transform.localScale;

        _initialColor = _levelSettings.cellColor;
        _fillColor = _levelSettings.fillColor;
        _fillScale = _levelSettings.fillScale;
        _fillSpeed = _levelSettings.fillSpeed;

        _currentTargetColor = _initialColor;
        _currentDesiredScale = _initialScale;

        

        Debug.Log($"Enabled new cell: {gameObject.name} with indexes [{Column}:{Row}]");
    }

    private void HandleGameDrop(int obj)
    {
        _prevCell = null;
        Toggle(false);
    }

    public void Toggle(bool on)
    {
        Filled = on ? true : false;
        _currentTargetColor = on ? _fillColor * (0.25f + 0.75f * _gameController.GetGameProgress()) : _initialColor;
        _currentDesiredScale = on ? _fillScale : _initialScale;

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _renderer.material.color = Color.Lerp(_renderer.material.color, _currentTargetColor, (_fillSpeed));
        transform.localScale = Vector3.Lerp(transform.localScale, _currentDesiredScale, (_fillSpeed));
    }


    public void SetPrevCell(Cell cell)
    {
        _prevCell = cell;
    }

    public Cell GetPrevCell()
    {
        return _prevCell;
    }


}
