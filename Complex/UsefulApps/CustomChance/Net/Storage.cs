using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace CustomChance
{
    [DataContract]
    public class Member
    {
        [DataMember]
        private string _name;
        public string Name { get => _name; set => _name = value; }

        [DataMember]
        private int _value;
        public int Value
        {
            get => _value;
            set
            {
                _value = value;
            }
        }
        [DataMember]
        private bool _iswinner;
        public bool IsWinner
        {
            get => _iswinner;
            set
            {
                _iswinner = value;
            }
        }
    }

    public class Storage
    {
        public int Selection { get; set; }

        public List<Member> Members { get; set; }

        public Storage()
        {
            Members = new List<Member>();
            Deserialize();
        }

        public void Serialize()
        {
            String path = "save.json";

            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Member>));
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, Members);
            }
        }

        public void Deserialize()
        {
            String path = "save.json";

            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Member>));
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    Members = (List<Member>)jsonFormatter.ReadObject(fs);
                }
            }
            catch (Exception exception)
            {
                if (!(exception is Win32Exception w32ex))
                {
                    w32ex = exception.InnerException as Win32Exception;
                }
                if (w32ex != null)
                {
                    int code = w32ex.ErrorCode;
                    Console.WriteLine("File is not exist or does not contain anything. " + code);
                }
            }
        }
    }
}
