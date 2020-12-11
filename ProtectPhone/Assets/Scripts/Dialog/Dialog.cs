using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public Text name;
    public Text content;
    public CanvasGroup dia;
    public NPC intereacting;
    public Chat Left;
    public Chat Right;
    private int Page;

    void Start()
    {
        dia.alpha=0;
    }
    void Update()
    {

    }
    public void Show()
    {
        dia.alpha=1;
        this.Hello();
    }
    public void Hide()
    {
        dia.alpha=0;
    }
    public bool Visiable()
    {
        return dia.alpha==1;
    }
    public void ClickRight()
    {
        switch(this.Page)
        {
            case 1:
                this.SayNo();
                break;
            case 2:
                this.Hide();
                break;
        }
    }
    public void ClickLeft()
    {
        switch(this.Page)
        {
            case 1:
                this.SayYes();
                break;
        }
    }
    public void SayYes()
    {
        this.intereacting.Check();
    }
    public void SayNo()
    {
        this.intereacting.Refuse();
    }
    private void Hello()
    {
        this.Left.title.text="给你";
        this.Left.gameObject.SetActive(true);
        this.Right.title.text="不给";
        this.Right.gameObject.SetActive(true);
        this.Page=1;
    }
    public void Bye()
    {
        this.Page=2;
        this.Left.gameObject.SetActive(false);
        this.Right.title.text="再见";
        this.Right.gameObject.SetActive(true);
    }
}
