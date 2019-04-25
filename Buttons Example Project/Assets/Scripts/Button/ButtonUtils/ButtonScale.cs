using i5.Utilities.Debugging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScale : MonoBehaviour
{
    [SerializeField] private Transform buttonBackground;
    [SerializeField] private Transform frame;
    [SerializeField] private Vector3 buttonSize = new Vector3(0.1f, 0.1f, 0.007f);
    [SerializeField] private float frameThickness = 1f;

    private SpriteRenderer frameRenderer;

    public Vector3 ButtonSize
    {
        get { return buttonSize; }
        set
        {
            buttonSize = value;
            UpdateSize();
        }
    }

    public float FrameThickness
    {
        get { return frameThickness; }
        set
        {
            frameThickness = value;
            UpdateSize();
        }
    }

    private void Awake()
    {
        CheckSetup();
        frameRenderer = frame.GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Called in the editor if a variable in the inspector is changed
    /// </summary>
    private void OnValidate()
    {
        CheckSetup();
        if (frameRenderer == null)
        {
            frameRenderer = frame.GetComponent<SpriteRenderer>();
        }
        UpdateSize();
    }

    private void UpdateSize()
    {
        buttonBackground.localScale = buttonSize;
        frameRenderer.size = new Vector2(buttonSize.x * 100, buttonSize.y * 100);
    }

    private bool CheckSetup()
    {
        bool setUpCorrectly = true;
        if (buttonBackground == null)
        {
            SpecialDebugMessages.LogMissingReferenceError(this, nameof(buttonBackground));
            setUpCorrectly = false;
        }
        if (frame == null)
        {
            SpecialDebugMessages.LogMissingReferenceError(this, nameof(frame));
            setUpCorrectly = false;
        }
        else
        {
            if (frame.GetComponent<SpriteRenderer>() == null)
            {
                SpecialDebugMessages.LogComponentNotFoundError(this, nameof(SpriteRenderer), frame.gameObject);
                setUpCorrectly = false;
            }
        }

        return setUpCorrectly;

    }
}
