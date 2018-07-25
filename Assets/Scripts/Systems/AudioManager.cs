using UnityEngine;

public class AudioManager : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private AudioSource audioSource;            // 해당 오디오 소스
	[SerializeField]
	private AudioClip[] audioClip;              // 오디오 클립의 모음

	public	bool		audioEnabled = true;	// 오디오 활성화 상태
	

	// 초기화
	private void Awake()
	{
		if (audioSource == null)
		{
			audioSource = GetComponent<AudioSource>();
		}
	}

	// 해당 인덱스의 소리 재생
	public bool StartAudio(int index)
	{
		if (audioEnabled)
		{
			// Index out of range exception
			if (index >= audioClip.Length)
			{
				return false;
			}

			AudioClip targetClip = audioClip[index];

			// Null reference exception
			if (targetClip == null)
			{
				return false;
			}

			audioSource.PlayOneShot(targetClip);

			return true;
		}

		return false;
	}

	// 전체 소리재생 종료
	public void StopAudio()
	{
		audioSource.Stop();
	}

	// 전체 소리재생 정지
	public void PauseAudio()
	{
		audioSource.Pause();
	}
	
	// 전체 소리재생 정지 해제
	public void UnPauseAudio()
	{
		audioSource.UnPause();
	}

	// 오디오 소스 반환
	public AudioSource GetAudioSource()
	{
		return audioSource;
	}

	// 오디오 소스 설정
	public void SetAudioSource(AudioSource target)
	{
		audioSource = target;

		return;
	}
}
