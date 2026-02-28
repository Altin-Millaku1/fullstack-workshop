# Option 2: Prishtina Hackerspace Dashboard

A live dashboard displaying real-time information for the Prishtina Hackerspace, featuring multiple widgets including air quality, weather, news, events, and more.

## Features

### Widgets Included:
1. **Air Quality Monitor** - Real-time air quality index for Prishtina
2. **Weather Widget** - Current conditions and forecast
3. **News Headlines** - Latest news from Albanian news sources
4. **Events Calendar** - Upcoming FLOSSK events
5. **Digital Clock** - Current time with today's historical fact
6. **Hackerspace Status** - Live open/closed status

## Technologies Used
- HTML5
- CSS3 (Grid, Animations, Gradients)
- Vanilla JavaScript (ES6+)

## Getting Started

### Install Live Server
1. Install [Live Server](https://marketplace.visualstudio.com/items?itemName=ritwickdey.LiveServer) in VS Code
2. Right-click `index.html` and select "Open with Live Server"

### Or Open Directly
Simply open `index.html` in your web browser

## API Integration (Optional Enhancements)

To add real API data:

**Air Quality:**
```javascript
// IQAir API
fetch(`https://api.waqi.info/feed/Prishtina/?token=YOUR_TOKEN`)
```

**Weather:**
```javascript
// OpenWeatherMap API
fetch(`https://api.openweathermap.org/data/2.5/weather?q=Prishtina&appid=YOUR_KEY`)
```

**News:**
```javascript
// NewsAPI
fetch(`https://newsapi.org/v2/everything?q=Kosovo&apiKey=YOUR_KEY`)
```

## Deploy

### Netlify (Recommended)
1. Go to [netlify.com](https://netlify.com)
2. Drag and drop the folder
3. Your dashboard is live!

### GitHub Pages
1. Push to a GitHub repository
2. Enable GitHub Pages in settings
3. Live on `https://<username>.github.io/<repo>`

## Customization

Edit `script.js` to:
- Change mock data to real API calls
- Add more widgets
- Customize refresh intervals
- Add more history facts

Edit `style.css` to:
- Change colors and gradients
- Adjust widget layout
- Modify animations and transitions

## Team Contributions

Each person can build one widget as a standalone component, then integrate into the main dashboard:

- **Person 1:** Air quality widget
- **Person 2:** Weather widget
- **Person 3:** News widget
- **Person 4:** Events widget
- **Person 5:** Clock widget
- **Person 6:** Status widget

## Demo

The dashboard shows:
- Auto-updating clock
- Real-time status indicators
- Smooth animations
- Responsive design for all screen sizes
- Beautiful gradient background

Perfect for display on a hackerspace TV screen!
