(function () {
  function byId(id) { return document.getElementById(id); }
  function qs(sel, el) { return (el || document).querySelector(sel); }
  function qsa(sel, el) { return Array.from((el || document).querySelectorAll(sel)); }

  function onlyDigits(value) { return String(value || '').replace(/\D+/g, ''); }
  function buildWhatsAppLink(number, presetText) {
    const n = onlyDigits(number);
    const text = encodeURIComponent(presetText || SITE.whatsappMessage || `Hello ${SITE.name}, I would like to book a cleaning.`);
    return `https://wa.me/${n}?text=${text}`;
  }

  function setContactLinks() {
    const tel = `tel:${onlyDigits(SITE.phone)}`;
    const wapp = buildWhatsAppLink(SITE.whatsapp);

    const pairs = [
      ['header-whatsapp', 'href', wapp],
      ['header-call', 'href', tel],
      ['hero-whatsapp', 'href', wapp],
      ['hero-call', 'href', tel],
      ['contact-whatsapp', 'href', wapp],
      ['contact-call', 'href', tel],
      ['whatsapp-fab', 'href', wapp],
      ['contact-phone-text', 'href', tel],
    ];
    pairs.forEach(([id, attr, val]) => { const el = byId(id); if (el) el.setAttribute(attr, val); });

    const phoneText = byId('contact-phone-text');
    if (phoneText) phoneText.textContent = SITE.phone;

    const address = byId('contact-address');
    if (address) address.textContent = SITE.address;

    const hours = byId('contact-hours');
    if (hours) hours.textContent = SITE.hours;
  }

  function setSiteIdentity() {
    qsa('[data-site-name]').forEach(el => el.textContent = SITE.name);
    const brandMark = qs('.brand-mark');
    if (brandMark) {
      const letters = (SITE.name || '').replace(/[^A-Za-z]/g, '').slice(0, 2).toUpperCase() || 'AR';
      brandMark.textContent = letters;
    }
    document.title = `${SITE.name} Cleaning Services | Professional Residential & Commercial Cleaning`;

    const heroTitle = qs('[data-hero-title]');
    const heroSubtitle = qs('[data-hero-subtitle]');
    if (CONTENT.hero?.title && heroTitle) heroTitle.textContent = CONTENT.hero.title;
    if (CONTENT.hero?.subtitle && heroSubtitle) heroSubtitle.textContent = CONTENT.hero.subtitle;

    const year = qs('[data-year]');
    if (year) year.textContent = new Date().getFullYear();
  }

  function renderServices() {
    const container = qs('[data-services-grid]');
    if (!container) return;
    container.innerHTML = '';
    (CONTENT.services || []).forEach(service => {
      const card = document.createElement('article');
      card.className = 'service-card';
      card.innerHTML = `
        <h3>${service.title}</h3>
        <p>${service.description || ''}</p>
      `;
      container.appendChild(card);
    });
  }

  function renderGallery() {
    const grid = qs('[data-gallery-grid]');
    if (!grid) return;
    grid.innerHTML = '';
    (CONTENT.gallery || []).forEach((src, idx) => {
      const img = document.createElement('img');
      img.loading = 'lazy';
      img.src = src;
      img.alt = `Gallery image ${idx + 1}`;
      grid.appendChild(img);
    });
  }

  function renderTestimonials() {
    const track = qs('[data-slider-track]');
    if (!track) return;
    track.innerHTML = '';
    (CONTENT.testimonials || []).forEach(t => {
      const slide = document.createElement('div');
      slide.className = 'slide';
      slide.innerHTML = `
        <figure class="testimonial">
          <div class="rating">${'★'.repeat(t.rating || 5)}${'☆'.repeat(Math.max(0, 5 - (t.rating || 5)))}</div>
          <blockquote>${t.text}</blockquote>
          <figcaption>— ${t.name}${t.location ? `, ${t.location}` : ''}</figcaption>
        </figure>
      `;
      track.appendChild(slide);
    });

    initSlider(track);
  }

  function initSlider(track) {
    let index = 0;
    const slides = qsa('.slide', track);
    function go(i) {
      if (!slides.length) return;
      index = (i + slides.length) % slides.length;
      track.style.transform = `translateX(-${index * 100}%)`;
    }
    const prev = qs('[data-prev]');
    const next = qs('[data-next]');
    if (prev) prev.addEventListener('click', () => go(index - 1));
    if (next) next.addEventListener('click', () => go(index + 1));

    let timer = setInterval(() => go(index + 1), 6000);
    const slider = track.closest('[data-slider]');
    if (slider) {
      slider.addEventListener('mouseenter', () => clearInterval(timer));
      slider.addEventListener('mouseleave', () => { timer = setInterval(() => go(index + 1), 6000); });
    }
  }

  function renderMap() {
    if (!SITE.mapEmbedUrl) return;
    const mapCard = qs('.map-card');
    const placeholder = qs('.map-embed', mapCard);
    if (!mapCard || !placeholder) return;
    const iframe = document.createElement('iframe');
    iframe.src = SITE.mapEmbedUrl;
    iframe.loading = 'lazy';
    iframe.referrerPolicy = 'no-referrer-when-downgrade';
    iframe.setAttribute('allowfullscreen', '');
    iframe.className = 'map-iframe';
    mapCard.replaceChild(iframe, placeholder);
  }

  function initNav() {
    const nav = qs('.nav');
    const toggle = qs('[data-nav-toggle]');
    const menu = qs('[data-nav-menu]');
    if (!nav || !toggle || !menu) return;

    toggle.addEventListener('click', () => {
      const open = nav.hasAttribute('data-open');
      nav.toggleAttribute('data-open');
      toggle.setAttribute('aria-expanded', String(!open));
    });

    qsa('a[href^="#"]', menu).forEach(a => {
      a.addEventListener('click', () => {
        nav.removeAttribute('data-open');
        toggle.setAttribute('aria-expanded', 'false');
      });
    });

    // Smooth scroll with header offset
    qsa('a[href^="#"]').forEach(a => {
      a.addEventListener('click', (e) => {
        const hash = a.getAttribute('href');
        if (!hash || hash === '#' || hash.length < 2) return;
        const target = byId(hash.slice(1));
        if (target) {
          e.preventDefault();
          const header = qs('[data-header]');
          const headerH = header ? header.getBoundingClientRect().height : 0;
          const y = target.getBoundingClientRect().top + window.scrollY - (headerH + 8);
          window.scrollTo({ top: y, behavior: 'smooth' });
          history.pushState(null, '', hash);
        }
      });
    });
  }

  document.addEventListener('DOMContentLoaded', function () {
    try {
      setSiteIdentity();
      setContactLinks();
      renderServices();
      renderGallery();
      renderTestimonials();
      renderMap();
      initNav();
    } catch (err) {
      console.error('Initialization error', err);
    }
  });
})();