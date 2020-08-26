using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
	GameObject powerUp;
	void OnEnable()
	{
		int i = Random.Range(0, 10);
		if (i > 7) {
			powerUp = TilePooler.Instance.EnableObject(transform, TilePooler.Instance.activePowerUps, TilePooler.Instance.disabledPowerUps);
		}
	}
	void OnDisable()
	{
		if (TilePooler.Instance.activePowerUps.Contains(powerUp))
		{
			TilePooler.Instance.DisableObject(powerUp, TilePooler.Instance.activePowerUps, TilePooler.Instance.disabledPowerUps);
		}
	}
}
