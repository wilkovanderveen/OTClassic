using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater.Common.Measuring.Text
{
    public enum SplitAction
    {
        Remove,
        Add,
        NextLine
    }

    public class SplitCharacter
    {
        public char Character;
        public SplitAction Action;
    }
}