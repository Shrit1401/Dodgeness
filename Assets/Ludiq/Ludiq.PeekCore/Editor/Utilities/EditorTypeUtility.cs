using System;
using System.Collections.Generic;
using Ludiq.PeekCore;
using UnityEditor;
using UnityEngine;

[assembly: InitializeAfterPlugins(typeof(EditorTypeUtility))]

namespace Ludiq.PeekCore
{
	public static class EditorTypeUtility
	{
		private static EditorBehaviorMode lastBehaviorMode = EditorSettings.defaultBehaviorMode;

		private static EditorBehaviorMode behaviorMode
		{
			get
			{
				if (UnityThread.allowsAPI)
				{
					lastBehaviorMode = EditorSettings.defaultBehaviorMode;
				}

				return lastBehaviorMode;
			}
		}

		public static IEnumerable<Type> commonTypes
		{
			get
			{
				yield return typeof(float);
				yield return typeof(int);
				yield return typeof(string);
				yield return typeof(bool);
				yield return new BehaviourTypeAssociation(typeof(Vector3), typeof(Vector2));
				yield return new BehaviourTypeAssociation(typeof(Vector3Int), typeof(Vector2Int));
				yield return typeof(GameObject);
				yield return typeof(List<>);
				yield return typeof(Dictionary<,>);
			}
		}

		private struct BehaviourTypeAssociation
		{
			public readonly Type typeFor3D;
			public readonly Type typeFor2D;

			public BehaviourTypeAssociation(Type typeFor3D, Type typeFor2D)
			{
				this.typeFor3D = typeFor3D;
				this.typeFor2D = typeFor2D;
			}

			public Type For(EditorBehaviorMode mode)
			{
				switch (mode)
				{
					case EditorBehaviorMode.Mode3D:
						return typeFor3D;
					case EditorBehaviorMode.Mode2D:
						return typeFor2D;
					default:
						throw new UnexpectedEnumValueException<EditorBehaviorMode>(mode);
				}
			}

			public static implicit operator Type(BehaviourTypeAssociation bta)
			{
				return bta.For(behaviorMode);
			}
		}
	}
}