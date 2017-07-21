namespace QuestionnaireSpecGenerator
{
    // Programmer note: When adding Question Types, add them here, and also in 
    // QuestionBlock.GenerateQuestionTypeString(), and don't forget to add the LanguageStrings.QuestionType

    /// <summary>
    /// The point of these Question Type flags is to save users time from writing question type 
    /// strings in the spec. These flags are intended to auto-generate these Question Types - 
    /// these flags are chosen automatically 
    /// </summary>
    public enum QuestionType
    {
        BreakScreen = 0,
        SingleCode,
        MultiCode,
        BrandTextField,
        FullTextField,
        NumericTextField,
        MultiTextField,
        HorizontalScale,
        Grid,
        Marker
    }
}
