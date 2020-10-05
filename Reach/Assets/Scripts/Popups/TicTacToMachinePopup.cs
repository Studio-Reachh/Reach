using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TicTacToMachinePopup : PopupMenu
{
    public GameObject HatchToOpenWhenSolved;
    public SpriteRenderer HatchSpriteRenderer;
    public Sprite ClosedHatchSprite, OpenHatchSprite;

    public Button XButton, OButton;
    public Text[] Textboxes;

    private int _currentNumberOfCharactersInTextbox;
    public string _solution = "XOXX";

    public bool IsSolved;

    public override void Awake()
    {
        if (SaveHandler.GetValueByProperty(SceneManager.GetActiveScene().name, name, "IsSolved", out bool isSolved))
        {
            IsSolved = isSolved;
        }

        base.Awake();
        if (IsSolved)
        {
            OpenHatch();

            for (int i = 0; i < _solution.Length; i++)
            {
                Textboxes[i].text = _solution[i].ToString();
            }
        }
    }

    public void OpenHatch()
    {
        if (HatchSpriteRenderer)
        {
            HatchSpriteRenderer.sprite = OpenHatchSprite;
            HatchSpriteRenderer.transform.parent.GetComponent<Collider2D>().enabled = true;
        }
    }

    private void Update()
    {
        if (_currentNumberOfCharactersInTextbox >= 4 && !IsSolved)
        {
            ResetTextboxes();
        }
    }

    public void XButtonDown()
    {
        if (IsSolved || _currentNumberOfCharactersInTextbox > 4)
        {
            return;
        }

        Textboxes[_currentNumberOfCharactersInTextbox].text = "X";
        CheckIfIsSolved();

        _currentNumberOfCharactersInTextbox++;
    }

    public void OButtonDown()
    {
        if (IsSolved || _currentNumberOfCharactersInTextbox > 4)
        {
            return;
        }

        Textboxes[_currentNumberOfCharactersInTextbox].text = "O";
        CheckIfIsSolved();

        _currentNumberOfCharactersInTextbox++;
    }

    private void CheckIfIsSolved()
    {
        for (int i = 0; i < _solution.Length; i++)
        {
            if (Textboxes[i].text != _solution[i].ToString())
            {
                return;
            }
        }

        SaveHandler.SaveLevel(name, "IsSolved", true);
        IsSolved = true;
        OpenHatch();
    }

    private void ResetTextboxes()
    {
        _currentNumberOfCharactersInTextbox = 0;
        foreach (Text textbox in Textboxes)
        {
            textbox.text = string.Empty;
        }
    }
}
