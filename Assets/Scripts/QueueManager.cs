using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueManager : MonoBehaviour
{
    //Putting this here
    public GameObject trapdoorPrefab;

    public List<QueuePerson> currentQueue;
    public Transform[] spawnPositions;
    public List<GameObject> trapdoorObjects;
    public List<bool> trapdoors;
    public float moveSpeed;
    public float swapSpeed;
    public float spinSpeed;
    private float timer;
    private int stagger;

    //These are used to swap
    private QueuePerson swap1;
    private QueuePerson swap2;

    //Used to delete and rejoin
    private List<QueuePerson> tempList;

    public enum actionStates
	{
        waiting,
        spawning,
        swapping,
        removal_delete,
        removal_trapdoor,
        removal_moveForward,
        repopulate,
        changingProperty
	}

    public actionStates currentState;

    public void LoadNewQueue(List<QueuePerson> queueToPopulate, List<bool> trapdoorList)
	{
        //Delete old queue
        for(int i = currentQueue.Count-1; i>=0; i--)
		{
            Destroy(currentQueue[i].gameObject);             
		}

        currentQueue = new List<QueuePerson>();
        //Create new list.
        foreach(QueuePerson person in queueToPopulate)
		{
            currentQueue.Add(person);
            person.moveSpeed = moveSpeed;
		}
        //Give them positions
        for (int i = 0; i < currentQueue.Count; i++)
        {
            currentQueue[i].currentTargetPos = spawnPositions[i].localPosition + (Vector3.up * currentQueue[i].yOffset);
        }
        timer = 0;
        stagger = 0;
        currentState = actionStates.spawning;
        //Also add trapdoors
        //Delete old trapdoors if they exist
        for(int i = 0; i < trapdoorObjects.Count; i++)
		{
            Destroy(trapdoorObjects[i].gameObject);
		}
        trapdoorObjects = new List<GameObject>();
        trapdoors = new List<bool>();
        for (int i = 0; i < currentQueue.Count; i++)
		{
            trapdoors.Add(trapdoorList[i]);
            //Spawn a trapdoor if necessary.
            if(trapdoorList[i] == true)
			{
                GameObject newDoor = Instantiate(trapdoorPrefab, spawnPositions[i], true);
                newDoor.transform.localPosition = new Vector3(0, 0.5f, 0);
                trapdoorObjects.Add(newDoor);
            }
		}
    }

	public void Update()
	{
		switch (currentState)
		{
            case actionStates.swapping:
                //Swapping two positions.
                //Wait for both of the guys to stop moving. Then move on
                if(swap1.move ==false && swap2.move == false)
				{
                    currentState = actionStates.waiting;
                    FinishAnAction();
				}
                break;
            case actionStates.spawning:
                //Bring people in staggered
                timer += Time.deltaTime;
                if (timer > 0.2f)
				{
                    if (stagger < currentQueue.Count)
                    {
                        currentQueue[stagger].move = true;
                        stagger += 1;
                        timer = 0;
					}
					else
					{
                        bool done = true;
                        foreach(QueuePerson person in currentQueue)
						{
                            if(person.move == true)
							{
                                done = false;
							}
						}
                        if(done == true)
						{
                            currentState = actionStates.waiting;
                        }
                    }
				}
                break;
            case actionStates.removal_delete:
                //Deleting some people to re-add them after.
                //For now they're popping away so just them really far to the left.
                //This function takes the remaining people and puts them in the correct spot;
                RecalculatePositionsAfterDelete();
                break;
            case actionStates.removal_trapdoor:
                //Everyone in the temp file needs to animated downwards. Once they're low enough then we just throw them to the left and its a normal removal
                timer += Time.deltaTime;
                foreach(QueuePerson dropper in tempList)
				{
                    Vector3 newTarget = dropper.transform.localPosition;
                    newTarget.y -= 1;
                    dropper.transform.localPosition = Vector3.Lerp(dropper.transform.localPosition, newTarget, 16*Time.deltaTime);
				}
                if(timer>1)
				{
                    foreach(QueuePerson person in tempList)
					{
                        person.transform.localPosition = new Vector3(-20, 0.5f, person.transform.localPosition.z);
                    }
                    RecalculatePositionsAfterDelete();
                    timer = 0;
                }
                break;
            case actionStates.removal_moveForward:
                //Moving leftovers forward
                //Wait for them all to be done.
                bool doneMoving = true;
                foreach(QueuePerson person in currentQueue)
				{
                    if(person.move == true)
					{
                        doneMoving = false;
					}
				}
                if (doneMoving == true)
                {
                    AddStragglersAfterDelete();
                }
                break;
            case actionStates.repopulate:
                //Bringing stragglers in.
                //This code is nearly identical to spawning new people in, but we can use the temporary list, they're the only ones left.
                timer += Time.deltaTime;
                if (timer > 0.2f)
                {
                    if (stagger < tempList.Count)
                    {
                        tempList[stagger].move = true;
                        stagger += 1;
                        timer = 0;
                    }
                    else
                    {
                        bool done = true;
                        foreach (QueuePerson person in currentQueue)
                        {
                            if (person.move == true)
                            {
                                done = false;
                            }
                        }
                        if (done == true)
                        {
                            currentState = actionStates.waiting;
                            FinishAnAction();
                        }
                    }
                }
                break;
            case actionStates.changingProperty:
                //This will be done whenever anyone is changing properties. Spin them while it changes, then stop spinning them
                //TODO later!

                break;

        }

	}

    //USE THIS FOR A CARD
    public void Swap(int index1, int index2)
	{
        if (AudioManager._audioManager != null)
        {
            AudioManager._audioManager.PlayRandomGrumble();
        }

        swap1 = currentQueue[index1];
        swap2 = currentQueue[index2];
        Vector3 temp = swap2.currentTargetPos;
        swap2.currentTargetPos = swap1.currentTargetPos;
        swap1.currentTargetPos = temp;
        swap1.move = true;
        swap2.move = true;
        swap1.moveSpeed = swapSpeed;
        swap2.moveSpeed = swapSpeed;
        currentQueue[index1] = swap2;
        currentQueue[index2] = swap1;
        currentState = actionStates.swapping;
    }

    public void Fizzle()
	{
        //Do nothing. Just resolve?
        UI_CardInventory._cardInventory.cardInventoryStates = UI_CardInventory.states.fizzleCard;

        // Audio
        if (AudioManager._audioManager != null)
        {
            AudioManager._audioManager.PlaySound(AudioManager._audioManager.fizzleCard);
        }

    }

    //USE THIS FOR A CARD
    public void Deletion(List<QueuePerson> removeThese)
	{
        if (AudioManager._audioManager != null)
        {
            AudioManager._audioManager.PlayRandomGrumble();
        }


        //Put them far to the left.
        foreach (QueuePerson person in removeThese)
		{
            currentQueue.Remove(person);
            person.transform.localPosition = new Vector3(-20, person.transform.localPosition.y, person.transform.localPosition.z);
		}
        //Save them to our templist. We'll need them later
        tempList = new List<QueuePerson>();
        foreach(QueuePerson p in removeThese)
		{
            tempList.Add(p);
		}
        currentState = actionStates.removal_delete;
    }

    public void TrapDoor(List<QueuePerson> removeThese)
    {

        if (AudioManager._audioManager != null)
        {
            AudioManager._audioManager.PlaySound(AudioManager._audioManager.wilheilm);
        }

        //Don't move them yet. Just remove them from the queue
        foreach (QueuePerson person in removeThese)
        {
            currentQueue.Remove(person);
        }
        //Save them to our templist. We'll need them later
        tempList = new List<QueuePerson>();
        foreach (QueuePerson p in removeThese)
        {
            tempList.Add(p);
        }
        timer = 0;
        currentState = actionStates.removal_trapdoor;
    }

    public void RecalculatePositionsAfterDelete()
	{
        //This goes through the current characters left and moves them all at the same time.
        for(int i = 0; i < currentQueue.Count; i++)
		{
            currentQueue[i].currentTargetPos = spawnPositions[i].localPosition + (Vector3.up * currentQueue[i].yOffset);
            currentQueue[i].move = true;
            currentQueue[i].moveSpeed = moveSpeed;
		}
        currentState = actionStates.removal_moveForward;
    }

    public void AddStragglersAfterDelete()
	{
        //Add the stragglers to the queue.
        foreach(QueuePerson straggler in tempList)
		{
            currentQueue.Add(straggler);
		}
        //Give the stragglers their new position. We can do this to everyone and it won't be a problem since people in the queue already are already in place.
        for (int i = 0; i < currentQueue.Count; i++)
        {
            currentQueue[i].currentTargetPos = spawnPositions[i].localPosition + (Vector3.up * currentQueue[i].yOffset);
            currentQueue[i].moveSpeed = moveSpeed;
        }
        //Now we will bring them in.
        stagger = 0;
        timer = 0;
        currentState = actionStates.repopulate;
    }

    public List<QueuePerson> largestGroup()
	{
        List<QueuePerson> returner = new List<QueuePerson>();

        QueuePerson bestStreakStarter = currentQueue[0];
        int currentStreak = 1;
        int bestStreak = 0;

        for(int i = 1; i< currentQueue.Count; i++)
		{
            if(currentQueue[i].shirt == currentQueue[i-1].shirt)
			{
                currentStreak += 1;
                if(currentStreak>bestStreak)
				{
                    bestStreakStarter = currentQueue[(i+1) - currentStreak];
                    bestStreak = currentStreak;
                }
			}
			else
			{
                currentStreak = 1;
			}
		}
        currentStreak = 1;
        for (int i = 1; i < currentQueue.Count; i++)
        {
            if (currentQueue[i].pants == currentQueue[i - 1].pants)
            {
                currentStreak += 1;
                if (currentStreak > bestStreak)
                {
                    bestStreakStarter = currentQueue[(i+1) - currentStreak];
                    bestStreak = currentStreak;
                }
            }
            else
            {
                currentStreak = 1;
            }
        }
        //NOW create an array of this group. Start with the streak starter and add anyone else.
        for(int i = 0; i < currentQueue.Count; i++)
		{
            if (returner.Count > 0)
            {
                if(bestStreak>0)
				{
                    bestStreak -= 1;
                    returner.Add(currentQueue[i]);
				}
			}
			else if (currentQueue[i] == bestStreakStarter)
			{
                returner.Add(currentQueue[i]);
                bestStreak -= 1;
			}
		}

        return returner;
	}


	public void FinishAnAction()
	{
        UI_CardInventory._cardInventory.DoneResolvingCard();
	}



    public int returnPlayerPosition()
	{
        int returner = 0;
        for(int i = 0; i < currentQueue.Count; i++)
		{
            if(currentQueue[i].isPlayer == true)
			{
                returner = i;
			}
		}
        return returner;
	}

    public bool checkForWin()
	{
        bool returner = false;
        if(currentQueue[0].isPlayer == true)
		{
            returner = true;
		}
        return returner;
	}

}
