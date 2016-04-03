﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class TooltipHint : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	public string tooltipText;
	public BaseTooltip tooltip;

	public void Start() {
		if (this.tooltipText.Length <= 0) {
			this.tooltipText = "Hello world.";
		}
		Canvas canvas = GameObject.FindObjectOfType<Canvas>();
		if (canvas != null) {
			TooltipLocator loc = canvas.GetComponent<TooltipLocator>();
			if (loc.tooltip != null) {
				this.tooltip = loc.tooltip;
			}
			else {
				Debug.LogError("Something is wrong.");
			}
		}
	}

	public void OnPointerEnter(PointerEventData eventData) {
		this.tooltip.SetToolTipHidden(false);
		this.tooltip.SetText(this.tooltipText);
		RectTransform rect = this.GetComponent<RectTransform>();
		this.tooltip.SetTarget(rect);
		this.tooltip.transform.position = this.transform.position;

		Debug.LogWarning("Target RectTransform: " + rect.localPosition + " " + rect.position);
		
	}

	public void OnPointerExit(PointerEventData eventData) {
		this.tooltip.SetToolTipHidden(true);
	}
}
