using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyHealth : HealthController {
	public bool isShocked = false;
	public float shockedTime = 0.5F;
	public int eps = 1;
    public Image healthGui;
    public Image healthGuiMain;
    private float maxHealth;
    private EnemySonar enemySonar;
	private EPController epController;
	private Animator anim;
	private int hitTrigger;
	private int dieBool;
	private AudioSource audioSource;
    private bool dead;

	void Start()
	{
		enemySonar = GetComponent<EnemySonar>();
		epController = GameObject.FindGameObjectWithTag("GameController").
			GetComponent<EPController>();
        hitTrigger = Animator.StringToHash ("Hit");
		dieBool = Animator.StringToHash ("Die");
		anim = transform.GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
        maxHealth = health;
	}

    void Update()
    {
        if (dead)
            CloseHealthBar();
    }

    void UpdateView()
    {
        if (healthGui != null)
        {
            healthGui.fillAmount = health / maxHealth;
        }
     }

    void OnMouseEnter()
    {
        //healthGuiMain.renderer.enable = true;
        healthGui.enabled = true;
        healthGuiMain.enabled = true;
        InvokeRepeating("UpdateView", 0.0f, 0.3f);
        Vector3 pos;
        pos.x = Input.mousePosition.x;
        pos.y = Input.mousePosition.y + 20;
        pos.z = 0;
        healthGuiMain.rectTransform.position = pos;
        healthGui.rectTransform.position = pos;
    }

    void OnMouseExit()
    {
        CancelInvoke("UpdateView");
        CloseHealthBar();
    }

    void CloseHealthBar()
    {
        healthGui.enabled = false;
        healthGuiMain.enabled = false;
    }
    public override void Damaging ()
	{	
		anim.SetTrigger(hitTrigger);
		audioSource.Play ();
		isShocked = true;

		if(!enemySonar.playerDetected)		
			enemySonar.StopSearching();

		Invoke("ResetShocked",shockedTime);
	}

	public override void Dying ()
	{
        anim.speed = 1;
        anim.SetBool(dieBool,true);
		anim.SetTrigger(hitTrigger);
		audioSource.Play ();
        CancelInvoke("UpdateView");
        CloseHealthBar();
        dead = true;
        isShocked = true;
        Invoke ("DestroyMe",1);
	}

	void ResetShocked()
	{
		isShocked = false;
	}

	void DestroyMe()
	{
		Destroy(gameObject);
		epController.AddPoints (eps);
	}


}
