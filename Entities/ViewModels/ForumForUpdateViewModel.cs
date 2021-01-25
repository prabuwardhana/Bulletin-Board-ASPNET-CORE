namespace Entities.ViewModels
{
    public class ForumForUpdateViewModel : ForumForManipulationViewModel
    {
        public int id { get; set; }
        public bool IsLocked { get; set; }
    }
}