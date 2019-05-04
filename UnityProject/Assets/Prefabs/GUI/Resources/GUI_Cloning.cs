using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_Cloning : NetTab
{
	public CloningConsole CloningConsole;

	public int CurrentScreen = 1;

	public GameObject AllScreens;
	public GameObject MainScreen;
	public GameObject RecordListScreen;
	public GameObject SpecificRecordScreen;


	void Start()
	{
		if (Provider != null)
		{
			//Makes sure it connects with the dispenser properly
			CloningConsole = Provider.GetComponentInChildren<CloningConsole>();
			//Subscribe to change event from CloningConsole.cs
			CloningConsole.changeEvent += UpdateDisplay;
			UpdateDisplay();
		}
	}


	public void UpdateDisplay()
	{
		if(CurrentScreen == 0)
		{
			AllScreens.SetActive(false);
		}
		if (CurrentScreen == 1)
		{
			RecordListScreen.SetActive(false);
			SpecificRecordScreen.SetActive(false);
			MainScreen.SetActive(true);
			AllScreens.SetActive(true); //DEBUG: for power switch in the black screen
		}
		if (CurrentScreen == 2)
		{
			MainScreen.SetActive(false);
			RecordListScreen.SetActive(true);
		}
		if (CurrentScreen == 3)
		{
			MainScreen.SetActive(false);
			SpecificRecordScreen.SetActive(true);
		}
	}

	public void OnDestroy()
	{
		//Unsubscribe container update event
		CloningConsole.changeEvent -= UpdateDisplay;
	}

	public void Autoprocess()
	{
		Debug.Log("ARAN: autoprocess");
		UpdateDisplay();
	}

	public void StartScan()
	{
		Debug.Log("ARAN: scanning");
		UpdateDisplay();
	}

	public void LockScanner()
	{
		Debug.Log("ARAN: lock toggled");
		UpdateDisplay();
	}

	public void ViewRecords()
	{
		CurrentScreen = 2;
		UpdateDisplay();
	}

	public void ViewSpecificRecord()
	{
		CurrentScreen = 3;
		UpdateDisplay();
	}

	public void Back()
	{
		CurrentScreen = 1;
		UpdateDisplay();
	}

	public void DeleteRecord()
	{
		UpdateDisplay();
	}

	public void Clone()
	{
		UpdateDisplay();
	}

	public void DEBUGTURNOFF()
	{
		CurrentScreen = 0;
		UpdateDisplay();
	}



}
