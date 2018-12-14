using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, IComparable<Obstacle> {

    public SpriteRenderer MySpriteRender { get; set; }

    private Color defaultColor;

    private Color fadedColor;

    public int CompareTo(Obstacle other)
    {
        if (MySpriteRender.sortingOrder > other.MySpriteRender.sortingOrder)
        {
            return 1;
        }
        else if (MySpriteRender.sortingOrder < other.MySpriteRender.sortingOrder)
        {
            return -1;
        }

        return 0;
    }

    // Use this for initialization
    void Start ()
    {
        MySpriteRender = GetComponent<SpriteRenderer>();

        defaultColor = MySpriteRender.color;
        fadedColor = defaultColor;
        fadedColor.a = 0.7f;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void FadeOut()
    {
        MySpriteRender.color = fadedColor;
    }

    public void FadeIn()
    {
        MySpriteRender.color = defaultColor;
    }
}
