using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections.ObjectModel;

namespace CustomChance
{
    class CommonLogic
    {
        static CommonLogic _instance;
        
        private CommonLogic()
        {
            Storage = new Storage();
        }

        public static CommonLogic GetInstance()
        {
            if (_instance != null)
                return _instance;
            return _instance = new CommonLogic();
        }

        public Storage Storage { get; set; }

        public void TrySerialize()
        {
            Storage.Serialize();
        }

        public void TryDeserialize()
        {
            Storage.Deserialize();
        }

        public void StartRandom(List<Member> members)
        {
            RandomChoise(members);
            int max = 0;
            int index = 0;
            for (int i = 0; i < members.Count; i++)
            {
                if (max < members[i].Value)
                {
                    index = i;
                    max = members[i].Value;
                }
            }
            members[index].IsWinner = true;
        }

        private void RandomChoise(List<Member> variants)
        {
            //preparation
            if (variants.Count == 0)
                return;
            foreach (var item in variants)
            {
                item.Value = 0;
                item.IsWinner = false;
            }

            //randomizing
            Random rnd = new Random();
            for (int i = 0; i < 100; i++)
            {
                int time = 0;

                while (time == 0)
                    time = DateTime.Now.Millisecond;

                int index = rnd.Next(1, variants.Count * time);
                variants[index / time].Value++;
            }
        }

        public bool AddMember(List<Member> members, String name)
        {
            if (members == null)
                members = new List<Member>();

            if (name.Equals(String.Empty))
                return false;

            foreach (var item in members)
            {
                if (item.Name.Equals(name))
                    return false;
            }
            members.Add(new Member()
            {
                Name = name,
                Value = 0,
                IsWinner = false
            });
            return true;
        }

        public void DeleteMember(List<Member> members, string selected)
        {
            foreach (var item in members)
            {
                if (item.Name.Equals(selected))
                {
                    members.Remove(item);
                    return;
                }
            }
        }

        public void PrintMembersStat()
        {
            foreach (var _ in Storage.Members)
            {
                Console.WriteLine(_.Name + " " + _.Value + " " + _.IsWinner);
            }
        }
    }
}
