using System.Runtime.InteropServices;
using UnityEngine;
using Unity;
using UnityEngine.Experimental.AI;

public delegate bool GameCardCallBack(string method, GameObject player);

// returns the value if the card is burnt or get into the dropDeck
namespace AssemblyCSharp.Assets.Scripts
{
    public struct GameCard
    {
        public string name;
        public GameCardCallBack callback;
        public int cost;

        public GameCard(string n, int cost, GameCardCallBack cb)
        {
            name = n;
            this.cost = cost;
            callback = cb;
        }
    }

    static class BasicCards
    {
        static public bool backupCallBack(string method, GameObject player)
        {
            if (method == "use")
            {
                Debug.Log("Used backup");
                Ibuff buff = new basicBuff.BackupBuff();
                player.GetComponent<PlayerBuffManager>().addBuff(buff);
            }

            return true;
        }

        static public bool lazerCallBack(string method, GameObject player)
        {
            if (method == "use")
            {
                Debug.Log("Used lazer");
                Ibuff buff = new basicBuff.lazerBuff();
                player.GetComponent<PlayerBuffManager>().addBuff(buff);
            }

            return true;
        }

        static public bool portableBombCallBack(string method, GameObject player)
        {
            if (method == "use")
            {
                Debug.Log("Used portableBomb");
                GameObject pb = Resources.Load<GameObject>("Prefabs/PortableBomb");
                player.GetComponent<PlayerBehaviour>().instanceObj(pb);

            }

            return true;
        }

        static public bool speedupCallBack(string method, GameObject player)
        {
            if (method == "use")
            {
                Debug.Log("Used speedup");
                player.GetComponent<CardCore>().currentCycleTime /= 2;
            }

            return true;
        }
        public static GameCard backup = new GameCard("backup", 1, backupCallBack);
        public static GameCard lazer = new GameCard("lazer", 1, lazerCallBack);
        public static GameCard portableBomb = new GameCard("portable_bomb", 2, portableBombCallBack);
        public static GameCard speedUp = new GameCard("speedup", 0, speedupCallBack);
    }
}