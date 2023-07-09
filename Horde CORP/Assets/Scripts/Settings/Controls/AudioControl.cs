using UnityEngine.UI;
using Game.Operator;
using UnityEngine;
using TMPro;

namespace Game.Settings 
{
public class AudioControl : MonoBehaviour
{
	public TextMeshProUGUI display;
	public Slider slider;
	enum VolumeType {master, sound, music} [SerializeField] VolumeType type;
	AudioManager manager;

	void OnEnable() 
	{
		//Get the audio manager
		manager = SessionOperator.i.audios;
		//Update display to show current manager volume
		UpdateDisplay();
		//Slide the volume when slider value changed
		slider.onValueChanged.AddListener(SlideVolume);
		//Update display when manager need to refresh
		manager.RefreshControl += UpdateDisplay;
	}

	public void SlideVolume(float value)
	{
		//Don't modify if there no manager
		if(manager == null) return;
		//If this control are master type
		if(type == VolumeType.master)
		{
			//Set master volume base on the slider value
			manager.SetMaster(Mathf.Round(slider.value * 100));
		}
		//If this control are sound type
		else if(type == VolumeType.sound)
		{
			//Set sound volume base on the slider value
			manager.SetSound(Mathf.Round(slider.value * 100));
		}
		//If this control are music type
		else if(type == VolumeType.music)
		{
			//Set music volume base on the slider value
			manager.SetMusic(Mathf.Round(slider.value * 100));
		}
		//Update both display and slider
		UpdateDisplay(); UpdateSlider();
	}

	void UpdateDisplay()
	{
		//Don't update if there no manager
		if(manager == null) return;
		//@ Set the display text as manager volume then update slider
		if(type == VolumeType.master) {display.text = "Master: "+manager.masterVolume;}
		if(type == VolumeType.sound) {display.text = "Sound: "+manager.soundVolume;}
		if(type == VolumeType.music) {display.text = "Music: "+manager.musicVolume;}
		UpdateSlider();
	}

	void UpdateSlider()
	{
		//Don't update if there no manager
		if(manager == null) return;
		//@ Update slider value to be manager volume
		if(type == VolumeType.master) {slider.value = (float)manager.masterVolume/100;}
		if(type == VolumeType.sound) {slider.value = (float)manager.soundVolume/100;}
		if(type == VolumeType.music) {slider.value = (float)manager.musicVolume/100;}
	}

	void OnDisable()
	{
		slider.onValueChanged.RemoveListener(SlideVolume);
		manager.RefreshControl -= UpdateDisplay;
	}
}
}