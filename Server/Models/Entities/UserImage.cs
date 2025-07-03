namespace Server.Models.Entities
{
    public class UserImage
    {
        public int Id { get; set; } // id for person whose photo will be uploaded
        public string Name { get; set; } // name of Person : abdelrahman, ....
        public string FileName { get; set; } //abdelrahman.jpg
        public string ImagePath { get; set; }
        public string OwnerUserId { get; set; } // FK to Home Owner
        public ApplicationUser OwnerUser { get; set; }
        public DateTime UploadedAt { get; set; }
        //public bool IsOwnerImage { get; set; }
        public string Relation {  get; set; }
    }
}
