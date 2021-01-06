using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellController: MonoBehaviour
{
    private Button cell;
    private List<Collider2D> adjCell;
    private int cellLayer = 8;
    private CellAliveState alive;
    private CellDeadState dead;
    private CellStates currentState;
    [HideInInspector] public CellStateEnum currState;
    protected void Awake()
    {
        cell = GetComponent<Button>();
        alive = GetComponent<CellAliveState>();
        dead = GetComponent<CellDeadState>();
    }

    private void Start()
    {
        adjCell = new List<Collider2D>();
        cell.onClick.AddListener(ChangeColorOnClick);
        currentState = dead;
        currState = CellStateEnum.Dead;
        EventManager.GenChange += UpdateState;
        EventManager.GenChange += ClearColliderList;
        EventManager.GenChange += DisableButtonInteraction;
        EventManager.ResetGame += ResetLayout;
        EventManager.ResetGame += ClearColliderList;
    }

    private void UpdateState()
    {
        currentState = currentState.Enter(adjCell.Count);
        ChangeCellColor();
    }

    private void ChangeCellColor()
    {
        ColorBlock cb = cell.colors;
        if (currentState == alive)
        {
            cb.normalColor = Color.black;
            currState = CellStateEnum.Alive;
        }
        else if (currentState == dead)
        {
            cb.normalColor = Color.white;
            currState = CellStateEnum.Dead;
        }
        cell.colors = cb;
        cell.gameObject.SetActive(false);
        cell.gameObject.SetActive(true);
    }

    private void ChangeColorOnClick()
    {
        ColorBlock cb = cell.colors;
        if (cell.colors.normalColor == Color.white)
        {
            cb.normalColor = Color.black;
            currentState = alive;
            currState = CellStateEnum.Alive;
        }
        else if (cell.colors.normalColor == Color.black)
        {
            cb.normalColor = Color.white;
            currentState = dead;
            currState = CellStateEnum.Dead;
        }
        cell.colors = cb;
        cell.gameObject.SetActive(false);
        cell.gameObject.SetActive(true);
        ClearColliderList();
    }

    private void ClearColliderList()
    {
        adjCell.Clear();
    }

    private void EnableButtonInteraction()
    {
        cell.onClick.AddListener(ChangeColorOnClick);
        EventManager.GenChange += DisableButtonInteraction;
    }

    private void DisableButtonInteraction()
    {
        cell.onClick.RemoveAllListeners();
        EventManager.GenChange -= DisableButtonInteraction;
    }

    private void ResetLayout()
    {
        EnableButtonInteraction();
        currentState = dead;
        ChangeCellColor();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals(cellLayer) && !adjCell.Contains(collision))
        {
            if (collision.gameObject.GetComponent<CellController>().currState == CellStateEnum.Alive)
            {
                adjCell.Add(collision);
            }
        }
    }
}
