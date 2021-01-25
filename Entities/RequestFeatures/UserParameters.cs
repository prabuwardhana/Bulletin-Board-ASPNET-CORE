namespace Entities.RequestFeatures
{
    public class UserParameters : RequestParameters
    {
        public UserParameters()
        {
            // Set the default sorting condition when requesting users list
            OrderBy = "RegisterDateTime desc";
        }
    }
}