using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManagerV2 : MonoBehaviour
{

    //just like an enemy list, this is where all our prefabs will go
    public List<GameObject> buffet;

    //assume that i want to battle a team of enemies, one at a time, until they are defeated
    //we will only fight some of the many types so we need a new list
    public List<GameObject> plate;

    //food to eat = enemy to fight
    public GameObject foodToEat;

    //how many items we want on our plate at once or how many enemies on the team
    public int numOfFoodItemsOnPlate;


    // Start is called before the first frame update
    void Start()
    {
        //select the enemies to fight
        for (int i = 0; i < numOfFoodItemsOnPlate; i++)
        {

            //add a random food item from buffet to plate, they are already different types!
            GameObject spawnedFood = Instantiate(buffet[Random.Range(0, buffet.Count)], transform);
            plate.Add(spawnedFood);
        }
        //actually start setting which food to eat
        SetNewFoodToEat();
    }

    void SetNewFoodToEat ()
    {
        //randomly get something from the plate and assign it to foodToEat
        foodToEat = plate[Random.Range(0, plate.Count)];
        StartCoroutine(Eating());
        Debug.Log(" Let's eat some " + foodToEat );
    }

    void EatFood()
    {
        //remove current food
        plate.Remove(foodToEat);
        Destroy(foodToEat);
        //check is we have eaten everything, if not eat something else
        if (plate.Count == 0)
        {
            //we win
            Debug.Log(" you ate everything!");
        }

        else
        {
            //something else
            SetNewFoodToEat();
        }
        //if we have we win!
    }
    IEnumerator Eating()
    {
        yield return new WaitForSeconds(2);
        EatFood();
    }
}
