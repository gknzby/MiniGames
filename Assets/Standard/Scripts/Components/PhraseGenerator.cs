using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gknzby.Components
{
    public struct PhraseGenerator
    {
        public string Phrase;
        public PhraseGenerator(string Phrase)
        {
            this.Phrase = Phrase;
        }
        public PhraseGenerator(System.Enum en)
        {
            this.Phrase = en.ToString();
        }
        public static PhraseGenerator operator +(PhraseGenerator pg, string str)
        {
            pg.Phrase += str;
            return pg;
        }
        public static PhraseGenerator operator +(string str, PhraseGenerator pg)
        {
            str += pg.Phrase;
            pg.Phrase = str;
            return pg;
        }
        public static PhraseGenerator operator +(PhraseGenerator pg, System.Enum en)
        {
            return pg + en.ToString();
        }
        public static PhraseGenerator operator +(System.Enum en, PhraseGenerator pg)
        {
            return en.ToString() + pg;
        }
        public override string ToString()
        {
            return Phrase;
        }
    }
}

