using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;

namespace goodmatch
{
    class Program
    {
        private static List<string> fline = new List<string>();
        private static List<string> males = new List<string>();
        private static List<string> females = new List<string>();
        private static List<string> flogs = new List<string>();
        static void Main(string[] args)
        {
            var watch = new Stopwatch();
            watch.Start();
            try
            {
                using (StreamReader fRead = new StreamReader("C:/Users/User/Desktop/derivco/goodmatch/goodmatch/file.csv"))
                {
                    while (!fRead.EndOfStream)
                    {
                        String line = fRead.ReadLine();
                        String[] names = line.Split(',');
                        if (names[1].ToLower().Equals("m"))
                        {
                            if (!males.Contains(names[0]))
                            {
                                males.Add(names[0]);
                            }
                        }
                        else
                        {
                            if (!females.Contains(names[0]))
                            {
                                females.Add(names[0]);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The File could not be read:");
                Console.WriteLine(e.Message);
            }

            matchNames(males, females);
            _ = writeToFile1();

            matchNames(females, males);
            _ = writeToFile2();
            
            matchName();
            watch.Stop();
            flogs.Add($"Execution Time : {watch.ElapsedMilliseconds} ms");
            _ = saveLogs();
        }

        public static void matchName() {
            try
            {
                Console.WriteLine("Enter name 1 : "); var name1 = Console.ReadLine();
                Console.WriteLine("Enter name 2 : "); var name2 = Console.ReadLine();
                if (isLetter(name1) && isLetter(name2))
                {
                    var sentence = name1 + "matches" + name2;
                    var num = countLetters(sentence);

                    var lines = "";
                    if (Convert.ToInt32(num) >= 80)
                    {
                        lines = name1 + " matches " + name2 + " " + num + "%, good match";
                        Console.WriteLine(lines);
                    }
                    else
                    {
                        lines = name1 + " matches " + name2 + " " + num + "%";
                        Console.WriteLine(lines);
                    }
                }
                else
                {
                    Console.WriteLine("Enter a string containing only alphabet.");
                    flogs.Add(name1 + "    " + name2);
                    matchName();
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static async Task saveLogs() {
            try
            {
                using StreamWriter file = new("C:/Users/User/Desktop/derivco/goodmatch/goodmatch/logs.txt", append: true);
                foreach (string lines in flogs)
                {
                    await file.WriteLineAsync(lines);
                }
                Console.WriteLine("Logs have been saved to logs.txt file");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void matchNames(List<string> male, List<string> female) {
            foreach (string name1 in male)
            {
                foreach (string name2 in female)
                {
                    try
                    {
                        if (isLetter(name1) && isLetter(name2))
                        {
                            var sentence = name1 + "matches" + name2;
                            var num = countLetters(sentence);

                            var lines = "";
                            if (Convert.ToInt32(num) >= 80)
                            {
                                lines = name1 + " matches " + name2 + " " + num + "%, good match";
                                fline.Add(lines);
                            }
                            else
                            {
                                lines = name1 + " matches " + name2 + " " + num + "%";
                                fline.Add(lines);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Enter a string containing only alphabet.");
                            flogs.Add(name1 + "    " + name2);
                        }
                    }
                    catch(ArgumentOutOfRangeException e) { 
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }

        public static async Task writeToFile1()
        {
            try
            {
                fline.Sort();
                using StreamWriter file = new("C:/Users/User/Desktop/derivco/goodmatch/goodmatch/output.txt", append: true);
                foreach (string lines in fline)
                {
                    await file.WriteLineAsync(lines);
                }
                fline.Clear();
                Console.WriteLine("Results have been printed on the file called output.txt.");
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static async Task writeToFile2()
        {
            try
            {
                fline.Sort();
                using StreamWriter file = new("C:/Users/User/Desktop/derivco/goodmatch/goodmatch/output2.txt", append: true);
                foreach (string lines in fline)
                {
                    await file.WriteLineAsync(lines);
                }
                fline.Clear();
                Console.WriteLine("Results for a reverse combination have been printed to output2.txt.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static bool isLetter(string name)
        {
            foreach (char c in name)
            {
                if (!Char.IsLetter(c))
                    return false;
            }
            return true;
        }

        public static string countLetters(string sentence) {
            var nums = "";
            var cchar = new List<Char>();
            var schar = sentence.ToLower().ToCharArray();
            int count = 0;
            for (int i=0;i<schar.Length;i++) {
                if (!cchar.Contains(schar[i]))
                {
                    for (int j=0;j<schar.Length;j++) {
                        if (schar[i] == schar[j]) {
                            count++;
                        }
                    }
                    cchar.Add(schar[i]);
                    nums += Convert.ToString(count);
                    count = 0;
                }
            }
            cchar.Clear();
            List<int> nm = getPercentage(nums);
            string vb = "";
            for (int i=0;i<nm.Count();i++) {
                vb += nm[i];
            }
            return vb;
        }


        public static List<int> getPercentage(string nums) {
            char[] nc = nums.ToCharArray();
            List<int> ll = new List<int>(); 
            int per = 0;
            if (nc.Length < 3) {
                foreach (char c in nc) {
                    ll.Add(Convert.ToInt32(char.GetNumericValue(c)));
                }
                return ll;
            }else {
                int fCount = 0;
                int bCount = nc.Length-1;
                var numss = "";
                while (fCount <= bCount)
                {
                    if (fCount != bCount)
                    {
                        per = int.Parse(nc[fCount].ToString()) + int.Parse(nc[bCount].ToString());
                        numss += Convert.ToString(per);
                        ll.Add(per);
                        fCount++;
                        bCount--;
                    }
                    else
                    {
                        per = int.Parse(nc[fCount].ToString());
                        numss += Convert.ToString(per);
                        ll.Add(per);
                        break;
                    }
                }
                return getPercentage(numss);
            }
        } 
    }
}
