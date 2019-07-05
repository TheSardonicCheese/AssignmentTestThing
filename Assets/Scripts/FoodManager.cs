using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    //food = enemies
    //prefab that we made that is our various foods
    public GameObject defaultFoodPrefab;

    //list to pick from
    public List<GameObject> buffetItems;

    //list of current items
    public List<GameObject> plateItems;

    //number of items to randomly create
    public int numOfItems;

    void Start()
    {
        //generate our buffet items based on how many we set using numOfItems
        for(int i = 0; i < numOfItems; i++)
        {
            //add item to list
            buffetItems.Add(defaultFoodPrefab);
            //set up a temporary enum
            //cast to enum type using (typeof)

        }
        //select something to put on our plate
        //taking our list buffetItems and getting a random item using random.range
        //random range takes a min and max, 0 = start
        //buffetitems.count = last index
        //random.range is max exclusive so it wont go past max index
        AddToPlate(buffetItems[Random.Range(0, buffetItems.Count)]);
        AddToPlate(buffetItems[Random.Range(0, buffetItems.Count)]);
        AddToPlate(buffetItems[Random.Range(0, buffetItems.Count)]);
        AddToPlate(buffetItems[Random.Range(0, buffetItems.Count)]);
        AddToPlate(buffetItems[Random.Range(0, buffetItems.Count)]);


        //moved here so it only runs once even if we run addtoplate multiple times
        StartCoroutine(Eating());

    }

    void AddToPlate(GameObject foodToAdd)
    {
        //step 1 spawn the item
        //can change "transform" top a set position, like a spawn point
        GameObject spawnedFood = Instantiate(foodToAdd, transform);

        //step 2 randomize the knd of food it is
        //cast to enum type using (enumtype

        spawnedFood.GetComponent<FoodItem>().myType = (FoodItem.FoodTypes)Random.Range(0, 8);
        //set the name of the food item to the type that it is
        spawnedFood.name = spawnedFood.GetComponent<FoodItem>().myType.ToString();


        //step 3 add to plate
        plateItems.Add(spawnedFood);
        //steo 4 eat the food

    }

    void Eat (GameObject foodToEat) //remove from plate
    {
        //remove the given item from the list
        plateItems.Remove(foodToEat);
        //removes it from the level
        Destroy(foodToEat);
    }
    IEnumerator Eating()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("nom");
        Debug.Log(plateItems[0]);
        //remove item from plate
        Eat(plateItems[0]);
        //add new item to plate
        AddToPlate(buffetItems[Random.Range(0, buffetItems.Count)]);
        //so it loops
        StartCoroutine(Eating());
    }
}
