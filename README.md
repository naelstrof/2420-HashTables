#7-Hashtables

**Due Wednesday** by 11:59pm    **Points** 10  **Submitting** a file upload  **File Types** zip

[video](https://utah.instructure.com/courses/351899/files/51787790/download?wrap=1)

80% 

Implement the built-in System.Collections.Generic.IDictionary interface. I stubbed it out for you at the bottom of this document.

Notice baseArray's type of LinkedList<KeyValuePair<TKey, TValue>>[]. It's an array of LinkedLists, each list holds instances of the built-in KeyValuePair type. KeyValuePair is a simple type that stores the key and the value.

90%

Pay attention to your load factor. If it hits 50%, expand the size of your base array. Don't double its size. Come up with something more intelligent. Prime numbers would be awesome, but not required. :)

100%

Thoroughly test all functionality of your Dictionary. What edge cases exists? Test for them too.
