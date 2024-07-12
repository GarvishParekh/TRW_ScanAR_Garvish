using System;

[Serializable]
public class TrwData 
{
    public string android_version;
    public string ios_version;
    public BookData[] book_data;
}

[Serializable]
public class BookData
{
    public int bookId;
    public string bookName;
    public PageData[] page;
}

[Serializable]
public class PageData
{
    public int pageId;
    public string pageName;
    public TracksData[] track;
}

[Serializable]
public class TracksData
{
    public int trackId;
    public string url;
}
