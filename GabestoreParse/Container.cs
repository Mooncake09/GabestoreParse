using AngleSharp.Dom;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GabestoreParse
{
    class Container
    {
        public List<AngleSharp.Dom.IElement> Links { get; set; }
        public List<Game> Games { get; set; }
    }
}
