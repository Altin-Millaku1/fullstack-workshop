// Benchmark Question Reviewer Application

let currentQuestionIndex = 0;
let userVotes = {}; // Store votes and comments per question
let currentUser = null;
let filteredQuestions = [];

// Initialize application
document.addEventListener('DOMContentLoaded', () => {
    setupEventListeners();
    checkUserLogin();
});

// Check if user is already logged in (from localStorage)
function checkUserLogin() {
    currentUser = localStorage.getItem('reviewerUser');
    
    if (currentUser) {
        showMainApp();
    } else {
        showLoginScreen();
    }
}

// Show login screen
function showLoginScreen() {
    document.getElementById('login-overlay').style.display = 'flex';
}

// Handle login
function handleLogin() {
    const usernameInput = document.getElementById('username-input');
    const username = usernameInput.value.trim();
    
    if (username) {
        currentUser = username;
        localStorage.setItem('reviewerUser', username);
        document.getElementById('login-overlay').style.display = 'none';
        showMainApp();
    } else {
        alert('Please enter your name or email');
    }
}

// Show main app
function showMainApp() {
    loadUserVotes();
    filteredQuestions = [...questionsDatabase];
    displayQuestion();
    updateStatistics();
}

// Setup all event listeners
function setupEventListeners() {
    document.getElementById('login-btn').addEventListener('click', handleLogin);
    document.getElementById('username-input').addEventListener('keypress', (e) => {
        if (e.key === 'Enter') handleLogin();
    });

    // Navigation
    document.getElementById('next-btn').addEventListener('click', nextQuestion);
    document.getElementById('prev-btn').addEventListener('click', previousQuestion);

    // Voting
    document.getElementById('vote-up').addEventListener('click', () => voteQuestion('approved'));
    document.getElementById('vote-neutral').addEventListener('click', () => voteQuestion('contested'));
    document.getElementById('vote-down').addEventListener('click', () => voteQuestion('rejected'));

    // Answer reveal
    document.getElementById('reveal-btn').addEventListener('click', revealAnswer);

    // Comment submission
    document.getElementById('submit-comment-btn').addEventListener('click', submitComment);

    // Filters
    document.getElementById('filter-all').addEventListener('change', applyFilters);
    document.getElementById('filter-approved').addEventListener('change', applyFilters);
    document.getElementById('filter-rejected').addEventListener('change', applyFilters);
    document.getElementById('filter-pending').addEventListener('change', applyFilters);
    document.getElementById('category-filter').addEventListener('change', applyFilters);

    // Export
    document.getElementById('export-btn').addEventListener('click', exportQuestions);
    document.getElementById('clear-votes-btn').addEventListener('click', clearAllVotes);
}

// Display current question
function displayQuestion() {
    if (filteredQuestions.length === 0) {
        alert('No questions match your filters');
        return;
    }

    const question = filteredQuestions[currentQuestionIndex];
    
    // Update question content
    document.getElementById('question-author').textContent = question.author;
    document.getElementById('question-category').textContent = question.category.charAt(0).toUpperCase() + question.category.slice(1);
    document.getElementById('question-content').textContent = question.question;
    
    // Update options
    document.getElementById('option-A').textContent = question.options.A;
    document.getElementById('option-B').textContent = question.options.B;
    document.getElementById('option-C').textContent = question.options.C;
    document.getElementById('option-D').textContent = question.options.D;

    // Reset answer display
    document.getElementById('answer-section').style.display = 'none';
    document.querySelectorAll('input[name="answer"]').forEach(input => input.checked = false);

    // Update status
    const userVote = userVotes[question.id];
    const status = userVote ? userVote.vote : 'pending';
    document.getElementById('question-status').innerHTML = status.charAt(0).toUpperCase() + status.slice(1);
    document.getElementById('question-status').className = `question-status ${status}`;

    // Load comments
    loadComments(question.id);

    // Update vote buttons
    updateVoteButtons(question.id);

    // Update counter
    document.getElementById('current-question').textContent = currentQuestionIndex + 1;
    document.getElementById('total-questions').textContent = filteredQuestions.length;

    // Update button states
    document.getElementById('prev-btn').disabled = currentQuestionIndex === 0;
    document.getElementById('next-btn').disabled = currentQuestionIndex === filteredQuestions.length - 1;
}

// Reveal answer
function revealAnswer() {
    const question = filteredQuestions[currentQuestionIndex];
    const answerSection = document.getElementById('answer-section');
    
    document.getElementById('correct-answer').textContent = question.correctAnswer;
    document.getElementById('explanation-text').textContent = question.explanation;
    
    answerSection.style.display = 'block';
    
    // Highlight correct option
    document.querySelectorAll('.option').forEach(option => {
        option.classList.remove('correct', 'incorrect');
    });
    
    document.querySelectorAll('input[name="answer"]').forEach(input => {
        const option = input.parentElement;
        if (input.value === question.correctAnswer) {
            option.classList.add('correct');
        }
    });
}

// Vote on question
function voteQuestion(vote) {
    const question = filteredQuestions[currentQuestionIndex];
    
    userVotes[question.id] = {
        vote: vote,
        comments: userVotes[question.id]?.comments || []
    };
    
    saveUserVotes();
    updateVoteButtons(question.id);
    updateStatistics();
    
    // Update status display
    document.getElementById('question-status').innerHTML = vote.charAt(0).toUpperCase() + vote.slice(1);
    document.getElementById('question-status').className = `question-status ${vote}`;
}

// Update vote button states
function updateVoteButtons(questionId) {
    const vote = userVotes[questionId]?.vote;
    
    document.querySelectorAll('.vote-btn').forEach(btn => {
        btn.classList.remove('active');
    });
    
    if (vote === 'approved') document.getElementById('vote-up').classList.add('active');
    if (vote === 'rejected') document.getElementById('vote-down').classList.add('active');
    if (vote === 'contested') document.getElementById('vote-neutral').classList.add('active');
}

// Submit comment
function submitComment() {
    const question = filteredQuestions[currentQuestionIndex];
    const commentInput = document.getElementById('comment-input');
    const comment = commentInput.value.trim();
    
    if (!comment) {
        alert('Please enter a comment');
        return;
    }
    
    if (!userVotes[question.id]) {
        userVotes[question.id] = { vote: 'pending', comments: [] };
    }
    
    userVotes[question.id].comments.push({
        user: currentUser,
        text: comment
    });
    
    saveUserVotes();
    commentInput.value = '';
    loadComments(question.id);
}

// Load comments
function loadComments(questionId) {
    const commentsList = document.getElementById('comments-list');
    const comments = userVotes[questionId]?.comments || [];
    
    if (comments.length === 0) {
        commentsList.innerHTML = '<p style="color: #999;">No comments yet. Be the first to comment!</p>';
        return;
    }
    
    commentsList.innerHTML = comments.map(comment => `
        <div class="comment">
            <div class="comment-user">${comment.user}</div>
            <div class="comment-text">${comment.text}</div>
        </div>
    `).join('');
}

// Navigation
function nextQuestion() {
    if (currentQuestionIndex < filteredQuestions.length - 1) {
        currentQuestionIndex++;
        displayQuestion();
        document.querySelector('.question-section').scrollIntoView({ behavior: 'smooth' });
    }
}

function previousQuestion() {
    if (currentQuestionIndex > 0) {
        currentQuestionIndex--;
        displayQuestion();
        document.querySelector('.question-section').scrollIntoView({ behavior: 'smooth' });
    }
}

// Update statistics
function updateStatistics() {
    let approved = 0, rejected = 0, contested = 0;
    
    Object.values(userVotes).forEach(vote => {
        if (vote.vote === 'approved') approved++;
        else if (vote.vote === 'rejected') rejected++;
        else if (vote.vote === 'contested') contested++;
    });
    
    document.getElementById('total-count').textContent = filteredQuestions.length;
    document.getElementById('approved-count').textContent = approved;
    document.getElementById('rejected-count').textContent = rejected;
    document.getElementById('contested-count').textContent = contested;
}

// Apply filters
function applyFilters() {
    const filterAll = document.getElementById('filter-all').checked;
    const filterApproved = document.getElementById('filter-approved').checked;
    const filterRejected = document.getElementById('filter-rejected').checked;
    const filterPending = document.getElementById('filter-pending').checked;
    const categoryFilter = document.getElementById('category-filter').value;
    
    filteredQuestions = questionsDatabase.filter(q => {
        let passFilter = false;
        
        if (filterAll) passFilter = true;
        
        const userVote = userVotes[q.id]?.vote || 'pending';
        if (filterApproved && userVote === 'approved') passFilter = true;
        if (filterRejected && userVote === 'rejected') passFilter = true;
        if (filterPending && userVote === 'pending') passFilter = true;
        
        if (categoryFilter && q.category !== categoryFilter) passFilter = false;
        
        return passFilter;
    });
    
    currentQuestionIndex = 0;
    displayQuestion();
    updateStatistics();
}

// Export validated questions
function exportQuestions() {
    const approvedQuestions = filteredQuestions.filter(q => {
        const userVote = userVotes[q.id];
        return userVote && userVote.vote === 'approved';
    });
    
    if (approvedQuestions.length === 0) {
        alert('No approved questions to export');
        return;
    }
    
    const jsonData = JSON.stringify(approvedQuestions, null, 2);
    const dataStr = "data:text/json;charset=utf-8," + encodeURIComponent(jsonData);
    
    const downloadLink = document.createElement('a');
    downloadLink.setAttribute("href", dataStr);
    downloadLink.setAttribute("download", "validated_questions.json");
    downloadLink.click();
}

// Clear all votes
function clearAllVotes() {
    if (confirm('Are you sure you want to clear all your votes? This cannot be undone.')) {
        userVotes = {};
        localStorage.removeItem('reviewerVotes');
        updateStatistics();
        displayQuestion();
    }
}

// Local storage functions
function saveUserVotes() {
    localStorage.setItem('reviewerVotes', JSON.stringify(userVotes));
}

function loadUserVotes() {
    const savedVotes = localStorage.getItem('reviewerVotes');
    if (savedVotes) {
        userVotes = JSON.parse(savedVotes);
    }
}
