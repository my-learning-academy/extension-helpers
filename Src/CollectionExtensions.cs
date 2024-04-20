namespace extension.helpers
{
    public static partial class CollectionExtensions
    {
        /*
             <summary>
                Returns a portion of the list, from the 'start' index up to, but not including, the 'end' index
             </summary>
             <param name="sourceList">The list to slice.</param>
             <param name="start">The starting index of the slice. 
                    If negative, it represents an index counting from the end of the list. 
                    Default is 0.
            </param>
            <param name="end">The ending index of the slice. 
                    If negative, it represents an index counting from the end of the list. 
                    Default is the length of the list.
            </param>
             <returns>A new list containing the sliced portion of the original list. 
                    If the original list is null or empty, an empty list is returned
            </returns>
        */
        public static List<T> Slice<T>(this List<T> sourceList, int start = 0, int? end = null)
        {
            if (sourceList is null || sourceList.Count is 0)
            {
                return [];
            }

            int length = sourceList.Count;
            end ??= length;

            start = start < 0 ? Math.Max(length + start, 0) : Math.Min(start, length);
            end = end < 0 ? Math.Max(length + end.Value, 0) : Math.Min(end.Value, length);

            return sourceList.GetRange(start, end.Value - start);
        }

        /*
            <summary>
                returns a new list containing the first 'takeElements' elements of the source list
            </summary>
             <param name="sourceList">The list from which to take elements.</param>
            <param name="takeElements">The number of elements to take from the start of the list. 
                Default is 1. If less than 0, an empty list is returned.
            </param>
            <param name="end">The ending index of the slice. 
                    If negative, it represents an index counting from the end of the list. 
                    Default is the length of the list.
            </param>
             <returns>
                A new list containing the first 'takeElements' elements of the source list. 
                If the source list is null or empty, an empty list is returned
            </returns>
        */
        public static List<T> TakeLeft<T>(this List<T> sourceList, int takeElements = 1)
        {
            if (sourceList is null || sourceList.Count is 0)
            {
                return [];
            }
            return Slice(sourceList, 0, takeElements < 0 ? 0 : takeElements);
        }
    }
}
