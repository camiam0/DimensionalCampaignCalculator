using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimensionalCampaignCalculator
{

    /* Useful information:
     * pEffort = physical effort
     * mEffort = mental effort
     * tEffort = tactical effort
     */

    internal class Calculator
    {

        // creates a random number generator for dice rolls
        private static readonly Random rng = new Random();

        public static int RollDice(int numberOfDice, int dieSize)
        {
            
            if (numberOfDice < 0) { return 0; }

            if (dieSize < 1) { return 0; }

            int total = 0;

            for (int i = 0; i < numberOfDice; i++)
            {
                
                total += rng.Next(1, dieSize + 1);
            
            }

            return total;

        } // end of RollDice

        public static int WeaponToHit(int roll, int statBonus, int pEffort, int edge, int training, int enchant, int buffs) 
        {
            
            return roll + statBonus * (pEffort + edge + training + 1) + enchant + buffs;
        
        } // end of WeaponToHit

        public static int SpellToHit(int roll, int statBonus, int mEffort, int edge, int training, int spellFocus, int buffs, bool wildMagicActive, int wildMagicEffects)
        {

            // wild magic
            int wildMagicBonus = WildMagicBonus(wildMagicActive, wildMagicEffects);

            return roll + statBonus * (mEffort + edge + training + wildMagicBonus + 1) + spellFocus + buffs;
        
        } // end of SpellToHit

        public static int WeaponToDamage(int numOfDice, int baseWeaponDie, int pEffort, int edge, int tEffort, int statBonus, int enchant, int buffs, 
            bool magusActive, int mEffort, int mEdge, int mageStatBonus, bool wildMagicActive, int wildMagicEffects, bool rageActive, bool bRageActive) 
        {

            // base
            int weaponDiceDamage = RollDice(numOfDice, baseWeaponDie);
            int pEffortBonus = RollDice(pEffort, baseWeaponDie);
            int edgeBonus = RollDice(edge, baseWeaponDie);
            int tEffortBonus = RollDice(tEffort, baseWeaponDie);

            // rage
            int rageBonus = RageBonus(rageActive, baseWeaponDie);
            int bRageBonus = BlindRageBonus(bRageActive, baseWeaponDie);

            // magus
            int magusBonus = MagusBonus(magusActive, mEffort, mEdge, baseWeaponDie, mageStatBonus);

            // wild magic
            int wildMagic = WildMagicBonus(wildMagicActive, wildMagicEffects);
            int wildMagicBonus = RollDice(wildMagic, baseWeaponDie);

            return weaponDiceDamage + pEffortBonus + edgeBonus + tEffortBonus + statBonus + enchant + buffs + rageBonus + bRageBonus + magusBonus + wildMagicBonus;
        
        } // end of WeaponToDamage

        public static int SpellToDamage(int numOfDice, int baseSpellDie, int mEffort, int edge, int statBonus, int buffs, bool wildMagicActive, int wildMagicEffects)
        {

            int spellDiceDamage = RollDice(numOfDice, baseSpellDie);
            int mEffortBonus = RollDice(mEffort, baseSpellDie);
            int edgeBonus = RollDice(edge, baseSpellDie);

            // wild magic
            int wildMagic = WildMagicBonus(wildMagicActive, wildMagicEffects);
            int wildMagicBonus = RollDice(wildMagic, baseSpellDie);

            return spellDiceDamage + mEffortBonus + edgeBonus + buffs + statBonus + wildMagicBonus;
        
        } // end of SpellToDamage

        // rage feat helper
        public static int RageBonus(bool rageActive, int baseWeaponDie) 
        {

            if (rageActive) 
            {

                return RollDice(2, baseWeaponDie);
            
            }

            return 0;
        
        } // end of RageBonus

        // blind rage feat helper
        public static int BlindRageBonus(bool bRageActive, int baseWeaponDie)
        {

            if (bRageActive)
            {

                return RollDice(2, baseWeaponDie);

            }

            return 0;

        } // end of BlindRageBonus

        // magus feat helper
        public static int MagusBonus(bool magusActive, int mEffort, int mEdge, int baseWeaponDie, int mageStatBonus) 
        {

            if (!magusActive || mEffort < 2) { return 0; }

            // +1 baseWeaponDie per 2 mEffort spent
            int magusDice = ((mEffort + mEdge) / 2);
            
            int magusRoll = RollDice(magusDice, baseWeaponDie);
        
            return magusRoll + mageStatBonus;         
        
        } // end of MagusBonus

        // wild magic feat helper
        public static int WildMagicBonus(bool wildMagicActive, int wildMagicEffects) 
        {

            if (wildMagicActive) 
            {

                return wildMagicEffects;
            
            }

            return 0;
        
        } // end of WildMagicBonus
    } // end of Calculator class
} // end of namespace