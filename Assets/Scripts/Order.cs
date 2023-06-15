using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order {
    private int orderId;
    private string orderName;

    public Order(int orderId, string orderName) {
        this.orderId = orderId;
        this.orderName = orderName;
    }

    public void SetOrderId(int orderId) {
        this.orderId = orderId;
    }

    public void SetOrderName(string orderName) {
        this.orderName = orderName;
    }

    public int GetOrderId() {
        return this.orderId;
    }

    public string GetOrderName() {
        return this.orderName;
    }
}
