// Sample benchmark questions database
const questionsDatabase = [
    {
        id: 1,
        author: "Teacher A",
        category: "math",
        question: "What is the square root of 144?",
        options: {
            A: "10",
            B: "12",
            C: "14",
            D: "16"
        },
        correctAnswer: "B",
        explanation: "The square root of 144 is 12, because 12 × 12 = 144. This is a fundamental arithmetic operation.",
        votes: { up: 5, neutral: 1, down: 0 },
        comments: [
            { user: "Reviewer B", text: "Clear and straightforward question for beginners." },
            { user: "Reviewer C", text: "Good for assessing basic math skills." }
        ]
    },
    {
        id: 2,
        author: "Teacher B",
        category: "science",
        question: "What is the chemical formula for water?",
        options: {
            A: "H2O",
            B: "CO2",
            C: "O2",
            D: "H2O2"
        },
        correctAnswer: "A",
        explanation: "Water consists of two hydrogen atoms and one oxygen atom, resulting in the formula H2O. This is one of the most common molecules on Earth.",
        votes: { up: 8, neutral: 0, down: 0 },
        comments: []
    },
    {
        id: 3,
        author: "Teacher C",
        category: "history",
        question: "In which year did Kosovo declare independence?",
        options: {
            A: "1989",
            B: "1999",
            C: "2008",
            D: "2010"
        },
        correctAnswer: "C",
        explanation: "Kosovo declared independence on February 17, 2008. This was a significant event in Balkan history.",
        votes: { up: 7, neutral: 2, down: 1 },
        comments: [
            { user: "Reviewer A", text: "Important historical event for our region." }
        ]
    },
    {
        id: 4,
        author: "Teacher D",
        category: "language",
        question: "What is the past tense of 'go'?",
        options: {
            A: "Gone",
            B: "Going",
            C: "Went",
            D: "Goes"
        },
        correctAnswer: "C",
        explanation: "'Went' is the simple past tense form of 'go'. While 'gone' is the past participle, it's used with auxiliary verbs (e.g., 'has gone').",
        votes: { up: 6, neutral: 1, down: 0 },
        comments: []
    },
    {
        id: 5,
        author: "Teacher E",
        category: "general",
        question: "What is the capital of France?",
        options: {
            A: "Lyon",
            B: "Marseille",
            C: "Paris",
            D: "Nice"
        },
        correctAnswer: "C",
        explanation: "Paris is the capital and largest city of France. It's known as the 'City of Light' and is one of the most visited cities in the world.",
        votes: { up: 10, neutral: 0, down: 0 },
        comments: []
    },
    {
        id: 6,
        author: "Teacher A",
        category: "math",
        question: "What is 15% of 200?",
        options: {
            A: "15",
            B: "20",
            C: "25",
            D: "30"
        },
        correctAnswer: "D",
        explanation: "15% of 200 = (15/100) × 200 = 0.15 × 200 = 30. Percentage calculations are fundamental for real-world applications.",
        votes: { up: 4, neutral: 2, down: 1 },
        comments: [
            { user: "Reviewer D", text: "Maybe needs to clarify if calculator is allowed?" }
        ]
    },
    {
        id: 7,
        author: "Teacher B",
        category: "science",
        question: "What is the powerhouse of the cell?",
        options: {
            A: "Nucleus",
            B: "Mitochondria",
            C: "Ribosome",
            D: "Vacuole"
        },
        correctAnswer: "B",
        explanation: "The mitochondria is often called the 'powerhouse of the cell' because it produces ATP, which provides energy for cellular functions.",
        votes: { up: 9, neutral: 1, down: 0 },
        comments: []
    },
    {
        id: 8,
        author: "Teacher C",
        category: "history",
        question: "Who was the first President of the United States?",
        options: {
            A: "Thomas Jefferson",
            B: "George Washington",
            C: "James Madison",
            D: "Benjamin Franklin"
        },
        correctAnswer: "B",
        explanation: "George Washington was the first President of the United States, serving from 1789 to 1797. He is often called the 'Father of His Country'.",
        votes: { up: 11, neutral: 0, down: 0 },
        comments: []
    }
];
