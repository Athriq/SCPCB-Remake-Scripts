using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {

	public Types types;
}

public enum Types
{
	Object,
	Paper,
	Button
};