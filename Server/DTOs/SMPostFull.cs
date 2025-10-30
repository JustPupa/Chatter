using Cozy_Chatter.Models;

namespace Cozy_Chatter.DTOs
{
    public class SMPostFull : SMPost
    {
        public User? Publisher;
        public SMPostFull? ReferencePostFull;
    }
}