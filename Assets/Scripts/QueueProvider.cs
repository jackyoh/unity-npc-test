using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueProvider {
    public static Queue counterQueue = new Queue();
    public static List<Queue> chargeQueue = new List<Queue>();
    public static List<Queue> shakeQueue = new List<Queue>();
    public static Queue resultQueue = new Queue();

    public static int[] playerArray;
    public static Queue playerQueue = new Queue();

    public static bool arriveCounterSite1 = true;
    public static bool isGetDrinks = false;
}
