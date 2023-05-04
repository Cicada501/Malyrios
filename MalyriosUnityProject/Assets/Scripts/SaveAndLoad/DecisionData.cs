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
        public DecisionData()
        {
            this.bigRatAttack = Decision.Instance.BigRatAttack;
            this.learnedFireball = Decision.Instance.LearnedFireball;
            this.wizardDialogueState = Decision.Instance.WizardDialogueState;
            sonDialogueState = Decision.Instance.SonDialogueState;
            hunterDialogState = Decision.Instance.HunterDialogState;
            smallWerewolfAttack = Decision.Instance.SmallWerewolfAttack;
            healerDialogState = Decision.Instance.HealerDialogState;
        }
    }
}