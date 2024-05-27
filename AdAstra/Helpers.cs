using AdAstra.Models;
using System.Text.RegularExpressions;

namespace AdAstra
{
    public static class Helpers
    {
        public static int GetReplyDepth(Models.Reply reply)
        {
            int depth = 0;
            while (reply.ParentReplyId is not null)
            {
                depth++;
                reply = reply.ParentReply;
            }
            return depth;
        }

        public static readonly List<string> Prophanities = new()
        {
            "ass",
            "asshole",
            "bastard",
            "bitch",
            "bollocks",
            "bugger",
            "bullshit",
            "crap",
            "cunt",
            "damn",
            "dick",
            "dickhead",
            "fag",
            "faggot",
            "fuck",
            "fucker",
            "fucking",
            "goddamn",
            "jerk",
            "kike",
            "motherfucker",
            "nigger",
            "piss",
            "prick",
            "pussy",
            "shit",
            "shithead",
            "shitty",
            "slut",
            "spick",
            "twat",
            "wanker",
            "whore",
            "arse",
            "bint",
            "bollocks",
            "clunge",
            "cock",
            "coon",
            "douche",
            "knob",
            "minge",
            "minger",
            "nonce",
            "pillock",
            "plonker",
            "tosser"
        };

        public static string ReplaceProphanities(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            string pattern = string.Join("|", Prophanities.Select(Regex.Escape));

            return Regex.Replace(input, pattern, match => new string('*', match.Length), RegexOptions.IgnoreCase);
        }
    }
}
