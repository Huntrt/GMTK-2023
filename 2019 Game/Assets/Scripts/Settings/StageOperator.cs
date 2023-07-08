using UnityEngine.SceneManagement;
using UnityEngine;

namespace Game.Operator
{
public class StageOperator : MonoBehaviour
{
	[Header("Gameplay")]
	public bool paused;
	public GameObject pauseMenu;
	float pausedTimeScale;

	//Set this class to singleton
	public static StageOperator i {get{if(_i==null){_i = GameObject.FindObjectOfType<StageOperator>();}return _i;}} static StageOperator _i;
	
	public void Pausing()
	{
		//Toggle the pause menu
		pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
		//Toggle pause
		paused = !paused;
		//If being pause
		if(paused) 
		{
			//Save the time scale before pausing
			pausedTimeScale = Time.timeScale;
			//Set time scale to zero
			Time.timeScale = 0; 
		}
		//When no longer pause
		else
		{
			//Revert time scale back like before it pause
			Time.timeScale = pausedTimeScale;
		}
	}

	void Update()
	{
		//Pause when press escaped
		if(pauseMenu != null && Input.GetKeyDown(KeyCode.Escape)) Pausing();
	}

    public void LoadSceneIndex(int i) 
	{
		//Load scene at given index
		SceneManager.LoadScene(i, LoadSceneMode.Single);
		//Unpause if currently being pause
		if(paused) Pausing();
	}

    public void QuitGame() {Application.Quit();}
}
}