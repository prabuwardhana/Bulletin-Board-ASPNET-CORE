using System;

namespace Entities.RequestFeatures
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