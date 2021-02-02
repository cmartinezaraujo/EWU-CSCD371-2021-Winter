using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSet.Writer
{
    public class SetWriter : IDisposable
    {

        private StreamWriter? streamWriter;
       
        private bool _DisposedValue;

        public SetWriter(string file)
        {
            if(file is null)
            {
                throw new ArgumentNullException(nameof(SetWriter));
            }

            streamWriter = new(file);
        }

        public void writeSet(NumSet set)
        {
            if(set is null)
            {
                throw new ArgumentNullException(nameof(SetWriter));
            }

            streamWriter!.WriteLine(set.ToString());
        }

        private void Dispose(bool disposing)
        {
            if (!_DisposedValue)
            {
                if (disposing)
                {
                    streamWriter!.Dispose();
                }

                _DisposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
