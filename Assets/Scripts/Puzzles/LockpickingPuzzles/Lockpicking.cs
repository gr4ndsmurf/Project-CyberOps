using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Lockpicking : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    private Image ballIMG;
    private Color mainColor;

    public enum LockpickStates { Stage_1, Stage_2, Stage_3, Stage_4, Completed}
    public LockpickStates LockpickCurrentState = LockpickStates.Stage_1;

    public bool completed = false;

    [SerializeField] private GameObject canvas;

    [SerializeField] private Image LockIMG;
    [SerializeField] private Sprite[] stages;

    [SerializeField] private GameObject Items;
    [SerializeField] private GameObject chest;
    [SerializeField] private Animator chestAnim;
    // Start is called before the first frame update
    void Start()
    {
        ballIMG = ball.GetComponent<Image>();
        mainColor = ballIMG.color;

        ball.transform.DOLocalMoveX(500f, 2.0f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

    // Update is called once per frame
    void Update()
    {
        if (canvas.activeSelf == false)
        {
            ball.transform.DOPause();
        }
        else
        {
            ball.transform.DOPlay();

            switch (LockpickCurrentState)
            {
                case LockpickStates.Stage_1:
                    LockIMG.sprite = stages[0];
                    if (ball.transform.localPosition.x > 250 && ball.transform.localPosition.x < 450)
                    {
                        ballIMG.color = Color.green;
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            LockpickCurrentState = LockpickStates.Stage_2;
                            AudioManager.Instance.Play("Lockpick");
                        }
                    }
                    else
                    {
                        ballIMG.color = mainColor;
                    }
                    break;
                case LockpickStates.Stage_2:
                    LockIMG.sprite = stages[1];
                    if (ball.transform.localPosition.x > 0 && ball.transform.localPosition.x < 200)
                    {
                        ballIMG.color = Color.green;
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            LockpickCurrentState = LockpickStates.Stage_3;
                            AudioManager.Instance.Play("Lockpick");
                        }
                    }
                    else
                    {
                        ballIMG.color = mainColor;
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            LockpickCurrentState = LockpickStates.Stage_1;
                            AudioManager.Instance.Play("FailLockpick");
                        }
                    }
                    break;
                case LockpickStates.Stage_3:
                    LockIMG.sprite = stages[2];
                    if (ball.transform.localPosition.x < -50 && ball.transform.localPosition.x > -250)
                    {
                        ballIMG.color = Color.green;
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            LockpickCurrentState = LockpickStates.Stage_4;
                            AudioManager.Instance.Play("Lockpick");
                        }
                    }
                    else
                    {
                        ballIMG.color = mainColor;
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            LockpickCurrentState = LockpickStates.Stage_2;
                            AudioManager.Instance.Play("FailLockpick");
                        }
                    }
                    break;
                case LockpickStates.Stage_4:
                    LockIMG.sprite = stages[3];
                    if (ball.transform.localPosition.x > 0 && ball.transform.localPosition.x < 125)
                    {
                        ballIMG.color = Color.green;
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            LockpickCurrentState = LockpickStates.Completed;
                            AudioManager.Instance.Play("OpenChest");
                        }
                    }
                    else
                    {
                        ballIMG.color = mainColor;
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            LockpickCurrentState = LockpickStates.Stage_3;
                            AudioManager.Instance.Play("FailLockpick");
                        }
                    }
                    break;
                case LockpickStates.Completed:
                    LockIMG.sprite = stages[4];
                    if (!completed)
                    {
                        Debug.Log("Lockpicking Completed");
                        Cursor.visible = false;
                        Items.SetActive(true);
                        canvas.SetActive(false);
                        ball.transform.DOKill(true);
                        chest.GetComponent<BoxCollider2D>().enabled = false;
                        chestAnim.SetBool("Opened", true);
                        completed = true;
                    }

                    break;
                default:
                    break;
            }
        }
    }

    public void CloseCanvas()
    {
        Cursor.visible = false;
        canvas.SetActive(false);
    }
}
