using Sitecore.Data;

namespace Final_Test
{
    public struct Templates
    {
        public struct FoodItem
        {
            public static readonly ID TemplateId = new ID("{F2335507-2C7B-4696-B856-FDFA3735A305}");
            public static readonly ID Title = new ID("{FD2BB91F-085B-4097-804E-74C5BF58AD91}");
            public static readonly ID Description = new ID("{6F02F44B-D843-4777-BAA1-2F4CCA8DD62D}");
            public static readonly ID Price = new ID("{132CFF3A-4CBF-4E39-B68D-DD95D8E6E8C1}");
            public static readonly ID FoodPhoto = new ID("{988DE6C0-09C6-4B48-AD33-B1A5669BE6E2}");
        }

        public struct Footer
        {
            public static readonly ID TemplateId = new ID("{306F6281-F7A9-404E-826D-D465741826CA}");
            public static readonly ID FooterContent = new ID("{25F2691D-D0CC-4261-952A-477B935F5821}");
        }

        public struct Header
        {
            public static readonly ID TemplateId = new ID("{2D46F930-02AC-41B2-A5D1-862F6A6D23CB}");
            public static readonly ID Title = new ID("{8F6F3D29-167C-43D4-8E16-964167E0F1B9}");
            public static readonly ID Description = new ID("{0E00DDAC-F363-4E2C-BBA5-1A6C5BD3F7B7}");
            public static readonly ID Logo = new ID("{5CD91822-EABE-4419-A34A-5A35F9B642D0}");
            public static readonly ID BackgroundImage = new ID("{AFC69CCE-E9CA-4010-B2D6-0EC283E93F03}");
            public static readonly ID FirstLink = new ID("{F2CD8FDC-DD09-4DD6-95CB-978FCA467307}");
            public static readonly ID SecondLink = new ID("{5FED1EAA-BA7F-4522-A21B-A379ECE6CD0A}");
        }

        public struct MainBodyContent
        {
            public static readonly ID TemplateId = new ID("{CB74F0C3-C813-4243-AF1C-6D62E226A3D6}");
            public static readonly ID Content = new ID("{D9C1EC9D-F10B-4831-8544-58C2ADCCB620}");
        }

        public struct MiniArticle
        {
            public static readonly ID TemplateId = new ID("{ED012543-E061-4AE2-BECD-533150C38F42}");
            public static readonly ID Image = new ID("{43248B05-E014-44F3-9BE9-FD20B9538925}");
            public static readonly ID Description = new ID("{6DC24343-0B32-4FB8-9842-CD661EC9F79A}");
            public static readonly ID Title = new ID("{E3D7A099-826D-4CAC-A68D-7779C13E7A97}");
        }

        public struct Search
        {
            public static readonly ID TemplateId = new ID("{C02A13FD-A69F-4BE7-B978-7EA108427908}");
        }
    }
}