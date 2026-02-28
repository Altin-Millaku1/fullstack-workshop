# Option 3: Benchmark Question Reviewer

A web application for validating and reviewing Albanian benchmark questions. Teachers and reviewers can vote on question quality, leave comments, and export validated questions.

## Features

### Core Functionality
- **Question Display** - View benchmark questions one at a time
- **Answer Verification** - Reveal correct answer with explanation
- **Voting System** - Vote: Approve ✅ / Reject ❌ / Contested ⚠️
- **Comments** - Leave feedback on question quality
- **Statistics Dashboard** - Track approval rates and validation status
- **Filtering** - Filter by category or validation status
- **Data Export** - Export approved questions as JSON

### Data Management
- **Local Storage** - All votes and comments saved locally
- **User Profiles** - Simple name-based login
- **Persistent Data** - Data survives browser refresh

## Getting Started

### Using Local Storage (No Backend Required)
1. Open `index.html` in a web browser
2. Enter your name to start reviewing
3. Vote on questions and leave comments
4. Data saves automatically

### Deploy Locally
1. Install [Live Server](https://marketplace.visualstudio.com/items?itemName=ritwickdey.LiveServer)
2. Right-click `index.html` → "Open with Live Server"

## Optional: Add Supabase Backend

For a more robust setup with database persistence:

1. Create account at [supabase.com](https://supabase.com)
2. Create a table: `questions`
3. Add columns: `id`, `question_text`, `category`, `created_by`, `votes`, `comments`
4. Replace `data.js` with API calls

Example:
```javascript
async function loadQuestions() {
    const { data } = await supabase
        .from('questions')
        .select('*');
    questionsDatabase = data;
}
```

## Usage Guide

### Reviewing Questions
1. **Read** the question carefully
2. **Try to answer** by selecting an option
3. **Click "Show Answer"** to verify
4. **Vote** on quality (Approve/Reject/Contested)
5. **Leave a comment** if needed
6. **Next** to continue

### Voting Meanings
- **✅ Approve**: Question is well-written and appropriate
- **❌ Reject**: Question is flawed, ambiguous, or incorrect
- **⚠️ Contested**: Question needs discussion before approval

### Exporting Results
1. Filter for approved questions only
2. Click **"Export Valid Questions"**
3. JSON file downloads with validated questions

## File Structure
```
option-3-question-reviewer/
├── index.html       # Main app structure
├── style.css        # Responsive styling
├── script.js        # Core application logic
├── data.js          # Question database
└── README.md        # Documentation
```

## Sample Data
The app includes 8 sample questions across categories:
- Mathematics
- Science
- History  
- Language
- General Knowledge

## Future Enhancements

**Easy:**
- Add more questions to `data.js`
- Customize categories
- Add difficulty levels

**Medium:**
- Connect to Google Sheets API
- Add user authentication
- Create review analytics

**Advanced:**
- Build REST API backend
- Connect to Supabase database
- Add real-time collaboration
- Implement notification system

## Deploy

### Netlify
1. Drag and drop folder to [netlify.com](https://netlify.com)
2. Site goes live instantly

### GitHub Pages
1. Push to GitHub repository
2. Enable GitHub Pages
3. Available at `https://<username>.github.io/<repo>`

### Vercel
1. Import from GitHub
2. Deploy with one click

## Browser Support
- Chrome/Edge (latest)
- Firefox (latest)
- Safari (latest)
- Mobile browsers

## Keyboard Shortcuts (Future)
- `N` = Next question
- `P` = Previous question
- `A` = Approve
- `R` = Reject
- `C` = Contested

## Data Privacy
- All data stored locally in browser
- No server-side tracking
- Users control their own data
- Optional: Connect to your own Supabase

Perfect for FLOSSK workshop and collaborative question validation!
