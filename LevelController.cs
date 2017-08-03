using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour
{

    ArrayList spawntimers = new ArrayList();                                                                                               								
    ArrayList tutorialtimers = new ArrayList();
    private string LevelName;
    private int currentLevel;
    public MidWaveText WaveScript;
    private bool tutorial;
    private int diff; 							//Difficulty Level
    public VoiceOverController VO;

    public int getCurrentLevel()
    {
        return currentLevel;
    }

    public string getLevelName()
    {
        return LevelName;
    }

    #region Refactor
    [System.Serializable]
    public struct LevelSpawners
    {
        public float SpawnDelay;
        public SpawnerScript script;
    }

    [System.Serializable]
    public struct Level
    {
        public string Name;
        public float DurationSeconds;
        public LevelSpawners[] Spawners;
    }

    public Level[] SpawnerList;
    public Level[] TutorialList;

    IEnumerator SpawnerSwitcher()
    {
        if (diff == 1) //Tutorial Level
        {
            StartCoroutine(VO.TutorialSetList());
            foreach (Level spawn in TutorialList)
            {
                Debug.LogFormat("{0} succesfully activated", spawn.Name);
                LevelName = spawn.Name;
                Debug.Log("Waiting 6 seconds...");
                foreach (LevelSpawners Spawner in spawn.Spawners)
                {
                    yield return new WaitForSeconds(6f);
                    Spawner.script.setActive(Spawner.SpawnDelay);
                }
                // Wait
                yield return new WaitForSeconds(spawn.DurationSeconds);
                // End level
                foreach (LevelSpawners Spawner in spawn.Spawners)
                {
                    Spawner.script.setActive();
                }
                Debug.LogFormat("{0} succesfully deactivated", spawn.Name);
            }
        }
        else // Difficulty
        {
            foreach (Level spawn in SpawnerList)
            {
                Debug.LogFormat("{0} succesfully activated", spawn.Name);
                LevelName = spawn.Name;
                Debug.Log("Waiting 6 seconds...");
                // Start level
                foreach (LevelSpawners Spawner in spawn.Spawners)
                {
                    yield return new WaitForSeconds(6f);
                    Spawner.script.setActive(Spawner.SpawnDelay);
                }
                // Wait
                yield return new WaitForSeconds(spawn.DurationSeconds);
                // End level
                foreach (LevelSpawners Spawner in spawn.Spawners)
                {
                    Spawner.script.setActive();
                }
                currentLevel++;
                //print(currentLevel);
                yield return new WaitForSeconds(2f);
                WaveScript.waveComplete();
                Debug.LogFormat("{0} succesfully deactivated", spawn.Name);
            }
        }
    }
    #endregion



    void Start()
    {
        diff = DifficultyLevel.getDifficultyLevel(); //Check what difficulty was picked in the menu
        StartCoroutine(SpawnerSwitcher());
    }
}