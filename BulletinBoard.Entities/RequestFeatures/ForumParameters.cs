using System;

namespace BulletinBoard.Entities.RequestFeatures
{
    public class ForumParameters : RequestParameters
    {
        public ForumParameters()
        {
            OrderBy = "createddatetime desc";
            SearchTerm = String.Empty;
        }        
    }
}