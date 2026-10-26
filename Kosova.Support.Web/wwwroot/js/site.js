(() => {
  const root = document.documentElement;
  const key = 'kosova-theme';
  const saved = localStorage.getItem(key);
  if (saved) root.setAttribute('data-theme', saved);

  const button = document.getElementById('theme-toggle');
  if (button) {
    button.addEventListener('click', () => {
      const next = root.getAttribute('data-theme') === 'dark' ? 'light' : 'dark';
      root.setAttribute('data-theme', next);
      localStorage.setItem(key, next);
      button.textContent = next === 'dark' ? '☀️' : '🌙';
    });
  }
})();
