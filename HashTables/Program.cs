using System;

namespace HashTables {
    class MainClass {
        public static void Main (string[] args) {
            MyHashTable<string, float> blah = new MyHashTable<string,float>();
            blah["abcd"] = 8457;
            blah["nael"] = 2139;
            blah["face"] = 3849;
            blah["foob"] = 6749;
            blah["food"] = 9345;
            blah["uido"] = 4798;
            blah["mdjt"] = 2847;
            blah["andu"] = 8472;
            blah["ifos"] = 2759;
            blah["pael"] = 2284;
            blah["urke"] = 2785;
            blah["aheu"] = 3758;
            Console.WriteLine (blah ["food"]);
        }
    }
}
