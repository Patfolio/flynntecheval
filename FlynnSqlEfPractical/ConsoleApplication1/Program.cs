using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = new FlynnPracticalTestEntities())
            {
                Console.WriteLine(" Table 1");
                Console.WriteLine(" -------------");
                Console.WriteLine(" | Id | Code |");
                Console.WriteLine(" -------------");
                var allt1 = from t1 in ctx.Table_1 select new { id = t1.Id, code = t1.Code };
                foreach (var newTable in allt1)
                {
                    Console.WriteLine(" | {0}  | {1}   |", newTable.id, newTable.code);
                }
                Console.WriteLine(" -------------");
                Console.WriteLine();

                Console.WriteLine(" Table 2");
                Console.WriteLine(" --------------");
                Console.WriteLine(" | Id | Name  |");
                Console.WriteLine(" -------------");
                var allt2 = from t2 in ctx.Table_2 select new { id = t2.Id, name = t2.Name };
                foreach (var newTable in allt2)
                {
                    Console.WriteLine(" | {0}  | {1,5} |", newTable.id, newTable.name);
                }
                Console.WriteLine(" -------------");
                Console.WriteLine();

                Console.WriteLine(" Inner Join");
                Console.WriteLine(" ----------------");
                Console.WriteLine(" | Code | Name  |");
                Console.WriteLine(" ----------------");
                var innerJoin = from t1 in ctx.Table_1
                                join t2 in ctx.Table_2 on t1.Id equals t2.Id
                                select new { code = t1.Code, name = t2.Name };

                foreach (var newTable in innerJoin)
                {
                    Console.WriteLine(" | {0}   | {1,5} |", newTable.code, newTable.name);
                }
                Console.WriteLine(" ----------------");
                Console.WriteLine();

                Console.WriteLine(" Left Join");
                Console.WriteLine(" --------------------");
                Console.WriteLine(" | Code | Name      |");
                Console.WriteLine(" --------------------");
                var leftJoin = from t1 in ctx.Table_1
                               join t2 in ctx.Table_2 on t1.Id equals t2.Id into ps
                               from t2 in ps.DefaultIfEmpty()
                               select new { code = t1.Code, name = t2.Name == null ? "(No name)" : t2.Name };

                foreach (var newTable in leftJoin)
                {
                    Console.WriteLine(" | {0}   | {1,9} |", newTable.code, newTable.name);
                }
                Console.WriteLine(" --------------------");
                Console.WriteLine();

                Console.WriteLine(" Right Join");
                Console.WriteLine(" ---------------------");
                Console.WriteLine(" | Code      | Name  |");
                Console.WriteLine(" ---------------------");
                var rightJoin = from t2 in ctx.Table_2
                                join t1 in ctx.Table_1 on t2.Id equals t1.Id into ps
                                from t1 in ps.DefaultIfEmpty()
                                select new { code = t1.Code == null ? "(No code)" : t1.Code, name = t2.Name };

                foreach (var newTable in rightJoin)
                {
                    Console.WriteLine(" | {0,9} | {1,5} |", newTable.code, newTable.name);
                }
                Console.WriteLine(" ---------------------");
                Console.WriteLine();

                Console.WriteLine(" Full Join");
                Console.WriteLine(" -------------------------");
                Console.WriteLine(" | Code      | Name      |");
                Console.WriteLine(" -------------------------");
                var fullJoin = leftJoin.Union(rightJoin);
                foreach (var newTable in fullJoin)
                {
                    Console.WriteLine(" | {0,9} | {1,9} |", newTable.code, newTable.name);
                }
                Console.WriteLine(" -------------------------");
                Console.ReadLine();

            }
        }
    }
}
