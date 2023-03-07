using static DataFromTwoDatabases.mysql;
using static DataFromTwoDatabases.sql;

namespace DataFromTwoDatabases
{
    class program
    {
        static void Main(string[] args)
        {
            Class1 class1 = new Class1();
            //Class2 class2 = new Class2();

            string val;
            int res;

            Console.WriteLine("Write a number either 1 or 2: ");
            val = Console.ReadLine();

            // convert to integer
            res = Convert.ToInt32(val);

            if (res == class1.value)
            {
                GetCSV1();
            }
            else
            {
                GetCSV2();
            }
        }
    }
}
   
