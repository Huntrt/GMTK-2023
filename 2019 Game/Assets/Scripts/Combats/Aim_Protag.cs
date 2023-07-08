using UnityEngine;

public class Aim_Protag : Aim
{
    void Start()
	{
		Target = Protag.i.transform;
	}
}