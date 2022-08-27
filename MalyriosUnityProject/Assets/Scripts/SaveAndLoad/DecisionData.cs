namespace SaveAndLoad
{
    [System.Serializable]
    public class DecisionData
    {
        public bool bigRatAttack;
        public bool learnedFireball;
        public int wizardDialogueState = 1;
    
        //create constructor
        public DecisionData()
        {
            this.bigRatAttack = Decision.BigRatAttack;
            this.learnedFireball = Decision.LearnedFireball;
            this.wizardDialogueState = Decision.WizardDialogueState;
        }
    }
}