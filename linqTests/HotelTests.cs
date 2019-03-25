using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using linq.model;
using linq.entitymodel;
using System.Data.Entity;
using System.Text;


namespace linq.Tests
{
    [TestClass()]
    public class HotelTests
    {
        EntityModel db = new EntityModel();

        [TestMethod()]
        public void DbConnection()
        {
            Assert.IsNotNull(db);
        }

        [TestMethod()]
        public void A1()
        {
            var q = from h in db.Hotels select h;
            Assert.IsNotNull(q);
            string res = GetPropertyListString(q, db.Hotels);
            Assert.AreEqual(@"1 The Pope Vaticanstreet 1  1111 Bishopcity
2 Lucky Star Lucky Road 12  2222 Hometown
3 Discount Inexpensive Road 7 3333 Cheaptown
4 deLuxeCapital Luxury Road 99  4444 Luxus
5 Discount Inexpensive Street 12  6666 Pricetown
6 Prindsen Algade 5, 4000 Roskilde
7 Scandic Sdr. Ringvej 5, 4000 Roskilde", res);
        }

        [TestMethod()]
        public void A2()
        {
            var q = from h in db.Hotels where h.Address.Contains("Roskilde") select h;
            var val = @"6 Prindsen Algade 5, 4000 Roskilde
7 Scandic Sdr. Ringvej 5, 4000 Roskilde";
            Assert.IsNotNull(q);
            Assert.IsNotNull(val);
            string res = GetPropertyListString(q, db.Hotels);
            Assert.AreEqual(val, res);
        }

        [TestMethod()]
        public void A3()
        {
            var q = from h in db.Rooms where (h.Price < 200 && h.Types.Contains("D")) select h;
            var val = @"1 3 D 175
2 3 D 180
2 5 D 170";
            Assert.IsNotNull(q);
            Assert.IsNotNull(val);
            string res = GetPropertyListString(q, db.Rooms);
            Assert.AreEqual(val, res);
        }

        [TestMethod()]
        public void A4()
        {
            var q = from h in db.Rooms where (h.Price < 400 && (h.Types.Contains("F") || h.Types.Contains("D"))) orderby h.Price select h;
            var val = @"2 5 D 170
1 3 D 175
2 3 D 180
31 3 F 200
1 1 D 200
2 7 D 200
3 1 D 200
1 7 D 200
2 1 D 200
3 7 D 200
4 1 D 200
4 7 D 200
21 1 F 220
24 7 F 220
23 7 F 220
21 7 F 220
22 1 F 220
22 7 F 220
23 1 F 220
32 3 F 230
4 2 D 230
2 2 D 230
3 2 D 230
1 2 D 230
1 5 D 250
1 6 D 290
21 2 F 300
21 5 F 300
22 2 F 300
22 5 F 310
23 5 F 320
24 5 F 320
21 6 F 360
22 6 F 370
23 6 F 380
24 6 F 380";
            Assert.IsNotNull(q);
            Assert.IsNotNull(val);
            string res = GetPropertyListString(q, db.Rooms);
            Assert.AreEqual(val, res);
        }

        [TestMethod()]
        public void A5()
        {
            var q = from h in db.Rooms where (h.Price > 400 && (h.Types.Contains("F") || h.Types.Contains("D"))) select h;
            var val = @"1 4 D 500
2 4 D 550
3 4 D 550";
            Assert.IsNotNull(q);
            Assert.IsNotNull(val);
            string res = GetPropertyListString(q, db.Rooms);
            Assert.AreEqual(val, res);
        }

        [TestMethod()]
        public void A6()
        {
            var q = from h in db.Guests where h.Name.Substring(0, 1) == "G" select h;
            var val = @"3 Goeg Sunset Blvd. 8, 2222 Hjemby
4 Gokke Sunset Blvd. 8, 2222 Hjemby
9 Godzilla Dommervænget 16A, 4000 Roskilde";
            Assert.IsNotNull(q);
            Assert.IsNotNull(val);
            string res = GetPropertyListString(q, db.Guests);
            Assert.AreEqual(val, res);
        }

        [TestMethod()]
        public void B1()
        {
            var q = db.Hotels.Count<Hotel>();
            var val = 7;
            var res = q;
            Assert.IsNotNull(q);
            Assert.IsNotNull(val);
            Assert.AreEqual(val, res);
        }

        [TestMethod()]
        public void B2()
        {
            var q = from h in db.Hotels where h.Address.Contains("Roskilde") select h;
            var res = q.Count<Hotel>();
            var val = 2;
            Assert.IsNotNull(q);
            Assert.IsNotNull(val);
            Assert.AreEqual(val, res);
        }

        [TestMethod()]
        public void B3()
        {
            var q = from h in db.Rooms select h;
            double val = 243.51851851851851D;
            double res = q.Average(h => h.Price);
            Assert.IsNotNull(q);
            Assert.IsNotNull(val);
            Assert.AreEqual(val, res);
        }

        [TestMethod()]
        public void B4()
        {
            var q = from h in db.Rooms where h.Types == "S" select h;
            var val = 177;
            var res = q.Average(h => h.Price);
            Assert.IsNotNull(q);
            Assert.IsNotNull(val);
            Assert.AreEqual(val, res);
        }

        [TestMethod()]
        public void B5()
        {
            var q = from h in db.Rooms where h.Types == "D" select h;
            var val = 259.25;
            var res = q.Average(h => h.Price);
            Assert.IsNotNull(q);
            Assert.IsNotNull(val);
            Assert.AreEqual(val, res);
        }

        [TestMethod()]
        public void B6()
        {
            var q = from h in db.Rooms where h.Hotel.Name.Contains("Scandic") select h;
            var val = 190;
            var res = q.Average(h => h.Price);
            Assert.IsNotNull(q);
            Assert.IsNotNull(val);
            Assert.AreEqual(val, res);
        }

        [TestMethod()]
        public void B7()
        {
            var q = from h in db.Rooms where h.Types == "D" select h;
            var val = 5185;
            var res = q.Sum(h => h.Price);
            Assert.IsNotNull(q);
            Assert.IsNotNull(val);
            Assert.AreEqual(val, res);
        }

        [TestMethod()]
        public void B8()
        {
            // TOOD
            var q = from h in db.Bookings where (h.Date_From.Month == 3 || h.Date_To.Month == 3) select h;
            int res = q.GroupBy(x => x.Guest_No).Count();
            int val = 4;

            Assert.IsNotNull(q);
            Assert.IsNotNull(val);
            Assert.AreEqual(val, res);
        }

        [TestMethod()]
        public void B9()
        {
            var q = from h in db.Bookings where h.Date_From < DateTime.Now && h.Date_To > DateTime.Now select h;
            var val = 0;
            var res = q.Count();
            Assert.IsNotNull(q);
            Assert.IsNotNull(val);
            Assert.AreEqual(val, res);
        }

        [TestMethod()]
        public void B10()
        {
            var date = DateTime.Now.AddDays(1);
            var q = from h in db.Bookings where h.Date_From <= date && h.Date_To > date select h;
            var val = 0;
            var res = q.Count();
            Assert.IsNotNull(q);
            Assert.IsNotNull(val);
            Assert.AreEqual(val, res);
        }

        [TestMethod()]
        public void C1()
        {
            var q = from h in db.Guests where h.Address.Contains("Roskilde") select h;
            var val = @"9 Godzilla Dommervænget 16A, 4000 Roskilde
10 KingKong Hyrdevænget 38, 4000 Roskilde
11 KongHans Algade 10, 4000 Roskilde
12 Hans Vikingevej 45, 4000 Roskilde
13 Poul Domkirkevej 12, 4000 Roskilde
14 Erik Hestetorvet 8, 4000 Roskilde
15 Ulla Stændertorvet 4, 4000 Roskilde
16 Yrsa Sdr. Ringvej 21, 4000 Roskilde
17 Yvonne Østre Ringvej 12, 4000 Roskilde
18 Tim Ringstedgade 33, 4000 Roskilde
19 Sten Ringstedvej 23, 4000 Roskilde
20 Erland Skovbovængets alle 3, 4000 Roskilde
21 Erwin Ternevej 17, 4000 Roskilde
22 Åge Solsortevej 9, 4000 Roskilde
23 Åse Gyvelvej 45, 4000 Roskilde
24 Frede Københavnsvej 25, 4000 Roskilde
25 Palle Sct Ohlsgade 10, 4000 Roskilde
26 Jørn Dronning Amaliesvej 22, 4000 Roskilde
27 Stefan Lærkevej 65, 4000 Roskilde
28 John By03ken 56, 4000 Roskilde
29 Dana Byageren 12, 4000 Roskilde
30 Arn Vindingevej 23, 4000 Roskilde";
            Assert.IsNotNull(q);
            Assert.IsNotNull(val);
            string res = GetPropertyListString(q, db.Guests);
            Assert.AreEqual(val, res);
        }

        [TestMethod()]
        public void C2()
        {
            var q = from h in db.Guests where h.Address.Contains("Roskilde") orderby h.Name select h;
            var val = @"22 Åge Solsortevej 9, 4000 Roskilde
30 Arn Vindingevej 23, 4000 Roskilde
23 Åse Gyvelvej 45, 4000 Roskilde
29 Dana Byageren 12, 4000 Roskilde
14 Erik Hestetorvet 8, 4000 Roskilde
20 Erland Skovbovængets alle 3, 4000 Roskilde
21 Erwin Ternevej 17, 4000 Roskilde
24 Frede Københavnsvej 25, 4000 Roskilde
9 Godzilla Dommervænget 16A, 4000 Roskilde
12 Hans Vikingevej 45, 4000 Roskilde
28 John By03ken 56, 4000 Roskilde
26 Jørn Dronning Amaliesvej 22, 4000 Roskilde
10 KingKong Hyrdevænget 38, 4000 Roskilde
11 KongHans Algade 10, 4000 Roskilde
25 Palle Sct Ohlsgade 10, 4000 Roskilde
13 Poul Domkirkevej 12, 4000 Roskilde
27 Stefan Lærkevej 65, 4000 Roskilde
19 Sten Ringstedvej 23, 4000 Roskilde
18 Tim Ringstedgade 33, 4000 Roskilde
15 Ulla Stændertorvet 4, 4000 Roskilde
16 Yrsa Sdr. Ringvej 21, 4000 Roskilde
17 Yvonne Østre Ringvej 12, 4000 Roskilde";
            Assert.IsNotNull(q);
            Assert.IsNotNull(val);
            string res = GetPropertyListString(q, db.Guests);
            Assert.AreEqual(val, res);
        }

        [TestMethod()]
        public void C3()
        {
            var q = from h in db.Rooms where h.Hotel.Name.Contains("Prindsen") select h;
            var val = @"1 6 D 290
11 6 S 185
21 6 F 360
22 6 F 370
23 6 F 380
24 6 F 380";
            Assert.IsNotNull(q);
            Assert.IsNotNull(val);
            string res = GetPropertyListString(q, db.Rooms);
            Assert.AreEqual(val, res);
        }

        [TestMethod()]
        public void C4()
        {
            var q = from g in db.Guests from b in db.Bookings from r in db.Rooms where (b.Guest_No == g.Guest_No) && (b.Room_No == r.Room_No) && r.Hotel.Name == "Prindsen" select g;
            var val = @"4 Gokke Sunset Blvd. 8, 2222 Hjemby
3 Goeg Sunset Blvd. 8, 2222 Hjemby
3 Goeg Sunset Blvd. 8, 2222 Hjemby
10 KingKong Hyrdevænget 38, 4000 Roskilde
6 Bi Bredgade 198, 3333 Lilleby
2 Adam Paradisvej 7, 1111 Bispeborg
2 Adam Paradisvej 7, 1111 Bispeborg
8 Julie Kaerlighedstunellen 2, 4444 Borgerslev
5 Fy Klovnevej 87, 3333 Lilleby
6 Bi Bredgade 198, 3333 Lilleby
12 Hans Vikingevej 45, 4000 Roskilde
13 Poul Domkirkevej 12, 4000 Roskilde
14 Erik Hestetorvet 8, 4000 Roskilde
21 Erwin Ternevej 17, 4000 Roskilde
22 Åge Solsortevej 9, 4000 Roskilde
23 Åse Gyvelvej 45, 4000 Roskilde
26 Jørn Dronning Amaliesvej 22, 4000 Roskilde
27 Stefan Lærkevej 65, 4000 Roskilde
30 Arn Vindingevej 23, 4000 Roskilde
15 Ulla Stændertorvet 4, 4000 Roskilde
16 Yrsa Sdr. Ringvej 21, 4000 Roskilde
17 Yvonne Østre Ringvej 12, 4000 Roskilde
1 Eva Paradisvej 3, 1111 Bispeborg
5 Fy Klovnevej 87, 3333 Lilleby
6 Bi Bredgade 198, 3333 Lilleby
7 Romeo Kaerlighedstunellen 1, 4444 Borgerslev
8 Julie Kaerlighedstunellen 2, 4444 Borgerslev
9 Godzilla Dommervænget 16A, 4000 Roskilde
10 KingKong Hyrdevænget 38, 4000 Roskilde
11 KongHans Algade 10, 4000 Roskilde
12 Hans Vikingevej 45, 4000 Roskilde
13 Poul Domkirkevej 12, 4000 Roskilde
17 Yvonne Østre Ringvej 12, 4000 Roskilde
18 Tim Ringstedgade 33, 4000 Roskilde
19 Sten Ringstedvej 23, 4000 Roskilde
20 Erland Skovbovængets alle 3, 4000 Roskilde
21 Erwin Ternevej 17, 4000 Roskilde
22 Åge Solsortevej 9, 4000 Roskilde
23 Åse Gyvelvej 45, 4000 Roskilde
24 Frede Københavnsvej 25, 4000 Roskilde
25 Palle Sct Ohlsgade 10, 4000 Roskilde";
            Assert.IsNotNull(q);
            Assert.IsNotNull(val);
            string res = GetPropertyListString(q, db.Guests);
            Assert.AreEqual(val, res);
        }

        public static string GetPropertyListString(object obj, object m)
        {
            var sb = new StringBuilder();
            Type modelType = m.GetType().GetGenericArguments()[0];
            foreach (var i in obj as IQueryable)
            {
                foreach (var p in modelType.GetProperties())
                {
                    if (!p.GetAccessors()[0].IsVirtual) {
                        sb.Append($"{p.GetValue(i, null)} ");
                    }
                }
                sb.Remove(sb.Length - 1, 1); // remove the last space
                sb.Append('\n');
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
    }
}
