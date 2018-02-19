using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TankGame.Persistence
{
    public class BinaryPersistence : IPersistence
    {
        public string Extension
        {
            get { return ".bin"; }
        }

        public string FilePath { get; private set; }

        public BinaryPersistence(string path)
        {
            FilePath = path + Extension;
        }

        public void Save<T>(T data)
        {
            using (FileStream stream = File.OpenWrite(FilePath))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(stream, data);
                // Calling stream.Close() not necessary when using 'using'.
                stream.Close();
            }
        }

        public T Load<T>()
        {
            T data = default(T);

            if (File.Exists(FilePath))
            {
                // If not using 'using', stream must be closed correctly.
                // The finally block makes sure to do that in every case.
                FileStream stream = File.OpenRead(FilePath);
                try
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    data = (T)bf.Deserialize(stream);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
                finally
                {
                    stream.Close();
                }
            }

            return data;
        }
    }
}
