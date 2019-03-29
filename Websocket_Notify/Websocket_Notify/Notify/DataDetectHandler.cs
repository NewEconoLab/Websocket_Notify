using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Websocket_Notify.Notify
{
    /// <summary>
    /// 
    /// 数据监测处理器
    /// 
    /// </summary>
    public class DataDetectHandler
    {
        /// <summary>
        /// 
        /// 数据变动监测
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool HasChanged(string network, bool first, out string message)
        {
            var a = dictInfo.GetValueOrDefault(network);
            if (a == null)
            {
                a = new A
                {
                    network = network,
                    blockHeight = 0,
                    blockTime = 0,
                    blockHash = ""
                };
            }
            bool flag = false;
            var res = new JObject() { { "network", network } };
            var newdata = getBlockAndNotifyCount(network);
            var blockHeight = long.Parse(newdata["index"].ToString());
            var blockTime = long.Parse(newdata["time"].ToString());
            var blockHash = newdata["hash"].ToString();
            if(a.blockHeight < blockHeight || first)
            {
                res.Add("blockHeight", blockHeight);
                res.Add("blockTime", blockTime);
                res.Add("blockHash", blockHash);
                res.Add("blockInsertTime:", TimeHelper.toTimeStamp(new ObjectId(newdata["_id"]["$oid"].ToString()).CreationTime));
                res.Add("svrSystemTime:", TimeHelper.toTimeStamp(System.DateTime.UtcNow));
                res.Add("tx", newdata["tx"]);
                a.blockHeight = blockHeight;
                a.blockTime = blockTime;
                a.blockHash = blockHash;
                flag = true;
            } else
            {
                flag = false;
            }
            dictInfo.Remove(network);
            dictInfo.Add(network, a);

            //message = res.ToString();
            message = Newtonsoft.Json.JsonConvert.SerializeObject(res);
            return flag;
        }
        
        private class A
        {
            public string network { get; set; }
            public long blockHeight { get; set; }
            public long blockTime { get; set; }
            public string blockHash { get; set; }
            public string[] txids;
        }
        private Dictionary<string, A> dictInfo = new Dictionary<string, A>();
        private JObject getBlockAndNotifyCount(string network)
        {
            bool flag = network == "mainnet";
            string mongodbConnStr =  flag ? WsConst.block_mongodbConnStr_mainnet : WsConst.block_mongodbConnStr_testnet;
            string mongodbDatabase = flag ? WsConst.block_mongodbDatabase_mainnet : WsConst.block_mongodbDatabase_testnet;

            //
            var client = new MongoClient(mongodbConnStr);
            var database = client.GetDatabase(mongodbDatabase);
            var collection = database.GetCollection<BsonDocument>("system_counter");
            string findStr = new JObject() { { "counter", "block" } }.ToString();
            var res = collection.Find(findStr).ToList();
            //
            var index = (int)res[0]["lastBlockindex"];
            findStr = new JObject() { {"index", index } }.ToString();
            string fieldStr = new JObject() { {"index",1 }, { "time", 1 }, { "hash", 1 },{ "tx.txid",1}}.ToString();
            collection = database.GetCollection<BsonDocument>("block");
            res = collection.Find(findStr).Project(fieldStr).ToList();
            //
            var jsonWriterSettings = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict };
            JArray JA = JArray.Parse(res.ToJson(jsonWriterSettings));
            return (JObject)JA[0];
        }
        
    }
    
}
