using Microsoft.VisualBasic;
using System.Text;

namespace SlotMachine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            intro();
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine();
            Console.Write("Credits:");


            Random rnd = new Random();
            decimal credits = Console.ReadLine().Parser();
      
            PlayRound(ref credits ,rnd);

            Console.ReadLine();
            
        }



        
        public static void intro() 
        {
            Console.WriteLine(@"
#####  #       ####### #######  #####  
#     # #       #     #    #    #     # 
#       #       #     #    #    #       
 #####  #       #     #    #     #####  
      # #       #     #    #          # 
#     # #       #     #    #    #     # 
 #####  ####### #######    #     #####");
        }


        public static char SpinReel(Random rnd) 
        {

            char myChar = rnd.Next(0,101) switch
            {
                <= 49 => 'A',
                <= 84 => 'B',
                _ => 'C'
            };
            return myChar;  
        }
     static   bool IsRightValue(decimal credits ,decimal bet) 
        {
            return credits >= bet;
        }

        public static int PlayOut(char char1, char char2, char char3)
        {

            Console.WriteLine("{0}{1}{2}" , char1, char2, char3);
            string asString = char1.ToString() + char2.ToString() + char3.ToString();
           


            Dictionary<string, int> Value = new Dictionary<string, int>
            {
                { "AAA",10 },
                {"BBB",20 },
                {"CCC",50 }
            };

            if (Value.ContainsKey(asString))
            {
                return Value[asString];
            }
            else
            {
                if (char1 == char2 || char2 == char3)
                {
                    return 2;
                }
                else
                {
                    return 0;
                }

            }
        }

        public static void PlayRound(ref decimal credits, Random rnd)
        {
            int count = 0;
           
            decimal stake;



            do
            {


    
                if (credits <= 0) 
                {
                   
                    Console.WriteLine("Insuficient fonds");

                    break;
                }
                
                Console.WriteLine("Credits: {0:C}",credits);
                Console.WriteLine("Enter Your Bet");

                stake = Console.ReadLine().Parser();


                Console.WriteLine("Bet: {0:C}", stake);

                if (!IsRightValue(credits, stake))
                {
     
                    "Invalid stake".Printer(ConsoleColor.Red,false);
                   
                }
                else
                {
                    ++count;
                    $"Round {count}".Printer(ConsoleColor.Magenta, false);
                    credits -= stake;
            
                    decimal amount =  stake * PlayOut(SpinReel(rnd), SpinReel(rnd), SpinReel(rnd));

                    credits += amount;
                    if (amount != 0)
                    {
                        Console.WriteLine($"Winst:{amount:C}");
                    }

                }

            } while (stake != -1m || credits > 0);
        }

    }

}





    public static class Helpers 
    {
    public static void Printer(this string message, ConsoleColor color, bool write)
    {
        if (write)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
       
        }
        else
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
          
        }
        Console.ResetColor();
    }
    public static decimal  Parser(this string str) 
        {
        string input = str?.Trim() ?? string.Empty;
   
        if (string.Equals(input , "Q",StringComparison.OrdinalIgnoreCase)) 
            {
                return -1;
            }




        decimal value;
        while (!decimal.TryParse(str ,out  value)) 
            {
            Console.WriteLine("Invalid Input");
                str = Console.ReadLine();
                
              
            }
            return value;
        }

    }

