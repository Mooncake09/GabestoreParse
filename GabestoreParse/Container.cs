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
        public const int PAGE_NUMBERS = 118;
        public static List<IElement> Links = new List<IElement>();
        public static Game[] Games = new Game[Links.Count]; 
    }
}
