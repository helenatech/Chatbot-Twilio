namespace Chatbot.Domain.Models
{
    public class Avaliation
    {
        public int Id { get; set; }               
        public int? InteractionId { get; set; }       
        public Interaction Interaction { get; set; } 
        public byte Rating { get; private set; }   
        public DateTime CreatedAt { get; set; }    
        public void SetRating(byte rating)
        {
            if (rating < 1 || rating > 5)
                throw new ArgumentException("Rating must be 1-5.");
            Rating = rating;
        }
    }
}
