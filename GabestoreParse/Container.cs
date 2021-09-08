using AngleSharp.Dom;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GabestoreParse
{
    static class Container
    {
        public const int CATALOG_PAGE_NUMBER = 118;
        public static List<IElement> Links = new List<IElement>();
        public static List<Game> Games = new List<Game>();
    }
}
