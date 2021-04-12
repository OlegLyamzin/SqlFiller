using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Dapper;

namespace SqlFiller
{
    class Program
    {
        private static List<string> _dictonaryName;
        private static List<string> _dictonaryLastName;
        private static int start = 5000001;
        private static int finish = 10000000;
        private static int values = 1000;
        static void Main(string[] args)
        {
            while (start < finish)
            {
                try
                {

                    FillNames();
                    //Ip замени
                    using (var connection = new SqlConnection("Data Source={Ip};Initial Catalog=CRM;Persist Security Info=True;User ID=student;Password=qwe!23"))
                    {
                        for (var i = start; start < finish; start += values)
                        {
                            var query = CreateQuery(start);
                            var orderDetail = connection.Execute(query);
                            Console.WriteLine(start);
                        }
                    }
                }
                catch(Exception ex)
                {
                    System.Threading.Thread.Sleep(5000);
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static string CreateQuery(int n)
        {
            var sb = new StringBuilder();
            var random = new Random();
            sb.Append(" set IDENTITY_INSERT [dbo].[Lead] on INSERT INTO [Lead] (Id,FirstName,LastName,Login,Password,Email,Phone,BirthDate,CityId,IsDeleted) VALUES ");
            for (int i = n; i < n + values; i++)
            {
                sb.Append($" ({i}, '{_dictonaryName[i % 299]}', '{_dictonaryLastName[i % 299]}', 'login{i}', 'password{i}', 'email{i}@gmail.com', '8950{random.Next(1000000, 9999999)}', '{random.Next(1, 12)}/{random.Next(1, 12)}/{random.Next(1960, 2001)}', {random.Next(1, 4)}, {random.Next(0, 2)}),");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(';');
            Console.WriteLine("Query done");
            return sb.ToString();
        }

        private static void FillNames()
        {
            _dictonaryName = new List<string>();
            _dictonaryLastName = new List<string>();

            var names = "Robert Murphy;Larry Guerrero;Danielle Bailey;Rick Brown;David Alvarado;Debra Morgan;Michelle Collins;Brenda Myers;Stephen Parker;Stephen Woods;Walter Berry;Phyllis Nelson;Erin Wilkins;Johnny Arnold;Bessie Perry;Beverly Robinson;Delores Stevenson;Michael Mendoza;Walter Lopez;James Thomas;Ricky Wallace;William Cruz;Evelyn Hill;Maria Blair;Debbie Thornton;Timothy Bradley;Martin Page;David Dixon;Eddie Vaughn;Lillie Campbell;John Medina;Charles Vasquez;Christina Joseph;Cynthia Jenkins;Terry Miles;Christine Thompson;Karen Ramirez;James Olson;Joseph Vargas;Willie Bryan;Tyler Rodriguez;Jonathan Mitchell;Mitchell Brown;Ronald Simmons;Cheryl Cortez;Manuel Mendez;Michelle Todd;Mary Murray;Juanita Barnes;Amanda Moody;Kathleen Miller;Roger Brown;Gordon Hayes;John Gardner;James Johnson;Charles Smith;Charlotte Wilkins;Clara Shelton;Julia Rodriguez;Ramon Johnston;Karen Martin;Doris Williams;Priscilla Hudson;Joanne Mills;Rebecca Brown;Erin Harvey;Bernard Roberts;Frances Smith;Scott Martinez;Anna Young;Sonia Adams;Juan Caldwell;Valerie Griffin;Jonathan Ryan;Donald Allen;Norma Bell;Karen Wilson;William Fox;Bertha Newman;Melissa Sanchez;Edward Obrien;Robert Rivera;Cody Ortiz;Raul Morgan;Melissa Smith;Edward Griffith;Theresa Gomez;Barbara Gregory;Rhonda Schmidt;Heather Warner;Vickie Harrison;Minnie Ross;Amy George;Gary French;Billy Allen;Willie Jordan;Annette Johnson;William Burns;Jesse Murray;Darlene Thomas;Ryan Powell;Sarah Greer;Mildred Frank;William Stewart;Elaine Jimenez;Amanda Mitchell;Roger Simmons;Constance Bailey;Dana Cummings;Carmen Davis;Katherine Sutton;Mary Cook;Brenda Robbins;Daniel Rice;Matthew Hernandez;Robert Fisher;Joseph Hernandez;Gail Bridges;Marilyn Mitchell;Travis Hart;Matthew Bryan;Troy Gordon;Chad Austin;Darren Richardson;Kelly Smith;Perry Hunter;Andrea Jones;Tonya Turner;Mike Johnson;Anthony Allen;James Rodriguez;Irma Ross;Sharon Rodriguez;Jason Peterson;Manuel Russell;Gerald Schmidt;Brian Hogan;Robin Lewis;Rodney Mills;Rose Davis;Linda Walton;Glenda Walters;Susan Martinez;David Jenkins;Sandra Johnson;Douglas Taylor;Thomas Wheeler;Craig Hanson;Felix Ford;Leon Sims;Brent Daniels;Caroline Foster;Allan Mendez;Richard Ortiz;Marilyn Hale;Donna Brown;Jesus Willis;Ralph Norman;Cheryl Green;Katherine Black;Pearl Jimenez;Dana Pearson;Jose Carter;Frank Knight;Paula Smith;Mary Pittman;Lydia Wells;Francis Henry;Marcus McLaughlin;Steven Rogers;Gloria Hodges;Allison Perez;Ashley Cannon;Nicole Perez;Amy Mack;Danny Brooks;Pauline Pierce;Edwin Martin;Maria Allison;Jimmy Baker;Leah Hall;April Ward;Sandra Holloway;Elizabeth Parks;Timothy Jones;Roberto Butler;John Johnson;Janice Ryan;Dennis Scott;Jared Price;Dennis Newton;Jamie Poole;Albert Kelly;Megan Bennett;Richard Pope;Helen Graves;John Flores;Ruth Lee;Darryl Smith;Doris Carpenter;Diana Barrett;Eric Peterson;Bonnie Johnson;Harry Green;Amy Miller;Thomas Brown;Maria Abbott;James Wilkerson;Sandra Wells;Linda Cain;Glenn Robinson;Bertha Elliott;Bruce Green;John Thomas;Mary Ward;Cindy Martinez;Allen McKinney;Robert Davis;Shelly Webster;Sharon Robinson;Elizabeth Lopez;Ann Woods;Wallace Wells;Rachel Buchanan;Joseph Ross;Timothy Sanders;Nancy Williams;William Smith;Kenneth Harris;Kim Floyd;Daniel Fox;Diane Stanley;Tracy Martin;Daisy Jensen;Daniel Scott;Brenda Garcia;Linda Torres;Robert Boone;Ruth James;Cynthia Lewis;Howard Bridges;Emily Chapman;Esther Robinson;Christopher McCarthy;Linda Kelley;Robert Thomas;Mary Scott;Gary Rogers;Jamie Carter;Debra Martin;John Norton;Fred Boyd;William Allen;Susan Gray;David Smith;Lee Shelton;Michael Taylor;Vera Moore;Scott Boyd;William Nelson;Tyrone Hunt;Stephen Dean;Grace Gray;Jennifer Moore;Carlos Gill;Andrea Bowers;Edna Montgomery;Jason Davis;Diane Harper;Elizabeth Stevenson;Mary Jenkins;Martin Waters;Raymond Walters;Julie Knight;Kimberly Hall;Kenneth Watson;Clifford Higgins;Christopher Reid;Michael Wilson;Karen Wilson;Tara Wilson;Steve Gonzales;Marion Copeland;Michael Lee;Keith Lopez;Robert Williams;Michael Howard;Russell Wright;Lisa Norris;Elaine Williams;Ella Lowe;Frank White;Daniel Chandler;Jason Sandoval;Lloyd Barker;Theresa Watson;Cheryl Armstrong;Lori Beck;Mark Crawford;Steve Hammond".Split(';');
            foreach(var fullName in names)
            {
                var firstSecondName = fullName.Split(" ");
                _dictonaryName.Add(firstSecondName[0]);
                _dictonaryLastName.Add(firstSecondName[1]);
            }
            Console.WriteLine("Names filled!");
        }
    }
}
