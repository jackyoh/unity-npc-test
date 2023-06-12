using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueProvider {
    public static Queue counterQueue = new Queue();
    public static Queue[] chargeQueue = { new Queue(), new Queue() };
    public static Queue[] shakeQueue = { new Queue(), new Queue() };
    public static Queue resultQueue = new Queue();

    public static int[] playerArray;
    public static Queue playerQueue = new Queue();

    public static bool arriveCounterSite1 = true;
    public static bool isGetDrinks = false;
}
