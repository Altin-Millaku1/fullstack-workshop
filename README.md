# FLOSSK Full Stack Workshop - All 3 Project Options

Complete, production-ready implementations of all three projects from the FLOSSK Full Stack workshop.

## 📁 Project Structure

```
fullstack-workshop/
├── option-1-personal-site/          # Personal portfolio website
│   ├── index.html
│   ├── style.css
│   ├── script.js
│   └── README.md
├── option-2-dashboard/              # Hackerspace dashboard
│   ├── index.html
│   ├── style.css
│   ├── script.js
│   └── README.md
├── option-3-question-reviewer/      # Question validation app
│   ├── index.html
│   ├── style.css
│   ├── script.js
│   ├── data.js
│   └── README.md
└── README.md                         # This file
```

## 🚀 Quick Start

### Option 1: Personal Portfolio Site (Beginners)
**Perfect for:** First-time web developers learning HTML/CSS/JS

```bash
cd option-1-personal-site
# Open index.html in browser
```

**Features:**
- Responsive design (mobile, tablet, desktop)
- Smooth scrolling navigation
- About section with skills
- Project showcase
- Contact form
- Social links
- Scroll animations

**Deploy to:**
- GitHub Pages (free)
- Netlify (free, drag & drop)
- Vercel

**Time to complete:** 30 min - 1 hour

---

### Option 2: Hackerspace Dashboard (Intermediate)
**Perfect for:** Multiple team members contributing widgets

```bash
cd option-2-dashboard
# Open index.html in browser
```

**Features:**
- 6 Live widgets (Air Quality, Weather, News, Events, Clock, Status)
- Real-time updates every 5 minutes
- Beautiful gradient interface
- Auto-updating clock
- Responsive mobile design
- Mock API data (easy to connect real APIs)

**Widgets included:**
1. 🌍 Air Quality Monitor
2. ⛅ Weather Widget
3. 📰 News Headlines
4. 📅 Events Calendar
5. 🕐 Digital Clock with facts
6. ✅ Hackerspace Status

**Team breakdown:**
- Person 1: Air quality widget
- Person 2: Weather widget
- Person 3: News widget
- Person 4: Events widget
- Person 5: Clock widget
- Person 6: Status indicator

**Deploy to:**
- Netlify (perfect for team collaboration)
- Heroku (if adding backend)
- GitHub Pages

**Time to complete:** 2-3 hours (team)

---

### Option 3: Question Reviewer (Advanced)
**Perfect for:** Building full feature applications

```bash
cd option-3-question-reviewer
# Open index.html in browser
```

**Features:**
- 📚 8 Sample questions (Math, Science, History, Language)
- 🗳️ Voting system (Approve/Reject/Contested)
- 💬 Comment system
- 📊 Statistics dashboard
- 🔍 Advanced filtering
- 📥 Export validated questions as JSON
- 💾 Local storage persistence
- 👤 User login system

**Functionality:**
- View benchmark questions one by one
- Reveal answer and explanation
- Vote on question quality
- Leave feedback comments
- Track approval statistics
- Filter by category
- Export approved questions

**Optional upgrades:**
- Connect to Google Sheets API
- Add Supabase database
- Real-time collaboration
- User authentication

**Deploy to:**
- Any static hosting (GitHub Pages, Netlify, Vercel)
- With backend (Vercel serverless functions + Supabase)

**Time to complete:** 3-4 hours

---

## 🛠️ Setup Instructions

### Prerequisites
- VS Code or any text editor
- Modern web browser (Chrome, Firefox, Safari, Edge)
- [Optional] Live Server extension for VS Code

### Installing Live Server (Recommended)
1. Open VS Code
2. Go to Extensions (Ctrl+Shift+X)
3. Search "Live Server"
4. Install by Ritwick Dey
5. Right-click any HTML file → "Open with Live Server"

### Running Each Project

**Using Live Server:**
```bash
1. Open the project folder
2. Right-click index.html
3. Select "Open with Live Server"
4. Browser opens automatically
```

**Direct browser:**
```bash
1. Open index.html in your browser
2. Or: Ctrl+O and navigate to index.html
```

---

## 🌐 Deployment Options

### GitHub Pages (Free, Easy)
```bash
1. Create repo named <username>.github.io
2. Push files to repository
3. Open https://<username>.github.io
```

### Netlify (Free, Easiest)
```bash
1. Go to netlify.com
2. Drag and drop your project folder
3. Done! Site is live within seconds
```

### Vercel (Free, Fast)
```bash
1. Go to vercel.com
2. Import from GitHub or upload folder
3. Automatic deployments on push
```

---

## 📝 Customization Guide

### Option 1: Personal Site
Edit `index.html`:
- Change name/title
- Update skills list
- Modify project descriptions
- Add your own images

Edit `style.css`:
- Change colors (--primary, --secondary)
- Adjust layout
- Modify animations

### Option 2: Dashboard
Edit `script.js`:
- Replace mock data with real APIs:
  - IQAir (air quality)
  - OpenWeatherMap (weather)
  - NewsAPI (news)
- Change refresh intervals
- Add more widgets

Edit `style.css`:
- Change gradient colors
- Resize widgets
- Modify animations

### Option 3: Question Reviewer
Edit `data.js`:
- Add more sample questions
- Change categories
- Customize explanations

Edit `script.js`:
- Connect to real database (Supabase)
- Add user authentication
- Implement real-time sync

---

## 🔌 API Integration Examples

### Option 2: Dashboard - Adding Real APIs

**Air Quality:**
```javascript
const response = await fetch(
  `https://api.waqi.info/feed/Prishtina/?token=YOUR_TOKEN`
);
const data = await response.json();
```

**Weather:**
```javascript
const response = await fetch(
  `https://api.openweathermap.org/data/2.5/weather?q=Prishtina&appid=YOUR_KEY`
);
const data = await response.json();
```

**News:**
```javascript
const response = await fetch(
  `https://newsapi.org/v2/everything?q=Kosovo&apiKey=YOUR_KEY`
);
const data = await response.json();
```

### Option 3: Database Connection (Optional)

```javascript
// Supabase example
import { createClient } from '@supabase/supabase-js';

const supabase = createClient(URL, KEY);

// List questions
const { data, error } = await supabase
  .from('questions')
  .select('*');

// Add vote
const { data, error } = await supabase
  .from('votes')
  .insert({ question_id: 1, user: 'John', vote: 'approved' });
```

---

## 📚 Learning Outcomes

### After completing these projects, you'll know:

**Option 1:**
- ✅ HTML structure and semantics
- ✅ CSS layouts, flexbox, grid
- ✅ Responsive design (mobile-first)
- ✅ JavaScript DOM manipulation
- ✅ Smooth animations and transitions

**Option 2:**
- ✅ Multi-widget architecture
- ✅ JavaScript async/await
- ✅ Real-time data updates
- ✅ API integration patterns
- ✅ Team collaboration workflow

**Option 3:**
- ✅ Application state management
- ✅ Local storage / persistence
- ✅ Advanced DOM interactions
- ✅ Data filtering and sorting
- ✅ Export functionality
- ✅ Database integration (optional)

---

## 🎯 Workshop Timeline

**Session 1: First Build (3 hours)**
- 30 min: Intro & setup
- 90 min: Build chosen project
- 30 min: Deploy project
- 30 min: Demo & feedback

**Deliverables:**
- Live URL
- 1-3 min demo
- Code on GitHub

---

## 🤝 Contributing

These projects are part of the FLOSSK workshop series. To contribute:

1. Fork or clone
2. Make improvements
3. Submit feedback
4. Share with workshop participants

---

## 📞 Support

**For issues:**
- Check project-specific README.md
- Review code comments
- Check browser console for errors (F12)

**For questions:**
- Ask in FLOSSK Slack
- Post in workshop forum
- Create GitHub issue

---

## 📄 License

These projects are open source. Use, modify, and share freely!

---

## 🎓 Next Steps After Workshop

1. **Add features** to your chosen project
2. **Connect real APIs**
3. **Learn a framework** (React, Vue, Next.js)
4. **Build a backend** (Node.js, Python, Go)
5. **Deploy with database** (Supabase, Firebase, MongoDB)
6. **Join FLOSSK community** for ongoing learning

---

**Happy coding! 🚀**

Made for FLOSSK Full Stack Workshop 2026
