using DragonballPichu.Common.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace DragonballPichu.Common.Systems
{
    public class FormSetsSystem
    {
        public string set;
        string[] hardcoreSet;
        string[] mediumcoreSet;
        string[] softcoreSet;
        public FormSetsSystem(string name) 
        {
            set = name;
            hardcoreSet = FormSetsHardcore.get(name);
            mediumcoreSet = FormSetsMediumcore.get(name);
            softcoreSet = FormSetsSoftcore.get(name);
        }

        public FormSetsSystem()
        {
            set = "";
            hardcoreSet = null;
            mediumcoreSet = null;
            softcoreSet = null;
        }

        public void setSets(string name)
        {
            set = name;
            hardcoreSet = FormSetsHardcore.get(name);
            mediumcoreSet = FormSetsMediumcore.get(name);
            softcoreSet = FormSetsSoftcore.get(name);
        }

        public string[] get(string set)
        {
            switch (set)
            {
                case "hardcore":
                    return hardcoreSet;
                case "mediumcore":
                    return mediumcoreSet;
                default:
                    return softcoreSet;
            }
        }

        public string[] get()
        {
            string formLocking = ModContent.GetInstance<ServerConfig>().formLocking.ToLower();
            switch (formLocking)
            {
                case "mediumcore":
                    return get("mediumcore");
                case "hardcore":
                    return get("hardcore");
                default:
                    return get("softcore");
            }
        }

        public bool isFormInSet(string form)
        {
            if(get() == null)
            {
                return true;
            }
            return get().Contains(form);
        }
    }
}
