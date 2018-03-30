using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {
    // Editable Thrust and Rotate
    [SerializeField] float thrustAmount = 5f;
    [SerializeField] float rotateAmount = 5f;

    // Sound Assets
    [SerializeField] AudioClip thrustSound;
    [SerializeField] AudioClip winSound;
    [SerializeField] AudioClip loseSound;

    // Particle Assets
    [SerializeField] ParticleSystem thrustParticle;
    [SerializeField] ParticleSystem winParticle;
    [SerializeField] ParticleSystem loseParticle;

    // Possible player states
    enum State {Winning, Losing, Alive}
    State playerState = State.Alive;

    // Vars
    bool isThrusting;
    AudioSource audioSource;
    Rigidbody rocketBody;

	// Use this for initialization
	void Start () 
    {
        rocketBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        // Player can move while alive
        if (playerState == State.Alive) 
        {
            HandleRotate();
            HandleThrust();
        }
	}

    private void HandleRotate()
    {
        // Prevent rotation collisions
        // between player and physics rotations
        rocketBody.freezeRotation = true;

        // Rotate dependent of framerate
        float rotationThisFrame = rotateAmount * Time.deltaTime;

        // Directions
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
        rocketBody.freezeRotation = false;
    }

    private void HandleThrust()
    {
        // Add upward force to user
        // Play thrust sound
        // Play thrust particle
        if (Input.GetKey(KeyCode.Space))
        {
            rocketBody.AddRelativeForce(Vector3.up * thrustAmount);
            if (!audioSource.isPlaying) {
                audioSource.PlayOneShot(thrustSound);
            }
            thrustParticle.Play();
        }
        else
        {
            thrustParticle.Stop();
        }

    }

	private void OnCollisionEnter(Collision collision)
	{
        if (playerState != State.Alive) { return; }

        switch (collision.gameObject.tag) 
        {
            case "Friendly":
                break;
            case "Finish":
                HandleFinishLevel();
                break;
            default:
                HandleDeath();
                break;

        }
	}

    private void HandleFinishLevel () 
    {
        playerState = State.Winning;
        audioSource.Stop();
        audioSource.PlayOneShot(winSound);
        winParticle.Play();
        Invoke("LoadNextLevel", 1f);

    }

    private void HandleDeath ()
    {
        playerState = State.Losing;
        audioSource.Stop();
        audioSource.PlayOneShot(loseSound);
        loseParticle.Play();
        Invoke("LoadFirstLevel", 1f);
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextLevel () 
    {
        SceneManager.LoadScene(1);
    }


}
