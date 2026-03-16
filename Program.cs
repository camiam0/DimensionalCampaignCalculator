using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimensionalCampaignCalculator
{
    internal class Program
    {
        
        // main method and menu for navigating calculator functions
        static void Main(string[] args)
        {

            bool exit = false;

            while (!exit)
            {

                Console.WriteLine("Welcome to the Dimensional Campaign Calculator!");
                Console.WriteLine();

                Console.WriteLine("""
                Please type a command:
                    1 - Make a skill check
                    2 - Hit with a weapon
                    3 - Hit with a spell
                    4 - Damage with a weapon
                    5 - Damage with a spell
                    6 - Heal a target
                    7 - Quit
                """);

                Console.WriteLine();
                Console.Write("> ");

                // check if input is an integer instead of a character
                if (!int.TryParse(Console.ReadLine(), out int command))
                {
                    
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                
                }

                switch (command)
                {

                    case 1:
                    { 
                        
                        int roll = Calculator.RollDice(1, 20);
                        Console.WriteLine($"You rolled {roll}");

                        if (roll == 20)
                        { 
                            
                            Console.WriteLine("Natural 20! Critical hit.");
                            Console.WriteLine("You gain 2 free effort to spend.");
                            
                        }

                        int statBonus = ReadInt("Associated stat bonus: ");
                        int effort = ReadInt("Effort spent: ");
                        int edge = ReadInt("Edge bonus: ");
                        int training = ReadInt("Training bonus: ");

                        int result = Calculator.SkillCheck
                        (
                        
                            roll,
                            statBonus,
                            effort,
                            edge,
                            training
                            
                        );

                        Console.WriteLine($"Skill check: {result}");
                        Console.WriteLine("\nPress Enter to clear result and continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    } // end of case 1

                    // hit with a weapon
                    case 2:
                    {

                        int roll = Calculator.RollDice(1, 20);
                        Console.WriteLine($"You rolled {roll}");

                        if (roll == 20)
                        { 
                            
                            Console.WriteLine("Natural 20! Critical hit.");
                            Console.WriteLine("You gain 2 free effort to spend.");
                            
                        }
                        
                        int statBonus = ReadInt("Associated stat bonus: ");
                        int pEffort = ReadInt("Physical effort spent: ");
                        int edge = ReadInt("Edge bonus: ");
                        int tEffort = ReadInt("Tactical effort spent: ");
                        int training = ReadInt("Training bonus: ");
                        int enchant = ReadInt("Weapon enchantment bonus: ");
                        
                        int result = Calculator.WeaponToHit
                        (

                            roll,
                            statBonus,
                            pEffort,
                            edge,
                            tEffort,
                            training,
                            enchant
                            
                        );

                        bool heroicActive = ReadYesNo("Is Heroic Presence active? (y/n): ");
                        if (heroicActive) { result = result + 5; }

                        Console.WriteLine($"{result} to hit.");
                        Console.WriteLine("\nPress Enter to clear result and continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    } // end of case 2
                    
                    // hit with a spell
                    case 3:
                    {

                        int roll = Calculator.RollDice(1, 20);
                        Console.WriteLine($"You rolled {roll}");

                        if (roll == 20)
                        { 
                            
                            Console.WriteLine("Natural 20! Critical hit.");
                            Console.WriteLine("You gain 2 free effort to spend.");
                            
                        }
                        
                        int statBonus = ReadInt("Associated stat bonus: ");
                        int mEffort = ReadInt("Mental effort spent: ");
                        int edge = ReadInt("Edge bonus: ");
                        int sEffort = ReadInt("Special effort spent: ");
                        int aEffort = ReadInt("Bonus armor effort spent: ");
                        int training = ReadInt("Training bonus: ");
                        int spellFocus = ReadInt("Spell focus bonus: ");
                        
                        bool wildMagicActive = ReadYesNo("Is Wild Magic active? (y/n): ");

                        int wildMagicEffects = 0;
                        if (wildMagicActive) { wildMagicEffects = ReadInt("How many Wild Magic effects have been accepted? "); }
                        
                        int result = Calculator.SpellToHit
                        (
                        
                            roll,
                            statBonus,
                            mEffort,
                            edge,
                            sEffort,
                            aEffort,
                            training,
                            spellFocus,
                            wildMagicActive,
                            wildMagicEffects
                            
                        );

                        bool heroicActive = ReadYesNo("Is Heroic Presence active? (y/n): ");
                        if (heroicActive) { result = result + 5; }
                         
                        Console.WriteLine($"{result} to hit.");
                        Console.WriteLine("\nPress Enter to clear result and continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    
                    } // end of case 3
                    
                    // damage with a weapon
                    case 4:
                    {

                        int numOfDice = ReadInt("Number of weapon dice: ");
                        int baseWeaponDie = ReadInt("Weapon die size (4, 6, 8, 10, etc.): ");
                        int pEffort = ReadInt("Physical effort spent: ");
                        int edge = ReadInt("Edge bonus: ");
                        int tEffort = ReadInt("Tactical effort spent: ");
                        int statBonus = ReadInt("Associated stat bonus: ");
                        int enchant = ReadInt("Enchantment bonus: ");

                        bool magusActive = ReadYesNo("Is Magus active? (y/n): ");
                        
                        int mEffort = 0;
                        int mEdge = 0;
                        int mageStatBonus = 0;
                        bool wildMagicActive = false;
                        int wildMagicEffects = 0;
                            
                        if (magusActive)
                        {

                            mEffort = ReadInt("Mental effort spent (must be in multiples of 2): ");
                            mEdge = ReadInt("Mental edge bonus: ");
                            mageStatBonus = ReadInt("Associated mage stat bonus: ");

                            wildMagicActive = ReadYesNo("Is Wild Magic active? (y/n): ");

                            wildMagicEffects = 0;
                            if (wildMagicActive) { wildMagicEffects = ReadInt("How many Wild Magic effects have been accepted? "); }

                        }

                        bool bRageActive = false;
                        bool rageActive = ReadYesNo("Is Rage active? (y/n): ");
                        if (rageActive) { bRageActive = ReadYesNo("Is Blind Rage active? (y/n): "); }

                        int result = Calculator.WeaponToDamage
                        (
                        
                            numOfDice,
                            baseWeaponDie,
                            pEffort,
                            edge,
                            tEffort,
                            statBonus,
                            enchant,
                            magusActive,
                            mEffort,
                            mEdge,
                            mageStatBonus,
                            wildMagicActive,
                            wildMagicEffects,
                            rageActive,
                            bRageActive
                            
                        );
                                                    
                        Console.WriteLine($"Damage: {result}");
                        Console.WriteLine("\nPress Enter to clear result and continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;    
                        
                    } // end of case 4

                    // damage with a spell
                    case 5:
                    {
                        
                        int numOfDice = ReadInt("Number of spell dice: ");
                        int baseSpellDie = ReadInt("Spell die size (4, 6, 8, 10, etc.): ");
                        int mEffort = ReadInt("Mental effort spent: ");
                        int aEffort = ReadInt("Bonus armor effort spent: "); 
                        int edge = ReadInt("Edge bonus: ");
                        int sEffort = ReadInt("Special effort spent: ");
                        int statBonus = ReadInt("Associated stat bonus: ");

                        bool wildMagicActive = ReadYesNo("Is Wild Magic active? (y/n): ");
                        int wildMagicEffects = 0;
                        if (wildMagicActive) { wildMagicEffects = ReadInt("How many Wild Magic effects have been accepted? "); }

                        int result = Calculator.SpellToDamage
                        (
                        
                            numOfDice,
                            baseSpellDie,
                            mEffort,
                            edge,
                            sEffort,
                            aEffort,
                            statBonus,
                            wildMagicActive,
                            wildMagicEffects
                            
                        );
                        
                        Console.WriteLine($"Damage: {result}");
                        Console.WriteLine("\nPress Enter to clear result and continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    } // end of case 5

                    // heal
                    case 6:
                    {
                        
                        int numOfDice = ReadInt("Number of catalyst dice: ");
                        int baseHealDie = ReadInt("Heal die size (4, 6, 8, 10, etc.): ");
                        int mEffort = ReadInt("Mental effort spent: ");
                        int statBonus = ReadInt("Associated stat bonus: ");

                        bool healingFocusActive = false;
                        int healingFocusRanks = 0;

                        if (mEffort > 0)
                        { 
                        
                            healingFocusActive = ReadYesNo("Is Healing Focus being used? (y/n): ");
                            if (healingFocusActive) { healingFocusRanks = ReadInt("How many ranks of Healing Focus do you have? "); }

                        }

                        int result = Calculator.Heal
                        (
                            
                            numOfDice,
                            baseHealDie,
                            mEffort,
                            statBonus,
                            healingFocusActive,
                            healingFocusRanks
                            
                        );

                        Console.WriteLine($"Heal: {result}");
                        Console.WriteLine("\nPress Enter to clear result and continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    } // end of case 6
                    
                    // quit
                    case 7:
                        
                        Console.Clear();
                        Console.WriteLine("Quitting program. Goodbye!");
                        exit = true;
                        break;

                    default:
                        
                        Console.WriteLine("Invalid command. Please only enter an integer between 1-5.");
                        break;

                } // end of switch case
            } // end of exit while

            Console.WriteLine("\nPress any key to close...");
            Console.ReadLine();
            Console.Clear();

        } // end of Main
        
        // helper method for reading integer inputs
        public static int ReadInt(string prompt)
        {

            while (true)
            {
                
                Console.Write(prompt);

                if (int.TryParse(Console.ReadLine(), out int value)) 
                {

                    return value;
                
                }

                Console.WriteLine("Invalid input. Please enter a whole number.");

            }
        } // end of ReadInt

        public static bool ReadYesNo(string prompt) 
        {

            while (true) 
            {

                Console.Write(prompt);

                string? input = Console.ReadLine()?.Trim().ToLower();

                if (input == "y") { return true; }

                if (input == "n") { return false; }

                Console.WriteLine("Invalid input. Please enter y or n.");
            
            } 
        } // end of ReadYesNo
    } // end of class
} // end of namespace