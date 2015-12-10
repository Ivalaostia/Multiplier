﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

namespace SinglePlayer.UI {

	[Serializable]
	public struct LevelRate {
		public int level;
		public float rate;
		public int isIncreasing;
		public Category category;

		public LevelRate(Category cat, int level, float rate) {
			this.category = cat;
			this.level = level;
			this.rate = rate;
			this.isIncreasing = 0;
		}
	}

	public class LevelRateHandler : MonoBehaviour {
		public GameObject panelItemPrefab;
		public List<List<LevelRate>> allAttributes;

		public void Start() {
			if (this.panelItemPrefab == null) {
				Debug.LogError("Panel item prefab is null. Please check.");
			}
			this.allAttributes = new List<List<LevelRate>>();
			foreach (Category cat in Category.Values) {
				List<LevelRate> tempList = new List<LevelRate>();
				for (int j = 0; j < AttributePanelUI.MAX_NUMBER_OF_LEVELS; j++) {
					LevelRate rate = new LevelRate(cat, j + 1, 1f);
					tempList.Add(rate);
				}
				this.allAttributes.Add(tempList);
			}

			//Panel Items
			for (int i = 0; i < AttributePanelUI.MAX_NUMBER_OF_LEVELS; i++) {
				//Panel items
				GameObject obj = MonoBehaviour.Instantiate(this.panelItemPrefab) as GameObject;
				RectTransform rectTrans = obj.GetComponent<RectTransform>();
				obj.transform.SetParent(this.transform);
				if (rectTrans != null) {
					rectTrans.localScale = Vector3.one;
				}
				PanelItem item = obj.GetComponent<PanelItem>();
				if (item != null) {
					LevelRate health = this.allAttributes[0][i];
					item.levelText.text = "Level " + health.level.ToString();
					item.rateText.text = health.rate.ToString();
					item.isIncreasingText.text = "N/A";
					this.allAttributes[0][i] = health;
				}
			}
		}

		public void ChangeCategory(Category cat) {
			//Panel Items
			List<LevelRate> tempList = this.allAttributes[cat.value];
			if (tempList != null) {
				for (int i = 0; i < tempList.Count; i++) {
					//Panel items
					Transform child = this.transform.GetChild(i);
					PanelItem item = child.GetComponent<PanelItem>();
					if (item != null) {
						LevelRate levelRate = tempList[i];
						item.levelText.text = "Level " + levelRate.level.ToString();
						item.rateText.text = levelRate.rate.ToString();
						if (levelRate.isIncreasing > 0) {
							item.isIncreasingText.text = "++";
						}
						else if (levelRate.isIncreasing < 0) {
							item.isIncreasingText.text = "--";
						}
						else {
							item.isIncreasingText.text = "N/A";
						}
						tempList[i] = levelRate;
					}
				}
			}
			else {
				Debug.LogError("There's something wrong with the attributes panel UI. Please check.");
			}
		}
	}
}
