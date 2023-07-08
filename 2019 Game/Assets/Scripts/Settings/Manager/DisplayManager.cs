using UnityEngine;
using System;

namespace Game.Settings 
{
public class DisplayManager : MonoBehaviour
{
    public bool fullScreen, vSync;
	public int resolutionChoosed, frameCapChoosed;
	public Action RefreshControl;

	void Awake()
	{
		//Haven't choose any resolution
		resolutionChoosed = -1;
	}
}
}