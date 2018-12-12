using System.Collections.Generic;
using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts
{
    public class PlayerBuffManager : MonoBehaviour
    {
        public List<Ibuff> buffList = new List<Ibuff>();
        private void Update()
        {
        }

        public void onNewCycle()
        {
            List<Ibuff> removeList = new List<Ibuff>();
            foreach (var buff in buffList)
            {
                buff.onNewCycle(gameObject);
                if(buff.shouldCancel()) removeList.Add(buff);
                
            }
            foreach (var buff in removeList)
            {
                buff.cancelBuff(gameObject);
                buffList.Remove(buff);
            }
        }

        public void addBuff(Ibuff buff)
        {
            string bn = buff.getName();
            foreach (var b in buffList)
            {
                if (bn == b.getName())
                {
                    b.mergeBuff(gameObject);
                }

                return;
            }
            buffList.Add(buff); 
            buff.applyBuff(gameObject);
        }
        private void Start()
        {
            
        }
    }
}