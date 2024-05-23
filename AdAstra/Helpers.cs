using AdAstra.Models;

namespace AdAstra
{
    public static class Helpers
    {
        public static int GetReplyDepth(Models.Reply reply)
        {
            int depth = 0;
            while(reply.ParentReplyId is not null)
            {
                depth++;
                reply = reply.ParentReply;
            }
            return depth;
        }

    }
}
