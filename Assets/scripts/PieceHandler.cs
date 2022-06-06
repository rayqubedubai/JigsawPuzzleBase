using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceHandler : MonoBehaviour
{
    public Vector3 correctPossition;
    public bool isOnRightPosition = false;
    public bool replacing = false;
    public Vector3 newPosition;
    // Start is called before the first frame update
    void Start()
    {
        correctPossition = transform.position;
        isOnRightPosition = false;
    }

    private void OnEnable()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (replacing)
        {
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * 2);
            if (Vector3.Distance(transform.position, newPosition) < 0.5f)
            {
                replacing = false;
            }
        }
    }

    public void checkCurrentPosition()
    {
        if (Mathf.Abs(Vector3.Distance(transform.position, correctPossition)) < 0.5f)
        {
            isOnRightPosition = true;
            transform.position = correctPossition;
            SceneHandler.Instance.puzzleBoard.incrementScore();
        }
    }

    public void replacePiece(Vector3 newPos)
    {
        newPosition = newPos;
        replacing = true;
    }

    public void setToCorrectPosition()
    {
        transform.position = correctPossition;
    }

}
