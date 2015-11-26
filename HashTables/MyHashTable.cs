using System;
using System.Collections;
using System.Collections.Generic;

namespace HashTables {
    class MyHashTable<TKey, TValue> : IDictionary<TKey, TValue> {

        public int size = 7;
        public LinkedList<KeyValuePair<TKey, TValue>>[] baseArray;
        public int length;

        public MyHashTable( int s ) {
            this.size = s;
            baseArray = new LinkedList<KeyValuePair<TKey,TValue>>[size];
        }

        public MyHashTable() {
            baseArray = new LinkedList<KeyValuePair<TKey,TValue>>[size];
        }

        public TValue this[TKey key] {
            get {
                int index = key.GetHashCode()%size;
                if ( baseArray[index] == null ) {
                    throw new KeyNotFoundException();
                }
                foreach( KeyValuePair<TKey, TValue> pair in baseArray[index] ) {
                    if ( pair.Key.Equals( key ) ) {
                        return pair.Value;
                    }
                }
                throw new KeyNotFoundException();
            }
            set {
                int index = key.GetHashCode()%size;
                if ( baseArray[index] == null ) {
                    baseArray[index] = new LinkedList<KeyValuePair<TKey,TValue>>();
                    baseArray[index].AddFirst( new KeyValuePair<TKey, TValue>( key, value ) );
                    length++;
                    CheckResize();
                    return;
                }
                LinkedListNode<KeyValuePair<TKey,TValue>> runner = baseArray[index].First;
                while( runner != null ) {
                    KeyValuePair<TKey,TValue> pair = runner.Value;
                    if ( pair.Key.Equals( key ) ) {
                        runner.Value = new KeyValuePair<TKey,TValue>( key, value );
                        return;
                    }
                    runner = runner.Next;
                }
                baseArray[index].AddFirst( new KeyValuePair<TKey, TValue>( key, value ) );
                length++;
                CheckResize();
            }
        }

        public int Count {
            get {
                return length;
            }
        }

        public bool IsReadOnly {
            get {
                return false;
            }
        }

        public ICollection<TKey> Keys {
            get {
                LinkedList<TKey> keys = new LinkedList<TKey>();
                foreach( KeyValuePair<TKey, TValue> pair in this ) {
                    keys.AddFirst( pair.Key );
                }
                return keys;
            }
        }

        public ICollection<TValue> Values {
            get {
                LinkedList<TValue> values = new LinkedList<TValue>();
                foreach( KeyValuePair<TKey, TValue> pair in this ) {
                    values.AddFirst( pair.Value );
                }
                return values;
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item) {
            Add( item.Key, item.Value );
        }

        public void Add(TKey key, TValue value) {
            int index = key.GetHashCode()%size;
            if ( baseArray[index] == null ) {
                baseArray[index] = new LinkedList<KeyValuePair<TKey,TValue>>();
                baseArray[index].AddFirst( new KeyValuePair<TKey, TValue>( key, value ) );
                length++;
                CheckResize();
                return;
            }
            foreach( KeyValuePair<TKey, TValue> pair in baseArray[index] ) {
                if ( pair.Key.Equals( key ) ) {
                    throw new ArgumentException();
                }
            }
            baseArray[index].AddFirst( new KeyValuePair<TKey, TValue>( key, value ) );
            length++;
            CheckResize();
        }

        public void Clear() {
            baseArray = new LinkedList<KeyValuePair<TKey,TValue>>[size];
        }

        public bool Contains(KeyValuePair<TKey, TValue> item) {
            int index = item.Key.GetHashCode()%size;
            if ( baseArray[index] == null ) {
                return false;
            }
            foreach( KeyValuePair<TKey, TValue> pair in baseArray[index] ) {
                if ( pair.Key.Equals( item.Key ) && pair.Value.Equals( item.Value ) ) {
                    return true;
                }
            }
            return false;
        }

        public bool ContainsKey(TKey key) {
            int index = key.GetHashCode()%size;
            if ( baseArray[index] == null ) {
                return false;
            }
            foreach( KeyValuePair<TKey, TValue> pair in baseArray[index] ) {
                if ( pair.Key.Equals( key ) ) {
                    return true;
                }
            }
            return false;
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int index) {
            if ( baseArray[index] == null ) {
                baseArray[index] = new LinkedList<KeyValuePair<TKey,TValue>>();
            }
            foreach( KeyValuePair<TKey, TValue> pair in array ) {
                baseArray[index].AddFirst( pair );
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() {
            foreach( LinkedList<KeyValuePair<TKey, TValue>> array in baseArray ) {
                if ( array == null ) {
                    continue;
                }
                foreach( KeyValuePair<TKey, TValue> pair in array ) {
                    yield return pair;
                }
            }
        }

        public bool Remove(KeyValuePair<TKey, TValue> item) {
            int index = item.Key.GetHashCode()%size;
            if ( baseArray[index] == null ) {
                return false;
            }
            baseArray[index].Remove( item );
            length--;
            return true;
        }

        public bool Remove(TKey key) {
            int index = key.GetHashCode()%size;
            if ( baseArray[index] == null ) {
                return false;
            }
            foreach( KeyValuePair<TKey, TValue> pair in baseArray[index] ) {
                if ( pair.Key.Equals( key ) ) {
                    baseArray[index].Remove( pair );
                    length--;
                }
            }
            return true;
        }

        public bool TryGetValue(TKey key, out TValue value) {
            value = default(TValue);
            int index = key.GetHashCode()%size;
            if ( baseArray[index] == null ) {
                return false;
            }
            foreach( KeyValuePair<TKey, TValue> pair in baseArray[index] ) {
                if ( pair.Key.Equals( key ) ) {
                    value = pair.Value;
                    return true;
                }
            }
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        private void CheckResize() {
            if ( length / size < 0.5 ) {
                return;
            }
            MyHashTable<TKey,TValue> table = new MyHashTable<TKey,TValue>(FindNextPrime( size ));
            foreach( KeyValuePair<TKey, TValue> pair in this ) {
                table.Add( pair );
            }
            baseArray = table.baseArray;
            size = table.size;
        }

        private bool IsPrime( int num ) {
            for ( int i=2;i<9;i++ ) {
                if ( num%i == 0 ) {
                    return false;
                }
            }
            return true;
        }

        private int FindNextPrime( int num ) {
            num++;
            while( !IsPrime( num ) ) {
                num++;
            }
            return num;
        }
    }
}
