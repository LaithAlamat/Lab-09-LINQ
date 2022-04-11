using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Lab09_LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            JSON();
        }
        static void JSON()
        {
           
            string path = @"../../../../data.json";
            string jsonToString = File.ReadAllText(path);

            
            Root root = JsonConvert.DeserializeObject<Root>(jsonToString);

            AllNeighborhoods(root);
            FilteredNeighborhoods(root);
            FilterNeighborhoodsWithNoName(root);
            NoDuplicates(root);
            ConsolidateQueries(root);
            OpposingMethod(root);

        }
        public static void AllNeighborhoods(Root root)
        {
            int count = 1;
            foreach (Feature feature in root.features)
            {
                Console.WriteLine($"{count}, {feature.properties.neighborhood}");
                count++;
            }
        }
        public static void FilteredNeighborhoods(Root root)
        {
            int count = 1;
            var query = root.features
                        .Select(feature => new { feature.properties.neighborhood });

            foreach (var feature in query)
            {
                Console.WriteLine($"Neighborhood: {count}, {feature}");
                count++;
            }

        }
        public static void FilterNeighborhoodsWithNoName(Root root)
        {
            int count = 1;
            var query = from feature in root.features
                        where feature.properties.neighborhood != ""
                        select feature.properties.neighborhood;

            foreach (var feature in query)
            {
                Console.WriteLine($"Neighborhood with name: {count}, {feature}");
                count++;
            }

        }
        public static void NoDuplicates(Root root)
        {
            int count = 1;
            var query = from feature in root.features
                        where feature.properties.neighborhood != ""
                        select feature.properties.neighborhood;

            var removeDuplicates = query.Distinct(); 

            foreach (var feature in removeDuplicates)
            {
                Console.WriteLine($"Distinct Neighborhood: {count}, {feature}");
                count++;
            }

        }
        public static void ConsolidateQueries(Root root)
        {
            int count = 1;
            var query = (from feature in root.features
                         where (feature.properties.neighborhood != "")
                         select (feature.properties.neighborhood)).Distinct();

            foreach (var feature in query)
            {
                Console.WriteLine($"Consolidated Queries:  {count}, {feature}");
                count++;
            }
        }

        public static void OpposingMethod(Root root)
        {
            int count = 1;
            //var query = from feature in root.features
            //            where feature.properties.neighborhood != ""
            //            select feature.properties.neighborhood;
            var query = root.features
                .Select(x => new { x.properties.neighborhood })
                .Where(x => x.neighborhood != "")
                .Distinct();

            foreach (var feature in query)
            {
                Console.WriteLine($"Filtered Neighborhoods Opposing Method: {count}, {feature}");
                count++;
            }

        }
        public class Root
        {
            public string type { get; set; }
            public List<Feature> features { get; set; }

        }

        public class Feature
        {
            public string type { get; set; }
            public Geometry geometry { get; set; }
            public Properties properties { get; set; }

        }

        public class Geometry
        {
            public string type { get; set; }
            public List<double> coordinates { get; set; }
        }

        public class Properties
        {
            public string zip { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string address { get; set; }
            public string borough { get; set; }
            public string neighborhood { get; set; }
            public string county { get; set; }
        }
    }
}
