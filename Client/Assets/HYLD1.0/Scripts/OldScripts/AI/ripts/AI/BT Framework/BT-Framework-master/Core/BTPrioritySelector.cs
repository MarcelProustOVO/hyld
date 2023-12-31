﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BT {

	/// <summary>
	/// BTPrioritySelector selects the first sussessfully evaluated child as the active child.
	/// </summary>
	public class BTPrioritySelector : BTNode {
		
		private BTNode _activeChild;

		public BTPrioritySelector (BTPrecondition precondition = null) : base (precondition) {}

		// selects the active child
		protected override bool DoEvaluate () {
			foreach (BTNode child in children) {
				//Logging.HYLDDebug.LogError(child);
				if (child.Evaluate()) {
					
					if (_activeChild != null && _activeChild != child) {
						_activeChild.Clear();	
					}
					_activeChild = child;
					return true;
				}
			}

			if (_activeChild != null) {
				_activeChild.Clear();
				_activeChild = null;
			}

			return false;
		}
		
		public override void Clear () {
			if (_activeChild != null) {
				_activeChild.Clear();
				_activeChild = null;
			}

		}
		
		public override BTResult Tick () {
			//Logging.HYLDDebug.LogError("prioritySelector的activechild:"+_activeChild);
			if (_activeChild == null) {
				return BTResult.Ended;
			}

			BTResult result = _activeChild.Tick();
			//Logging.HYLDDebug.LogError("prioritySelecto的result: "+result);
			if (result != BTResult.Running) {
				_activeChild.Clear();
				_activeChild = null;
			}
			return result;
		}
	}
}