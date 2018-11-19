using UnityEngine;
using System.Collections;

public class AccessScriptableData : MonoBehaviour {

	public UnitData unitData, unitDataInMemory;

	void Start () {
		// Acessa dado em disco, se associado o unitData a uma instância do projeto
		Debug.Log (unitData.Actions[0].ActionName);

		// Criar instancia em memória; O 1o. não funciona para scriptable Object, usar formato abaixo:
		//unitData = new UnitData();
		unitDataInMemory = ScriptableObject.CreateInstance<UnitData>();

		// Copiando dados em disco para uma instância temporária em memória
		unitDataInMemory = unitData.Clone();
	}

}
