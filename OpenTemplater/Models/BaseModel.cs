using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTemplater.Models
{
    public abstract class BaseModel
    {
        private string _key;

        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        protected BaseModel(string key)
        {
            _key = key;
        }
    }
}
