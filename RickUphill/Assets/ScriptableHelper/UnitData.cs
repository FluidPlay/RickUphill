using UnityEngine;

public class UnitData : ScriptableObject {
	
	public ActionTypes[] Actions;

	[System.Serializable]
	public struct ActionTypes {
		public int Idx;
		public string ActionName;
	}

	public UnitData Clone() {
		var NewUnitData = CreateInstance<UnitData>();

		NewUnitData.Actions = (ActionTypes[])Actions.Clone();

		return NewUnitData;
	}

}
