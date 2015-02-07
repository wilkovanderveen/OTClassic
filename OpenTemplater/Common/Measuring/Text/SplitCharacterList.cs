using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater.Common.Measuring.Text
{
    public class SplitCharacterList
    {
        private List<SplitCharacter> _splitCharacters;

        public SplitCharacterList()
        {
            _splitCharacters = new List<SplitCharacter>();
        }

        public void Add(char character, SplitAction splitAction)
        {
            SplitCharacter splitCharacter = new SplitCharacter();
            splitCharacter.Character = character;
            splitCharacter.Action = splitAction;
            _splitCharacters.Add(splitCharacter);
        }

        public override string ToString()
        {
            String characterArray = "";
            foreach (SplitCharacter splitCharacter in _splitCharacters)
            {
                characterArray += splitCharacter.Character;
            }
            return characterArray;
        }

        public char[] ToArray()
        {
            String characterArray = "";
            foreach (SplitCharacter splitCharacter in _splitCharacters)
            {
                characterArray += splitCharacter.Character;
            }
            return characterArray.ToCharArray();
        }
    }
}