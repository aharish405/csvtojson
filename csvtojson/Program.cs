using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Text.Json;
using CsvHelper;

namespace CsvToJsonConverter
{
    public class Address
    {
        public string addressLine1 { get; set; }
        public string city { get; set; }
        public string stateCode { get; set; }
        public string country { get; set; }
        public string zipCode { get; set; }
    }

    public class Entity
    {
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string tinNumbez { get; set; }
        public string dob { get; set; }
        public string entityType { get; set; }
        public Address address { get; set; }
    }

    public class Record
    {
        public string referenceld { get; set; }
        public string sequenceId { get; set; }
        public string complianceSource { get; set; }
        public Entity entity { get; set; }
    }

    public class CsvRow
    {
        public string referenceld { get; set; }
        public string sequenceId { get; set; }
        public string complianceSource { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string tinNumbez { get; set; }
        public string dob { get; set; }
        public string entityType { get; set; }
        public string addressLine1 { get; set; }
        public string city { get; set; }
        public string stateCode { get; set; }
        public string country { get; set; }
        public string zipCode { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string csvFilePath = "input.csv"; // Change to your CSV file path
            string outputFilePath = "output.txt"; // Output text file

            try
            {
                using var reader = new StreamReader(csvFilePath);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                var rows = csv.GetRecords<CsvRow>();

                var resultList = new List<Record>();

                foreach (var row in rows)
                {
                    var record = new Record
                    {
                        referenceld = row.referenceld,
                        sequenceId = row.sequenceId,
                        complianceSource = row.complianceSource,
                        entity = new Entity
                        {
                            firstName = row.firstName,
                            middleName = row.middleName,
                            lastName = row.lastName,
                            tinNumbez = row.tinNumbez,
                            dob = row.dob,
                            entityType = row.entityType,
                            address = new Address
                            {
                                addressLine1 = row.addressLine1,
                                city = row.city,
                                stateCode = row.stateCode,
                                country = row.country,
                                zipCode = row.zipCode
                            }
                        }
                    };
                    resultList.Add(record);
                }

                var jsonOptions = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                string jsonOutput = JsonSerializer.Serialize(resultList, jsonOptions);
                File.WriteAllText(outputFilePath, jsonOutput);

                Console.WriteLine("JSON output written to: " + outputFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
