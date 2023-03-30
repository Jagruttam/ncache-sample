using Alachisoft.NCache.Client;
using BusinessModel.Models.FramesDashboard;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MerchReport
{
   public class Program
    {
        public  ICache cache;
      
        public static void Main(string[] args)
        {
            var Watch = Stopwatch.StartNew();
            var MainWatch = Stopwatch.StartNew();
            var query = $"SELECT * FROM  {typeof(MerchReportInventoyModel).FullName} WHERE StoreNumber IN (1333,1337,1338,1309,1178,1118,1036,1188,1275,1223,1204,1008,1038,1279,1294,1133,1319,1258,1287,1339,1068,1288,1228,1154,1353,1189,1335,1191,1063,1009,1167,1114,1040,1329,1138,1004,1032,1340,1196,1256,1332,1081,1144,1346,1347,1284,1224,1007,1277,1080,1227,1342,1330,1320,1229,1226,1049,1242,1316,1173,1302,1311,1312,1313,1357,1089,1069,1328,1170,1253,1239,1303,1321,1141,1314,1244,1245,1139,1267,1269,1268,1327,1317,1274,1272,1273,1271,1283,1286,1023,1221,1153,1184,1064,1126,1010,1304,1151,1205,1140,1156,1175,1143,1011,1193,1006,1119,1057,1356,1343,1199,1310,1232,1233,1231,1254,1058,1033,1150,1065,1117,1026,1120,1125,1158,1220,1041,1027,1042,1043,1044,1225,1070,1262,1087,1295,1281,1005,1261,1183,1061,1121,1278,1206,1325,1234,1276,1185,1131,1045,1203,1088,1029,1243,1155,1251,1248,1250,1247,1297,1071,1197,1034,1358,1123,1210,1366,1331,1130,1127,1062,1145,1159,1306,1305,1307,1298,1300,1299,1323,1324,1115,1116,1067,1168,1166,1169,1354,1148,1230,1012,1084,1090,1241,1085,1341,1212,1222,1086,1030,1048,1194,1092,1282,1165,1201,1202,1289,1192,1132,1285,1190,1013,1031,1083,1344,1252,1072,1318,1259,1149,1200,1270,1322,1147,1334,1035,1219,1315,1177,1093,1174,1082,1073,1214,1213,1124,1326,1291,1292,1146,1246) AND FullDate_CY = DateTime('12/31/2022 12:00:00 AM')";

            // Connect to cache
            Console.WriteLine("Connecting to cache...");
            var cache = CacheManager.GetCache("MerchReportHistorical_Dev");

            var queryCommand = new QueryCommand(query);
            ICacheReader reader = cache.SearchService.ExecuteReader(queryCommand, false);
            var keys = new List<string>();
            Console.WriteLine("Fetching Keys...");
            while (reader.Read())
            {
                keys.Add(reader.GetValue<string>(0));
            }
            Console.WriteLine("Time to fetch keys = " + $"{Watch.ElapsedMilliseconds}ms");
            Watch.Restart();
            Console.WriteLine("Fetching Values from NCache...");
            var typeName = typeof(MerchReportInventoyModel).Name;
            var bulkSize = 10000;
            var data = new List<MerchReportInventoyModel>();
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < keys.Count; i += bulkSize)
            {


                var bulkKeys = keys.Skip(i).Take(bulkSize);
                tasks.Add(Task.Run(() =>
                {
                    var fetchedRecords = cache.GetBulk<MerchReportInventoyModel>(bulkKeys).Select(x => x.Value);
                    lock (data) data.AddRange(fetchedRecords);
                }
                ));


            }
           
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine("Time to fetch values = " + $"{Watch.ElapsedMilliseconds}ms");
            Watch.Stop();
            Console.WriteLine("Total Time taken = " + $"{MainWatch.ElapsedMilliseconds}ms");
            Watch.Stop();
            var totalRows = data.Count;
        }
    }
}
