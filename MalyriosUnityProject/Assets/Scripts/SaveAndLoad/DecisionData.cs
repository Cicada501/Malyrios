namespace SaveAndLoad
{
    [System.Serializable]
    public class DecisionData
    {
        public bool bigRatAttack;
        public bool learnedFireball;
        public int wizardDialogueState = 1;
        public int sonDialogueState = 1;
        public int hunterDialogState = 1;
        public int healerDialogState = 1;
        public bool smallWerewolfAttack;
        
    
        //create constructor
        // public DecisionData()
        // {
        //     this.bigRatAttack = Decision.BigRatAttack;
        //     this.learnedFireball = Decision.LearnedFireball;
        //     this.wizardDialogueState = Decision.WizardDialogueState;
        //     sonDialogueState = Decision.SonDialogueState;
        //     hunterDialogState = Decision.HunterDialogState;
        //     smallWerewolfAttack = Decision.SmallWerewolfAttack;
        //     healerDialogState = Decision.HealerDialogState;
        // }
    }
}