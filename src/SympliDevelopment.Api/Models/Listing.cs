using System;
using System.Text.RegularExpressions;

namespace SympliDevelopment.Api.Models
{
    public class Listings
    {
        private List<Uri> uriList;
        private readonly string hostToFind;
        public Listings(string HostToFind)
        {
            uriList = new List<Uri>();
            hostToFind = HostToFind;
        }

        public void AddUri(string Uri)
        {
            // allows 'silent failure'
            if (CheckUri(Uri))
            {
                Uri uri = new Uri(Uri);
                uriList.Add(uri);
            }
        }
        private bool CheckUri(string Uri)
        {
            return System.Uri.IsWellFormedUriString(Uri, UriKind.RelativeOrAbsolute);
        }
        public string GetUriIndexes()
        {
            string indexString = String.Empty;

            List<string> indexList = new List<string>();
            string[] indexArray;

            if (uriList.Where(uri => uri.Host == hostToFind).Count() == 0)
            {
                indexString = "0";
            }
            else
            {
                for (int i = 0; i < uriList.Count(); i++)
                {
                    if (uriList[i].Host == hostToFind)
                    {
                        indexList.Add((i + 1).ToString()); // indexer + 1 is the search engine listing rank
                    }
                }

                indexArray = indexList.ToArray();
                indexString = String.Join(", ", indexArray);
            }
            return indexString;
        }
        public string GetListingsCount()
        {
            return uriList.Count().ToString();
        }
    }
}
