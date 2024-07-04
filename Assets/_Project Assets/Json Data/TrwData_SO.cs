using System;

[Serializable]
public class TrwData 
{
    public string androidVersion;
    public string iosVersion;

    public BookData[] bookData;
}

[Serializable]
public class BookData
{
    public int bookId;
    public PageData[] page;
}

[Serializable]
public class PageData
{
    public int pageId;
    public TracksData[] track;
}

[Serializable]
public class TracksData
{
    public int trackNumber;
    public string url;
}
