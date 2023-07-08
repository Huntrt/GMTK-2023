using System.Collections.Generic;
using Game.Operator;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace Game.Settings 
{
public class DisplayControl : MonoBehaviour
{
	//! RESOLUTION AND FULLSCREEN ARE ALREADY SAVED ACROSS GAME SESSION

	[SerializeField] FullscreenControl fullscreenControl; [System.Serializable] class FullscreenControl
	{
		public Toggle toggler;

		//Set full screen toggle status base on full screen state
		public void SetupToggler() {toggler.SetIsOnWithoutNotify(Screen.fullScreen);}
		
		public void SetFullscreen(bool toggleOn) 
		{
			//Change it toggle to given vlaue
			toggler.isOn = toggleOn;
			//Save the fullscreen toggle state to display manager
			SessionOperator.i.displays.fullScreen = toggleOn;
			//Set full screen base on it toggle state
			Screen.fullScreen = toggler.isOn;
		}
	}

	[SerializeField] VSyncControl vSyncControl; [System.Serializable] class VSyncControl
	{
		public Toggle toggler;

		//Set vsync toggle status base on is game current vsync are 1 (on) or 0 (off) 
		public void SetupToggler() {toggler.SetIsOnWithoutNotify(QualitySettings.vSyncCount == 1);}

		public void SetVSync(bool toggleOn)
		{
			//Change it toggle to given vlaue
			toggler.isOn = toggleOn;
			//Save the vsync toggle state to display manager
			SessionOperator.i.displays.vSync = toggleOn;
			//Set vsync base on toggler 
			QualitySettings.vSyncCount = (toggler.isOn) ? 1 : 0;
		}
	}

	[SerializeField] ResolutionControl resolutionControl; [System.Serializable] class ResolutionControl
	{
		public TMP_Dropdown dropdown;

		public void SetupDropdown()
		{
			//? Disable resolution setup when running WebGL since it cause problem
			if(Application.platform == RuntimePlatform.WebGLPlayer) return;
			//Create an new list of string for available resolution
			List<string> availRes = new List<string>();
			//The index of screen size currently use
			int use = 0;
			//Get the resolution currently use
			Resolution curRes = Screen.currentResolution;
			//Go through all the available resolution
			for (int r = 0; r < Screen.resolutions.Length; r++)
			{
				//Get this resolution
				Resolution res = Res(r);
				//Adding this available resolution as string
				availRes.Add(res.width + " x " + res.height);
				/// Use this index if this resolution match with RESOLUTION currently use in full screen
				if(Screen.fullScreen && res.width == curRes.width && res.height == curRes.height) use = r;
				/// Use this index if this resolution match with WINDOW SIZE currently use in windowed
				if(!Screen.fullScreen && res.width == Screen.width && res.height == Screen.height) use = r;
			}
			//Add all the availables resolution option to drop down
			dropdown.AddOptions(availRes);
			//Get the resolution option current choose in manager
			int choosed = SessionOperator.i.displays.resolutionChoosed;
			//Use resolution currently use when haven't choose any option
			dropdown.value = (choosed == -1) ? use : choosed;
			//Clear after use list
			availRes.Clear();
		}

		public void ChangeResolution(int c)
		{
			//? Disable resolution choosing when running WebGL since it cause problem
			if(Application.platform == RuntimePlatform.WebGLPlayer) return;
			//Get the highest resolution available if havent choose an any
			if(c == -1) c = Screen.resolutions.Length-1;
			//Set it dropdown value to given value
			dropdown.value = c;
			//Save the resolution option has choosed
			SessionOperator.i.displays.resolutionChoosed = c;
			//Set resolution in available base on given change 
			Screen.SetResolution(Res(c).width, Res(c).height, Screen.fullScreen, Res(c).refreshRate);
		}

		//Return available resolution at given index 
		Resolution Res(int i) {return Screen.resolutions[i];}
	}

	[SerializeField] FrameCapControl frameCapControl; [System.Serializable] class FrameCapControl
	{
		public TMP_Dropdown dropdown;
		[SerializeField] List<string> frameList;
		[SerializeField] TextMeshProUGUI fpsCounter;
		public float fpsCounterRefresh;

		public void SetupDropdown()
		{
			//Add all frame list to drop down option
			dropdown.AddOptions(frameList);
		}

		public void ChangeFrameCap(int c)
		{
			//Set it dropdown value to given value
			dropdown.value = c;
			//Save the frame cap option has choosed
			SessionOperator.i.displays.frameCapChoosed = c;
			//Choose the given value from list, if choose 0 then use unlimited instead
			int frameChoose = (c > 0) ? int.Parse(frameList[c]) : -1;
			//Apply frame rate choose to game
			Application.targetFrameRate = frameChoose;
		}
	}

	DisplayManager manager;

	void OnEnable()
	{
		//Get the display manager
		manager = SessionOperator.i.displays;
		//Refresh all control whenever manager need to
		manager.RefreshControl += RefreshAllControl;
	}

	void Start()
	{
		//@ Setup display control for new scene
		fullscreenControl.SetupToggler();
		vSyncControl.SetupToggler();
		resolutionControl.SetupDropdown();
		frameCapControl.SetupDropdown();
		//Refresh all control after setup them
		RefreshAllControl();
	}

	void RefreshAllControl()
	{
		//@ Set all control GUI to be manager's value
		fullscreenControl.SetFullscreen(manager.fullScreen);
		vSyncControl.SetVSync(manager.vSync);
		resolutionControl.ChangeResolution(manager.resolutionChoosed);
		frameCapControl.ChangeFrameCap(manager.frameCapChoosed);
	}
	
	public void ToggleFullscreen() {fullscreenControl.SetFullscreen(fullscreenControl.toggler.isOn);}
	public void ToggleVSync() {vSyncControl.SetVSync(vSyncControl.toggler.isOn);}
	public void DropdownResolution() {resolutionControl.ChangeResolution(resolutionControl.dropdown.value);}
	public void DropdownFrameRate() {frameCapControl.ChangeFrameCap(frameCapControl.dropdown.value);}

	void OnDisable()
	{
		manager.RefreshControl -= RefreshAllControl;
	}
}
}