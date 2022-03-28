using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private int size = 3;
    [SerializeField] private Cell prefabCell;
    [SerializeField] private Image currentPlayer;

    [SerializeField] private GameObject panel;
    [SerializeField] private Text result;

    [SerializeField] private Text textZeros;
    [SerializeField] private Text textCross;

    private int zeros;
    private int cross;
    public int n;

    private Cell[,] grid;

    private int playerId;

    private void Awake()
    {
        textZeros.text = "" + zeros;
        textCross.text = "" + cross;

        playerId = Random.Range(0, 2);
        spawCellInGrid();
        CurrentPlayer();

    }

    private void spawCellInGrid()
    {
        grid = new Cell[size, size];

        for (int i =0; i<size; i++)
        {
            for (int j=0; j<size; j++)
            {
                grid[i, j] = Instantiate(prefabCell, transform);
                grid[i, j].onCellPressed += CellCheck;
             
            }
           
        }
    }

    private void CellCheck(Cell cell)
    {
        if (cell.playerId! != -1)
            return;
        cell.SetId(playerId);
        n += 1;
        CheckWin();
        PlayerChange();
        CurrentPlayer();
    }

    private void CheckWin()
    {
        int[] rows = new int[size];
        int[] cols = new int[size];

        int leftDiagonal = 0;
        int rightDiagonal = 0;
        result.text = "";

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                int operation = grid[i, j].playerId == 1 ? 1 : grid[i, j].playerId == 0 ? -1 : 0;

                rows[i] += operation;
                cols[j] += operation;

                if (i == j)
                    leftDiagonal += operation;

                if (i + j + 1 == size)
                    rightDiagonal += operation;
            }
        }
            if (leftDiagonal == size|| rightDiagonal == size)
            {
                panel.SetActive(true);
                result.text = "Крестики выиграли!";
                cross++;
                textCross.text = "" + cross;
                Invoke("ResetGame", 1.5f);
                return;
            }

            if (leftDiagonal == -size|| rightDiagonal == -size)
            {
                panel.SetActive(true);
                result.text = "Нолики выиграли!";
                zeros++;
                textZeros.text = "" + zeros;
                Invoke("ResetGame", 1.5f);
                return;
            }

            for (int i=0; i<size; i++)
        {
                if (rows[i] ==size || cols[i] == size)
            {
                panel.SetActive(true);
                result.text = "Крестики выиграли!";
                cross++;
                textCross.text = "" + cross;
                Invoke("ResetGame", 1.5f);
                return;
            }

                if (rows[i] == -size || cols[i] == -size)
            {
                panel.SetActive(true);
                result.text = "Нолики выиграли!";
                zeros++;
                textZeros.text = "" + zeros;
                Invoke("ResetGame", 1.5f);
                return;
            }
                if (n==9)
            {
                panel.SetActive(true);
                result.text = "Ничья!";
                cross++;
                textCross.text = "" + cross;
                zeros++;
                textZeros.text = "" + zeros;
                Invoke("ResetGame", 1.5f);
                return;

            }
        }
    }

    private void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void PlayerChange()
    {
        playerId = playerId == 0 ? 1 : 0;
    }

    private void CurrentPlayer()
    {
        int currentImage = playerId == 0 ? 1 : 2;
        Texture2D texture = Resources.Load<Texture2D>(currentImage.ToString());
        Sprite sprites = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(.5f, .5f));
        currentPlayer.sprite = sprites;
    }

}
