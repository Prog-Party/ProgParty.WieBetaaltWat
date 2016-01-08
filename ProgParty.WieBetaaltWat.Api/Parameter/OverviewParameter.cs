namespace ProgParty.BoredPanda.Api.Parameter
{
    public class OverviewParameter
    {
        public int Paging { get; set; }

        public bool StartOver { get; set; } = false;

        public ArticleCategory Category { get; set; } = ArticleCategory.All;
    }

    public enum ArticleCategory
    {
        Advertising
        ,All
        ,Animals
        ,Architecture
        ,Art
        ,Automotive
        ,BodyArt
        ,Comics
        ,DigitalArt
        ,DIY
        ,Drawing
        ,Entertainment
        ,FoodArt
        ,Funny
        ,FurnitureDesign
        ,GoodNews
        ,GraphicDesign
        ,History
        ,Home
        ,Illustration
        ,Installation
        ,InteriorDesign
        ,LandArt
        ,Nature
        ,NeedleAndThread
        ,OpticalIllusions
        ,Other
        ,Packaging
        ,Painting
        ,PaperArt
        ,Parenting
        ,Photography
        ,Pics
        ,ProductDesign
        ,Recycling
        ,Science
        ,Sculpting
        ,SocialIssues
        ,StreetArt
        ,Style
        ,Technology
        ,Travel
        ,Typography
        ,Video
        ,Weird
    }
}
