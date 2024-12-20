using System;
using System.Collections.Generic;
[System.Serializable]

//----Classe generale----//
public class UserData
{
    public string username;  // Deve corrispondere al campo "username"
    public string password;  // Deve corrispondere al campo "password"
    public GameData gameData;  // Deve corrispondere a "gameData"

    // Costruttore per inizializzare i dati
    public UserData()
    {
        gameData = new GameData();  // Inizializza la proprietà gameData
    }
}
[System.Serializable]
public class GameData
{
    public int coins;  // Deve corrispondere a "coins"
    public int level;  // Deve corrispondere a "level"
    public List<InventoryItem> inventory;  // Usa List invece di array per l'inventario

    // Costruttore per inizializzare i dati dell'inventario
    public GameData()
    {
        inventory = new List<InventoryItem>();  // Inizializza la lista degli oggetti dell'inventario
    }
}
[System.Serializable]
public class InventoryItem
{
    public string itemName;  // Nome dell'oggetto
    public int quantity;    // Quantità dell'oggetto
    public InventoryItem(string itemName, int quantity) // Costruttore per creare un oggetto inventario
    {
        this.itemName = itemName;
        this.quantity = quantity;
    }
    public InventoryItem() // Costruttore vuoto per iniziallare l'oggetto
    {
    }
}
//------------------------------------------------------------------------------//
//-----Classe per Aggiornare la moneta------//
[System.Serializable]
public class UserCoins
{
    public string username;  // Deve corrispondere al campo "username"
    public GameData_coins gameData;  // Deve corrispondere a "gameData"

    // Costruttore che inizializza gameData2
    public UserCoins()
    {
        gameData = new GameData_coins();  // Inizializza il campo gameData2
    }
}
[System.Serializable]
public class GameData_coins
{
    public int coins;  // Deve corrispondere a "coins"
}
//--------------------------------------//
//-----Classe per aggiornare sia moneta che inventario----//
[System.Serializable]
public class UserInventoryCoins
{
    public string username;  // Deve corrispondere al campo "username"
    public GameDataWithInventory gameData;  // Contiene sia la moneta che l'inventario

    // Costruttore per inizializzare gameData
    public UserInventoryCoins()
    {
        gameData = new GameDataWithInventory();  // Inizializza il campo gameData
    }
}

[System.Serializable]
public class GameDataWithInventory
{
    public int coins;  // Deve corrispondere a "coins"
    public List<InventoryItem> inventory;  // Lista degli oggetti nell'inventario

    // Costruttore per inizializzare la lista dell'inventario
    public GameDataWithInventory()
    {
        inventory = new List<InventoryItem>();  // Inizializza la lista dell'inventario
    }
}
//--------------------------------------//
//----Classe statica per memorizzare i dati globali----
public static class GlobalUserData
{
    // Dati di base
    public static string Username { get; set; }
    public static string Password { get; set; }
    public static int Coins { get; set; }
    public static int Level { get; set; }

    public static Boolean flag=false;
    public static List<InventoryItem> Inventory { get; set; } = new List<InventoryItem>();
}


