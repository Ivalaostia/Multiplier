﻿using UnityEngine;
using System.Collections.Generic;

namespace Tutorial {
	public struct CursorPanGroup {
		public Vector3 start;
		public Vector3 end;

		public CursorPanGroup(Vector3 s, Vector3 e) {
			this.start = s;
			this.end = e;
		}
	}


	public class Cursor : MonoBehaviour {
		public CanvasGroup cursorGroup;
		public RectTransform rectTransform;
		public Vector3 startingPosition;
		public Vector3 endingPosition;
		public bool isAppearing;
		public bool isPanning;
		public float panningElapsedTime;

		public void Start() {
			this.rectTransform = this.GetComponent<RectTransform>();
			if (this.rectTransform == null) {
				Debug.LogError("Cannot obtain cursor's RectTransform. Please check.");
			}
			this.cursorGroup = this.GetComponent<CanvasGroup>();
			if (this.cursorGroup == null) {
				Debug.LogError("Cannot obtain cursor's CanvasGroup. Please check.");
			}

			this.isAppearing = false;
			this.isPanning = false;
			this.cursorGroup.alpha = 0f;
			this.cursorGroup.interactable = false;
			this.cursorGroup.blocksRaycasts = false;
			this.panningElapsedTime = 0f;

			this.rectTransform.localPosition = Vector3.zero;
			this.endingPosition = this.startingPosition = Vector3.zero;
		}

		public void Update() {
			if (this.isAppearing) {
				if (this.cursorGroup.alpha < 1f) {
					this.cursorGroup.alpha += Time.deltaTime;
					this.isPanning = false;
				}
				else {
					this.isPanning = true;
				}
			}
			else {
				if (this.cursorGroup.alpha > 0f) {
					this.cursorGroup.alpha -= Time.deltaTime;
				}
			}

			if (this.isPanning) {
				this.rectTransform.localPosition = Vector3.Lerp(this.startingPosition, this.endingPosition, this.panningElapsedTime);
				if (this.panningElapsedTime < 1f) {
					this.panningElapsedTime += Time.deltaTime / 2f;
				}
				else {
					this.isPanning = false;
					this.isAppearing = false;

				}
			}
		}



		/// <summary>
		/// True if the cursor is already panning, and I do not want to disturb the panning in progress.
		/// False if the cursor has finished panning, and there's nothing else to do.
		/// </summary>
		/// <param name="start">Vector3 position to indicate where the cursor begins the panning animation.</param>
		/// <param name="end">Vector3 position to indicate where the cursor ends in the panning animation.</param>
		/// <returns></returns>

		public bool PanCursor(Vector3 start, Vector3 end) {
			if (this.isPanning) {
				return false;
			}
			this.startingPosition = start;
			this.endingPosition = end;
			this.rectTransform.localPosition = start;
			this.panningElapsedTime = 0f;
			this.isAppearing = true;
			return true;
		}


		/// <summary>
		/// True if the cursor is already panning, and I do not want to disturb the panning in progress.
		/// False if the cursor has finished panning, and there's nothing else to do.
		/// </summary>
		/// <param name="start">Vector3 position to indicate where the cursor begins the panning animation.</param>
		/// <param name="end">Vector3 position to indicate where the cursor ends in the panning animation.</param>
		/// <returns></returns>
		public bool PanCursor(CursorPanGroup group) {
			if (this.isPanning) {
				return false;
			}
			this.startingPosition = group.start;
			this.endingPosition = group.end;
			this.rectTransform.position = group.start;
			this.panningElapsedTime = 0f;
			this.isAppearing = true;
			return true;
		}

		public void OnDrawGizmosSelected() {
			Gizmos.color = Color.blue;
			Gizmos.DrawLine(this.startingPosition, this.endingPosition);
		}

		public void OnDrawGizmos() {
			Gizmos.color = Color.blue;
			Gizmos.DrawLine(this.startingPosition, this.endingPosition);
		}
	
	}
}
