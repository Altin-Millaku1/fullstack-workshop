// Prishtina Hackerspace Dashboard

// 1. AIR QUALITY WIDGET
async function loadAirQuality() {
    try {
        // Using IQAir API (free tier limited) - showing mock data for demo
        const mockData = {
            aqi: 62,
            status: 'Moderate air quality',
            detail: 'PM2.5: 25 µg/m³ - Individuals with respiratory conditions should limit prolonged outdoor exertion'
        };
        
        document.getElementById('aqi-value').textContent = mockData.aqi;
        document.getElementById('air-status').textContent = mockData.status;
        document.getElementById('air-detail').textContent = mockData.detail;
        
        // Update gauge color
        const gauge = document.querySelector('.gauge-circle');
        if (mockData.aqi <= 50) gauge.style.filter = 'hue-rotate(90deg)';
        else if (mockData.aqi <= 100) gauge.style.filter = 'hue-rotate(45deg)';
        else if (mockData.aqi <= 150) gauge.style.filter = 'hue-rotate(0deg)';
        else gauge.style.filter = 'hue-rotate(-45deg)';
    } catch (error) {
        console.error('Error loading air quality:', error);
    }
}

// 2. WEATHER WIDGET
async function loadWeather() {
    try {
        // Mock weather data for Prishtina
        const mockWeather = {
            temp: 12,
            condition: 'Partly Cloudy',
            humidity: 65,
            wind: 3.2,
            sunrise: '06:45',
            sunset: '18:20'
        };
        
        document.getElementById('temp').textContent = mockWeather.temp + '°C';
        document.getElementById('condition').textContent = mockWeather.condition;
        document.getElementById('humidity').textContent = mockWeather.humidity;
        document.getElementById('wind').textContent = mockWeather.wind;
        document.getElementById('sunrise').textContent = mockWeather.sunrise;
    } catch (error) {
        console.error('Error loading weather:', error);
    }
}

// 3. NEWS HEADLINES
async function loadNews() {
    try {
        const mockNews = [
            { title: 'FLOSSK Workshop Series Launches', date: 'Feb 28, 2026' },
            { title: 'Kosovo Tech Community Grows', date: 'Feb 25, 2026' },
            { title: 'Open Source Projects in Prishtina', date: 'Feb 22, 2026' },
            { title: 'Tech Jobs Fair Next Month', date: 'Feb 20, 2026' }
        ];
        
        const newsFeed = document.getElementById('news-feed');
        newsFeed.innerHTML = mockNews.map(item => `
            <div class="news-item">
                <h4>${item.title}</h4>
                <p>${item.date}</p>
            </div>
        `).join('');
    } catch (error) {
        console.error('Error loading news:', error);
    }
}

// 4. EVENTS
async function loadEvents() {
    try {
        const mockEvents = [
            { title: 'Full Stack Workshop', date: 'Mar 1, 2026', time: '6:00 PM' },
            { title: 'React Meetup', date: 'Mar 8, 2026', time: '7:00 PM' },
            { title: 'Lightning Talks', date: 'Mar 15, 2026', time: '5:30 PM' },
            { title: 'Hackathon Planning', date: 'Mar 22, 2026', time: '6:00 PM' }
        ];
        
        const eventsFeed = document.getElementById('events-feed');
        eventsFeed.innerHTML = mockEvents.map(event => `
            <div class="event-item">
                <h4>${event.title}</h4>
                <div class="event-date">📅 ${event.date}</div>
                <p>🕐 ${event.time}</p>
            </div>
        `).join('');
    } catch (error) {
        console.error('Error loading events:', error);
    }
}

// 5. CLOCK & HISTORY FACTS
function updateClock() {
    const now = new Date();
    const timeString = now.toLocaleTimeString();
    const dateString = now.toLocaleDateString('en-US', { 
        weekday: 'long', 
        year: 'numeric', 
        month: 'long', 
        day: 'numeric' 
    });
    
    document.getElementById('current-time').textContent = timeString;
    document.getElementById('current-date').textContent = dateString;
}

function loadHistoryFact() {
    const historyFacts = [
        'On this day in 1993, Kosovo declared independence from Serbia.',
        'Today marks the birth of internet pioneer Vint Cerf (1943).',
        'On this day in 2001, Wikipedia was launched.',
        'March 1 is Women\'s Day celebration in many countries.',
        'Today is Leap Day! February 29 happens every 4 years.',
        'Open Source Day celebrated worldwide on this day.'
    ];
    
    const randomFact = historyFacts[Math.floor(Math.random() * historyFacts.length)];
    document.getElementById('history-fact').textContent = randomFact;
}

// 6. HACKERSPACE STATUS
function updateStatus() {
    const hour = new Date().getHours();
    const statusIndicator = document.getElementById('status-indicator');
    const statusText = document.getElementById('status-text');
    const statusDesc = document.getElementById('status-description');
    
    if (hour >= 18 && hour <= 23) {
        statusIndicator.className = 'status-indicator open';
        statusText.textContent = 'OPEN';
        statusDesc.textContent = 'The hackerspace is now open! Come join us.';
    } else if (hour >= 9 && hour <= 17) {
        statusIndicator.className = 'status-indicator busy';
        statusText.textContent = 'CLOSING SOON';
        statusDesc.textContent = 'The hackerspace opens at 6 PM.';
    } else {
        statusIndicator.className = 'status-indicator closed';
        statusText.textContent = 'CLOSED';
        statusDesc.textContent = 'The hackerspace is closed right now.';
    }
}

// Update last updated time
function updateLastUpdate() {
    const now = new Date().toLocaleTimeString();
    document.getElementById('last-update').textContent = now;
}

// Initialize all widgets
function initDashboard() {
    loadAirQuality();
    loadWeather();
    loadNews();
    loadEvents();
    updateClock();
    loadHistoryFact();
    updateStatus();
    updateLastUpdate();
    
    // Update clock every second
    setInterval(updateClock, 1000);
    
    // Refresh all data every 5 minutes
    setInterval(() => {
        loadAirQuality();
        loadWeather();
        loadEvents();
        updateStatus();
        updateLastUpdate();
    }, 300000);
}

// Start dashboard when DOM is ready
document.addEventListener('DOMContentLoaded', initDashboard);
