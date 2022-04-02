namespace prjList
{
    class MyList
    {
        public const int conNull = -1;
        public struct SItem
        {
            public string Value;
            public int Next;

            public SItem(string newValue, int newNext)
            {
                Value = newValue;
                Next = newNext;

            }
        }
        public SItem[] arrSpace;
        private int intFree = 1;
        private int size;

        public MyList(int NewSize)
        {
            arrSpace = new SItem[NewSize];
            size = NewSize;
            intFree = 0;

            for (int c = 0; c < NewSize - 1; c++)
            {
                arrSpace[c].Next = c + 1;
            }

            arrSpace[NewSize - 1].Next = conNull;
        }


        public void Add(SItem item)
        {
            arrSpace[intFree++] = item;
            arrSpace[intFree].Next = intFree;
        }

        public int Length()
        {
            return size;
        }

    }
}
