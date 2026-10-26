(function () {
  const key = 'ks-theme';
  const root = document.documentElement;
  const toggle = document.getElementById('themeToggle');

  const setTheme = (theme) => {
    root.setAttribute('data-theme', theme);
    localStorage.setItem(key, theme);
    if (toggle) toggle.textContent = theme === 'dark' ? '☀️' : '🌙';
  };

  setTheme(localStorage.getItem(key) || 'light');

  if (toggle) {
    toggle.addEventListener('click', () => {
      setTheme(root.getAttribute('data-theme') === 'dark' ? 'light' : 'dark');
    });
  }
})();
