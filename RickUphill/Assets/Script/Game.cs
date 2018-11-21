using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
	public static Game Data;
	public int Lives = 3;
	public int Score = 0;
	public Text LivesUIText, ScoreUIText;
	public GameObject GameOverText;
	public bool GameOver;
	public float WaitTimeForReload = 3f;
	public event Action OnGameOver;
	public BoxCollider2D GroundRick;
	public RickController Rick;
	public float RespawnTime = 2f;
	
	// Use this for initialization
	void Awake()
	{
		if (Data == null)
			Data = this;
	}

	public void AddScore()
	{
		Score++;
		ScoreUIText.text = Score + "";
	}

	public void Death()
	{
		Lives--;
		Debug.Log("Death");
		LivesUIText.text = Lives + "";
		if (Lives <= 0 && !GameOver) {
			StartCoroutine(GameOverActions());
		}
	}

	IEnumerator GameOverActions()
	{
		GameOverText.SetActive(true);
		GameOver = true;
		if (OnGameOver != null)
			OnGameOver(); // Disparar todos os métodos associados a OnGameOver
		
		// Pausa antes de recarregar a cena / jogo
		yield return new WaitForSeconds(WaitTimeForReload);
		var asyncLoad = SceneManager.LoadSceneAsync(0);
		while (!asyncLoad.isDone) {
			yield return null;
		}
		//Application.LoadLevel(0);
	}
}
