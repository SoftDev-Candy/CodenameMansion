
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerDeathRespawn : MonoBehaviour
{
    public Transform CurrentCheckPoint;
    public bool isDying;
    public Animator playerAnimator; // Assign Player Animator in Inspector
    public Image fadeScreen; // Assign a UI Image (black panel) for fading
    public float fadeDuration = 1.5f; // Time to fade in/out
    public float playerMoveAfterDeathDurration = 0.5f;
    private PlayerCommands playerCommand;

    private void Start()
    {
        playerCommand = GetComponent<PlayerCommands>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (!isDying)
            {
                GameObject enemy = other.gameObject;
                if(!enemy.GetComponent<EnemyCombat>().isDead)
                {
                    AudioManager.instance.PlayOneShot(FMODEvents.instance.playerDeath, transform.position);
                    AudioManager.instance.PlayOneShot(FMODEvents.instance.monsterKillingPlayer, transform.position);
                    StartCoroutine(HandleDeath(enemy));
                }
            }
        }
    }
    IEnumerator HandleDeath(GameObject enemy)
    {
        isDying = true;
        playerCommand.ClearCurrentTasks();
        playerCommand.isDead = true;
        Debug.Log("Player Died");
        //  Play Death Animation
        //playerAnimator.SetTrigger("Die");
        //yield return new WaitForSeconds(playerAnimator.GetCurrentAnimatorStateInfo(0).length);

        // Fade to Black
        yield return StartCoroutine(FadeToBlack());

        // Reset Enemy Position & Health
        enemy.GetComponent<Enemy_Movement>().RespawnPos();
        
        enemy.GetComponent<EnemyCombat>().EnemyRespawnPlayerDeath();

        // Respawn Player
        transform.position = CurrentCheckPoint.position;

        // Fade Back to Game
        yield return StartCoroutine(FadeFromBlack());
    }

    IEnumerator FadeToBlack()
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            fadeScreen.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, elapsedTime / fadeDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        fadeScreen.color = Color.black;
    }

    IEnumerator FadeFromBlack()
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            if(elapsedTime > playerMoveAfterDeathDurration)
            {
                playerCommand.isDead = false;
            }
            fadeScreen.color = new Color(0, 0, 0, Mathf.Lerp(1, 0, elapsedTime / fadeDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        fadeScreen.color = new Color(0, 0, 0, 0);
        isDying = false;
        
    }
}