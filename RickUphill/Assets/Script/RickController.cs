using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class RickController : MonoBehaviour
{
	public PlayerStats Stats;
	
	private bool _groundCheck;
	private Animator _anim;
	
	private Rigidbody2D _rb;

	private bool _isJumping, _isDead;
	private float _jumpStartTime;
	private Collider2D LastCheckedRock;
	private bool _rockWasJumped;
	private int _rockLayer, _groundRickLayer;
	private Vector2 _startPos;

	private AudioSource _audioSource;

	public enum RickStates { walking, jumping, dead }

	// Use this for initialization
	void Awake ()
	{
		_anim = GetComponent<Animator>();
		_rb = GetComponent<Rigidbody2D>();
		_audioSource = GetComponent<AudioSource>();
		_rockLayer = LayerMask.NameToLayer("Rock");	// int 
		_groundRickLayer = LayerMask.NameToLayer("GroundRick");
		_startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (_isDead)
			return;
		if (Input.GetButton("Jump") || Input.touches.Length > 0)
			if (!_isJumping)
			{
				_jumpStartTime = Time.time;
				_isJumping = true;
				_groundCheck = true;
				DoJump();				
			} 
			else
			{
				var jumpEndTime = _jumpStartTime + Stats.JumpAddTime;
				if (Time.time <= jumpEndTime)
					DoJump();
			}
		if (_isJumping)
			CheckRockUnderneath();
	}

	private void CheckRockUnderneath()
	{
		var hit = Physics2D.Raycast(transform.position, Vector2.down, Stats.RockCheckLength,
			1 << _rockLayer); //1 << 11
		Debug.DrawRay(transform.position, Vector2.down * Stats.RockCheckLength, Color.green);

		if (hit == null)
			return;
		//Debug.LogError("Collider hit: "+hit.collider.gameObject.name);
		var colliderHit = hit.collider;
		if ( !colliderHit || colliderHit == LastCheckedRock)
			return;

		_rockWasJumped = true;
		LastCheckedRock = colliderHit;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.layer == _groundRickLayer)
		{
			_isJumping = false;
			if (_groundCheck)
				_groundCheck = false;
			if (_rockWasJumped)
			{
				Game.Data.AddScore();
				_rockWasJumped = false;
			}
			return;
		}
		Death();
	}	

	void DoJump()
	{
		_rb.AddForce(Vector2.up * Stats.JumpForce);
		PlaySoundFX(Stats.SoundFX.Jump);		
	}

	public void Death()
	{
		_anim.SetTrigger("isdead");
		_isDead = true;
		PlaySoundFX(Stats.SoundFX.Death);
		Game.Data.Death();
		if (Game.Data.Lives > 0)
			StartCoroutine(Respawn());
	}

	private void PlaySoundFX(AudioClip clip)
	{
		_audioSource.clip = clip;
		_audioSource.Play();
	}


	private IEnumerator Respawn()
	{
		yield return new WaitForSeconds(Game.Data.RespawnTime);
		transform.position = _startPos;
		transform.rotation = Quaternion.identity;
		_rb.velocity = Vector2.zero;
		_rb.angularVelocity = 0f;
		_anim.SetTrigger("revive");
		_isDead = false;		
	}	
}
