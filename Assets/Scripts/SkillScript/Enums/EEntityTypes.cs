namespace SkillScript.Enums
{
    public enum EEntityTypes
    {
        PlayerCharacter,
        FriendNPCCharacter,
        EnemyNPCCharacter,
        Team1,
        Team2,
        Team3,
        Team4
    }
    public static class EntityTypeUtils{
        public static bool IsPlayer(this EEntityTypes type)
        {
            return type == EEntityTypes.PlayerCharacter ||
                   type == EEntityTypes.Team1 ||
                   type == EEntityTypes.Team2 ||
                   type == EEntityTypes.Team3 ||
                   type == EEntityTypes.Team4;
        }

        public static bool IsNPC(this EEntityTypes type)
        {
            return type == EEntityTypes.EnemyNPCCharacter ||
                   type == EEntityTypes.FriendNPCCharacter;
        }
    }
}