using System;
using System.Reflection;
using UnityEngine;
using Object = System.Object;

namespace AssemblyCSharp.Assets.Scripts
{
    public interface Ibuff
    {
        bool shouldCancel();
        void applyBuff(GameObject player);
        void cancelBuff(GameObject player);
        void onNewCycle(GameObject player);
        string getName();
        int getLayerNumber();
        void mergeBuff(GameObject player);
        string getDescription();
    }

    public static class basicBuff
    {
        public class lazerBuff : Ibuff
        {
            private float originDamage;
            private int layer = 1;
            private bool cancel = false;

            public string getDescription()
            {
                return "Your lazer has been enhanced to have" + Mathf.Pow(1.5f, layer) + "times attack";
            }

            public void applyBuff(GameObject player)
            {
                originDamage = player.GetComponent<PlayerBulletLSGenerator>().bullet
                    .GetComponent<PlayerBulletBehaviour>().damage;
                player.GetComponent<PlayerBulletLSGenerator>().bullet
                        .GetComponent<PlayerBulletBehaviour>().damage = originDamage * 1.5f;
            }

            public void cancelBuff(GameObject player)
            {
                player.GetComponent<PlayerBulletLSGenerator>().bullet
                    .GetComponent<PlayerBulletBehaviour>().damage = originDamage;
                cancel = true;
            }

            public bool shouldCancel()
            {
                return cancel;
            }

            public void onNewCycle(GameObject player)
            {
                cancelBuff(player);
            }

            public string getName()
            {
                return "LazerEnhanced";
            }

            public void mergeBuff(GameObject player)
            {
                ++layer;
                player.GetComponent<PlayerBulletLSGenerator>().bullet
                        .GetComponent<PlayerBulletBehaviour>().damage = originDamage * Mathf.Pow(1.5f, layer);
            }

            public int getLayerNumber()
            {
                return layer;
            }
        }

        public class BackupBuff : Ibuff
        {
            private int layer = 1;
            private int initCost;

            public string getDescription()
            {
                return "You have backed up" + layer + "energies to next turn";
            }

            public void applyBuff(GameObject player)
            {
                initCost = player.GetComponent<CardCore>().initCost;
                ++player.GetComponent<CardCore>().initCost;
                player.GetComponent<CardCore>().DrawOne();
            }

            public void cancelBuff(GameObject player)
            {
                player.GetComponent<CardCore>().initCost = initCost;
            }

            public bool shouldCancel()
            {
                return true;
            }

            public void onNewCycle(GameObject player)
            {
            }

            public string getName()
            {
                return "Backup";
            }

            public void mergeBuff(GameObject player)
            {
                ++layer;
                ++player.GetComponent<CardCore>().initCost;
                player.GetComponent<CardCore>().DrawOne();
            }

            public int getLayerNumber()
            {
                return layer;
            }
        }
    }
}