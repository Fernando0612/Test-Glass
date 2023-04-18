using Glass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Text.RegularExpressions;

namespace Glass.Helpers
{
    public class DocumentHelper
    {
        public static List<Document> SearchDocuments(string query, bool matchAll)
        {
            return matchAll ? MathAll(query) : MathAny(query);

        }

        public static List<Document> MathAny(string query)
        {
            List<Document> allDocuments = GetAllDocuments();
            List<Document> documents = new List<Document>();
            string[] words = (Regex.Replace(query, @"[^a-zA-Z0-9% ._]", string.Empty).ToLower()).Split(' ');

            foreach (Document doc in allDocuments)
            {

                if (words.Any(w => Regex.Replace(doc.Content, @"[^a-zA-Z0-9% ._]", string.Empty).ToLower().Contains(w)))
                    documents.Add(doc);
            }

            return documents;

        }

        public static List<Document> MathAll(string query)
        {
            List<Document> allDocuments = GetAllDocuments();
            List<Document> documents = new List<Document>();
            string[] words = (Regex.Replace(query, @"[^a-zA-Z0-9% ._]", string.Empty).ToLower()).Split(' ');

            foreach(Document doc in allDocuments){

                if (words.All(w => Regex.Replace(doc.Content, @"[^a-zA-Z0-9% ._]", string.Empty).ToLower().Contains(w)))
                    documents.Add(doc);
            }

            return documents;

        }

        public static List<Document> GetAllDocuments()
        {
            List<Document> documents = new List<Document>();

            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);

            string commandText = "SELECT * FROM DOCUMENTS";

            con.Open();

            using (var Cmd = new OracleCommand(commandText, con))
            {

                using (var Reader = Cmd.ExecuteReader())
                {
                    if (Reader.HasRows)
                    {
                        while (Reader.Read())
                        {

                            documents.Add(new Document()
                            {
                                Id = Convert.ToInt32(Reader["Id"].ToString()),
                                Title = Reader["Title"].ToString(),
                                Content = Reader["Content"].ToString()
                            }); ;
                        }
                    }
                }

            }

            con.Close();

            return documents;

        }

    }
}