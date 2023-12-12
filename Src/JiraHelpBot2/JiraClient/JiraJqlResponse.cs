namespace JiraHelpBot2.JiraClient;

public class Rootobject
{
    //public string expand { get; set; }
    //public int startAt { get; set; }
    //public int maxResults { get; set; }
    //public int total { get; set; }
    public Issue[] issues { get; set; }
}

public class Issue
{
    public string expand { get; set; }

    public string id { get; set; }

    public string self { get; set; }

    public string key { get; set; }

    public Fields fields { get; set; }
}

public class Fields
{
    //public object customfield_10070 { get; set; }
    //public object customfield_10071 { get; set; }
    //public object customfield_13582 { get; set; }
    //public object customfield_10072 { get; set; }
    //public object customfield_10073 { get; set; }
    //public object customfield_13581 { get; set; }
    //public object customfield_13100 { get; set; }
    //public object customfield_13584 { get; set; }
    //public object customfield_13583 { get; set; }
    //public object customfield_13586 { get; set; }
    //public object customfield_13102 { get; set; }
    //public object customfield_13585 { get; set; }
    //public object customfield_13101 { get; set; }
    //public object customfield_13588 { get; set; }
    //public object customfield_13104 { get; set; }
    //public object customfield_13103 { get; set; }
    //public object customfield_13587 { get; set; }
    //public Resolution resolution { get; set; }
    //public object customfield_13106 { get; set; }
    //public object customfield_13589 { get; set; }
    //public object customfield_13105 { get; set; }
    //public object customfield_12802 { get; set; }
    //public object customfield_12801 { get; set; }
    //public object customfield_12803 { get; set; }
    //public object lastViewed { get; set; }
    //public object customfield_13571 { get; set; }
    //public object customfield_13570 { get; set; }
    //public object customfield_12000 { get; set; }
    //public object customfield_13573 { get; set; }
    //public object customfield_13572 { get; set; }
    //public object customfield_13575 { get; set; }
    //public object customfield_13576 { get; set; }
    //public object customfield_13579 { get; set; }
    //public object customfield_13578 { get; set; }
    public object[] labels { get; set; }

    //public Customfield_11820 customfield_11820 { get; set; }
    //public object customfield_13569 { get; set; }
    //public int aggregatetimeoriginalestimate { get; set; }
    //public Issuelink[] issuelinks { get; set; }
    public Assignee assignee { get; set; }

    //public Component[] components { get; set; }
    //public object customfield_13560 { get; set; }
    //public string[] customfield_10050 { get; set; }
    //public object customfield_13562 { get; set; }
    //public DateTime customfield_11021 { get; set; }
    //public object customfield_13561 { get; set; }
    //public string customfield_11022 { get; set; }
    //public object customfield_13201 { get; set; }
    //public object customfield_13564 { get; set; }
    //public object customfield_13563 { get; set; }
    //public object customfield_13200 { get; set; }
    //public object customfield_13566 { get; set; }
    //public object customfield_13202 { get; set; }
    //public object customfield_13565 { get; set; }
    //public object customfield_13568 { get; set; }
    //public object customfield_13567 { get; set; }
    //public object customfield_13559 { get; set; }
    //public object customfield_13558 { get; set; }
    //public object customfield_12900 { get; set; }
    //public object[] subtasks { get; set; }
    //public object customfield_13670 { get; set; }
    //public object customfield_13672 { get; set; }
    //public object customfield_13551 { get; set; }
    //public object customfield_13550 { get; set; }
    //public object customfield_13671 { get; set; }
    //public Reporter reporter { get; set; }
    //public object customfield_12101 { get; set; }
    //public object customfield_12100 { get; set; }
    //public object customfield_13552 { get; set; }
    //public object customfield_13555 { get; set; }
    //public object customfield_12103 { get; set; }
    //public object customfield_12102 { get; set; }
    //public object customfield_13557 { get; set; }
    //public object customfield_13556 { get; set; }
    //public object customfield_12104 { get; set; }
    //public string customfield_11920 { get; set; }
    //public object customfield_13669 { get; set; }
    //public object customfield_13547 { get; set; }
    //public object customfield_13668 { get; set; }
    //public Progress progress { get; set; }
    //public Votes votes { get; set; }
    public Issuetype issuetype { get; set; }

    //public object customfield_13540 { get; set; }
    //public object customfield_13661 { get; set; }
    //public object customfield_11120 { get; set; }
    //public object customfield_13660 { get; set; }
    //public Project project { get; set; }
    //public object customfield_13300 { get; set; }
    //public object customfield_13542 { get; set; }
    //public object customfield_13662 { get; set; }
    //public object customfield_13541 { get; set; }
    //public object customfield_13301 { get; set; }
    //public object customfield_13543 { get; set; }
    //public object customfield_13667 { get; set; }
    //public string customfield_13546 { get; set; }
    //public object customfield_13537 { get; set; }
    //public object customfield_13536 { get; set; }
    //public object customfield_13657 { get; set; }
    //public object customfield_13539 { get; set; }
    //public object customfield_13538 { get; set; }
    //public DateTime resolutiondate { get; set; }
    //public Watches watches { get; set; }
    //public object customfield_13650 { get; set; }
    //public object customfield_13531 { get; set; }
    //public object customfield_13652 { get; set; }
    //public object customfield_12200 { get; set; }
    //public object customfield_13651 { get; set; }
    //public object customfield_13530 { get; set; }
    //public object customfield_13654 { get; set; }
    //public object customfield_13653 { get; set; }
    //public object customfield_13535 { get; set; }
    //public object customfield_13656 { get; set; }
    //public object customfield_13647 { get; set; }
    //public object customfield_13526 { get; set; }
    //public object[] customfield_13646 { get; set; }
    //public object customfield_13525 { get; set; }
    //public object customfield_13528 { get; set; }
    //public object customfield_13648 { get; set; }
    //public object customfield_13527 { get; set; }
    //public object customfield_13529 { get; set; }
    //public DateTime updated { get; set; }
    //public int timeoriginalestimate { get; set; }
    //public string description { get; set; }
    //public object customfield_11220 { get; set; }
    //public object customfield_13641 { get; set; }
    //public object customfield_13640 { get; set; }
    //public object[] customfield_11221 { get; set; }
    //public object customfield_11222 { get; set; }
    //public object customfield_13643 { get; set; }
    //public object customfield_13642 { get; set; }
    //public object customfield_11223 { get; set; }
    //public object customfield_13645 { get; set; }
    //public object customfield_13644 { get; set; }
    //public object customfield_13636 { get; set; }
    //public object customfield_13635 { get; set; }
    //public object customfield_13514 { get; set; }
    //public object customfield_13517 { get; set; }
    //public object customfield_13516 { get; set; }
    //public object customfield_13637 { get; set; }
    //public string customfield_10921 { get; set; }
    //public string customfield_10922 { get; set; }
    //public object customfield_13639 { get; set; }
    //public object customfield_13518 { get; set; }
    public string summary { get; set; }

    //public object customfield_13630 { get; set; }
    //public object customfield_13632 { get; set; }
    //public object customfield_13511 { get; set; }
    //public object customfield_13631 { get; set; }
    //public object customfield_13510 { get; set; }
    //public object customfield_13513 { get; set; }
    //public object customfield_13512 { get; set; }
    //public object customfield_12302 { get; set; }
    //public object customfield_11326 { get; set; }
    //public object customfield_13504 { get; set; }
    //public object customfield_13625 { get; set; }
    //public object customfield_13624 { get; set; }
    //public object customfield_13503 { get; set; }
    //public object customfield_11325 { get; set; }
    //public object customfield_11328 { get; set; }
    //public object customfield_13627 { get; set; }
    //public object environment { get; set; }
    //public object customfield_13505 { get; set; }
    //public object customfield_11327 { get; set; }
    //public object customfield_13626 { get; set; }
    //public object customfield_13508 { get; set; }
    //public object customfield_13629 { get; set; }
    //public object customfield_13507 { get; set; }
    //public object customfield_13628 { get; set; }
    //public object duedate { get; set; }
    //public object customfield_13509 { get; set; }
    //public DateTime statuscategorychangedate { get; set; }
    //public object customfield_10110 { get; set; }
    public Fixversion[] fixVersions { get; set; }

    //public object customfield_13500 { get; set; }
    //public object customfield_11321 { get; set; }
    //public object customfield_13621 { get; set; }
    //public object customfield_13620 { get; set; }
    //public object customfield_11322 { get; set; }
    //public object customfield_13502 { get; set; }
    //public object customfield_13623 { get; set; }
    //public object customfield_13622 { get; set; }
    //public object customfield_13501 { get; set; }
    //public object customfield_11324 { get; set; }
    //public object customfield_13614 { get; set; }
    //public object customfield_13613 { get; set; }
    //public object customfield_13616 { get; set; }
    //public object customfield_13615 { get; set; }
    //public object customfield_13618 { get; set; }
    //public object customfield_13617 { get; set; }
    //public object customfield_13619 { get; set; }
    //public object customfield_10220 { get; set; }
    //public object customfield_13610 { get; set; }
    public Priority priority { get; set; }

    //public object customfield_10100 { get; set; }
    //public object customfield_13612 { get; set; }
    //public object customfield_13611 { get; set; }
    //public object customfield_13603 { get; set; }
    //public object customfield_13602 { get; set; }
    //public object customfield_13605 { get; set; }
    //public object customfield_13607 { get; set; }
    //public int timeestimate { get; set; }
    //public object customfield_13606 { get; set; }
    //public object[] versions { get; set; }
    //public object customfield_13609 { get; set; }
    //public object customfield_13608 { get; set; }
    public Status status { get; set; }
    //public object customfield_13601 { get; set; }
    //public object customfield_13600 { get; set; }
    //public int aggregatetimeestimate { get; set; }
    //public Creator creator { get; set; }
    //public Aggregateprogress aggregateprogress { get; set; }
    //public object timespent { get; set; }
    //public object aggregatetimespent { get; set; }
    //public object customfield_12601 { get; set; }
    //public int workratio { get; set; }
    //public DateTime created { get; set; }
    //public object customfield_10090 { get; set; }
    //public object customfield_13001 { get; set; }
    //public object customfield_13000 { get; set; }
    //public object customfield_13002 { get; set; }
    //public object customfield_11620 { get; set; }
    //public object customfield_12701 { get; set; }
    //public object customfield_10522 { get; set; }
    //public object customfield_12700 { get; set; }
    //public object security { get; set; }
    //public object customfield_10080 { get; set; }
    //public object customfield_13591 { get; set; }
    //public object customfield_13590 { get; set; }
    //public object customfield_13593 { get; set; }
    //public object customfield_13592 { get; set; }
    //public object customfield_13595 { get; set; }
    //public object customfield_13111 { get; set; }
    //public object customfield_13110 { get; set; }
    //public object customfield_13594 { get; set; }
    //public object customfield_13597 { get; set; }
    //public object customfield_13113 { get; set; }
    //public object customfield_13112 { get; set; }
    //public object customfield_13596 { get; set; }
    //public object customfield_13599 { get; set; }
    //public object customfield_13115 { get; set; }
    //public object customfield_13114 { get; set; }
    //public object customfield_13117 { get; set; }
    //public object customfield_13116 { get; set; }
    //public object customfield_13108 { get; set; }
    //public object customfield_13107 { get; set; }
    //public object customfield_13109 { get; set; }
}

public class Resolution
{
    public string self { get; set; }

    public string id { get; set; }

    public string description { get; set; }

    public string name { get; set; }
}

public class Customfield_11820
{
    public bool hasEpicLinkFieldDependency { get; set; }

    public bool showField { get; set; }

    public Noneditablereason nonEditableReason { get; set; }
}

public class Noneditablereason
{
    public string reason { get; set; }

    public string message { get; set; }
}

public class Assignee
{
    public string self { get; set; }

    public string accountId { get; set; }

    public Avatarurls avatarUrls { get; set; }

    public string displayName { get; set; }

    public bool active { get; set; }

    public string timeZone { get; set; }

    public string accountType { get; set; }
}

public class Avatarurls
{
    public string _48x48 { get; set; }

    public string _24x24 { get; set; }

    public string _16x16 { get; set; }

    public string _32x32 { get; set; }
}

public class Reporter
{
    public string self { get; set; }

    public string accountId { get; set; }

    public Avatarurls1 avatarUrls { get; set; }

    public string displayName { get; set; }

    public bool active { get; set; }

    public string timeZone { get; set; }

    public string accountType { get; set; }
}

public class Avatarurls1
{
    public string _48x48 { get; set; }

    public string _24x24 { get; set; }

    public string _16x16 { get; set; }

    public string _32x32 { get; set; }
}

public class Progress
{
    public int progress { get; set; }

    public int total { get; set; }

    public int percent { get; set; }
}

public class Votes
{
    public string self { get; set; }

    public int votes { get; set; }

    public bool hasVoted { get; set; }
}

public class Issuetype
{
    public string self { get; set; }

    public string id { get; set; }

    public string description { get; set; }

    public string iconUrl { get; set; }

    public string name { get; set; }

    public bool subtask { get; set; }

    public int avatarId { get; set; }

    public int hierarchyLevel { get; set; }
}

public class Project
{
    public string self { get; set; }

    public string id { get; set; }

    public string key { get; set; }

    public string name { get; set; }

    public string projectTypeKey { get; set; }

    public bool simplified { get; set; }

    public Avatarurls2 avatarUrls { get; set; }
}

public class Avatarurls2
{
    public string _48x48 { get; set; }

    public string _24x24 { get; set; }

    public string _16x16 { get; set; }

    public string _32x32 { get; set; }
}

public class Watches
{
    public string self { get; set; }

    public int watchCount { get; set; }

    public bool isWatching { get; set; }
}

public class Priority
{
    public string self { get; set; }

    public string iconUrl { get; set; }

    public string name { get; set; }

    public string id { get; set; }
}

public class Status
{
    public string self { get; set; }

    public string description { get; set; }

    public string iconUrl { get; set; }

    public string name { get; set; }

    public string id { get; set; }

    public Statuscategory statusCategory { get; set; }
}

public class Statuscategory
{
    public string self { get; set; }

    public int id { get; set; }

    public string key { get; set; }

    public string colorName { get; set; }

    public string name { get; set; }
}

public class Creator
{
    public string self { get; set; }

    public string accountId { get; set; }

    public Avatarurls3 avatarUrls { get; set; }

    public string displayName { get; set; }

    public bool active { get; set; }

    public string timeZone { get; set; }

    public string accountType { get; set; }
}

public class Avatarurls3
{
    public string _48x48 { get; set; }

    public string _24x24 { get; set; }

    public string _16x16 { get; set; }

    public string _32x32 { get; set; }
}

public class Aggregateprogress
{
    public int progress { get; set; }

    public int total { get; set; }

    public int percent { get; set; }
}

public class Issuelink
{
    public string id { get; set; }

    public string self { get; set; }

    public Type type { get; set; }

    public Outwardissue outwardIssue { get; set; }
}

public class Type
{
    public string id { get; set; }

    public string name { get; set; }

    public string inward { get; set; }

    public string outward { get; set; }

    public string self { get; set; }
}

public class Outwardissue
{
    public string id { get; set; }

    public string key { get; set; }

    public string self { get; set; }

    public Fields1 fields { get; set; }
}

public class Fields1
{
    public string summary { get; set; }

    public Status1 status { get; set; }

    public Priority1 priority { get; set; }

    public Issuetype1 issuetype { get; set; }
}

public class Status1
{
    public string self { get; set; }

    public string description { get; set; }

    public string iconUrl { get; set; }

    public string name { get; set; }

    public string id { get; set; }

    public Statuscategory1 statusCategory { get; set; }
}

public class Statuscategory1
{
    public string self { get; set; }

    public int id { get; set; }

    public string key { get; set; }

    public string colorName { get; set; }

    public string name { get; set; }
}

public class Priority1
{
    public string self { get; set; }

    public string iconUrl { get; set; }

    public string name { get; set; }

    public string id { get; set; }
}

public class Issuetype1
{
    public string self { get; set; }

    public string id { get; set; }

    public string description { get; set; }

    public string iconUrl { get; set; }

    public string name { get; set; }

    public bool subtask { get; set; }

    public int avatarId { get; set; }

    public int hierarchyLevel { get; set; }
}

public class Component
{
    public string self { get; set; }

    public string id { get; set; }

    public string name { get; set; }

    public string description { get; set; }
}

public class Fixversion
{
    public string self { get; set; }

    public string id { get; set; }

    public string description { get; set; }

    public string name { get; set; }

    public bool archived { get; set; }

    public bool released { get; set; }

    public string releaseDate { get; set; }
}