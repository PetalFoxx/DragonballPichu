using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonballPichu.Common.Systems
{
    internal class FormStats
    {
        public Stat MultKi = new Stat("MultKi", 1);
        public Stat DivideDrain = new Stat("DivideDrain", 1);
        public Stat MultDamage = new Stat("MultDamage", 1);
        public Stat MultSpeed = new Stat("MultSpeed", 1);
        public Stat MultDefense = new Stat("MultDefense", 1);
        public Stat Special = new Stat("Special", 1);

        float experience = 0;
        int level = 0;
        int points = 0;
        int levelInAll = 0;

        public FormStats(string form)
        {

        }

        public float expNeededToAdvanceLevel()
        {
            float expNeeded = ((float)Math.Pow(level+1, 1.5))*100;
            return expNeeded;
        }

        public void levelUp()
        {

            float expNeeded = expNeededToAdvanceLevel();
            while (experience > expNeeded)
            {
                experience -= expNeeded;
                increaseLevel();
                expNeeded = expNeededToAdvanceLevel();
            }
        }

        public void gainExperience(float experience)
        {
            this.experience += experience;
            levelUp();
        }

        public void increaseLevel()
        {
            level += 1;
            points++;
            if(level % 5 == 0)
            {
                levelInAll++;
                increaseAll(1);
            }
        }
        public void setLevel(int level)
        {
            this.level = level;
            this.points = level;
            levelInAll = (level / 5);
            increaseAll(levelInAll);
        }

        public Stat getStat(string statName)
        {
            switch(statName)
            {
                case "MultKi":
                    return MultKi;
                case "DivideDrain":
                    return DivideDrain;
                case "MultDamage":
                    return MultDamage;
                case "MultSpeed":
                    return MultSpeed;
                case "MultDefense":
                    return MultDefense;
                case "Special":
                    return Special;
                default:
                    return null;
            }
        }

        public float getExperience() 
        {
            return experience;
        }

        public int getLevel()
        {
            return level;
        }

        public int getPoints()
        {
            return points;
        }

        public Boolean usePoint()
        {
            if(points >= 1)
            {
                points--;
                return true;
            }
            return false;
        }

        public void respec()
        {
            points = level;
            MultKi.setValue(1f);
            DivideDrain.setValue(1f);
            MultDamage.setValue(1f);
            MultSpeed.setValue(1f);
            MultDefense.setValue(1f);
            Special.setValue(1f);

            increaseAll(levelInAll);
        }

        public void increaseAll(int num)
        {
            MultKi.increaseValue(0.1f * num);
            DivideDrain.increaseValue(0.1f * num);
            MultDamage.increaseValue(0.1f* num);
            MultSpeed.increaseValue(0.1f* num);
            MultDefense.increaseValue(0.1f* num);
            Special.increaseValue(0.1f * num);
        }
    }
}
