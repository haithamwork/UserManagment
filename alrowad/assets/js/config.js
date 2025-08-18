/* Editable site configuration for ALROWAD */
const SITE = {
  name: "ALROWAD",
  phone: "+966500000000",
  whatsapp: "+966500000000",
  email: "info@alrowad.example",
  address: "Riyadh, Saudi Arabia",
  hours: "24/7",
  whatsappMessage: "Hello ALROWAD, I would like to book a cleaning.",
  mapEmbedUrl: "",
};

const CONTENT = {
  lang: 'en',
  i18n: {
    en: {
      a11y: { skip: 'Skip to content' },
      map: { placeholder: 'Map placeholder' },
      nav: { home: 'Home', services: 'Services', about: 'About', gallery: 'Gallery', testimonials: 'Testimonials', contact: 'Contact' },
      cta: { whatsapp: 'WhatsApp', call: 'Call', bookWhatsApp: 'Book on WhatsApp', callNow: 'Call Now' },
      servicesSection: { title: 'Our Services', subtitle: 'Flexible, high-quality cleaning tailored to your needs.' },
      gallerySection: { title: 'Gallery', subtitle: 'Recent work and results.' },
      testimonialsSection: { title: 'What our clients say', subtitle: 'Real feedback from happy customers.' },
      contactSection: { title: 'Get in touch', subtitle: 'We respond quickly on WhatsApp and phone.', phone: 'Phone', address: 'Address', hours: 'Hours', chatWhatsapp: 'Chat on WhatsApp', callUs: 'Call us' },
      footer: { rights: 'All rights reserved.' },
      about: {
        title: 'About',
        body: 'We provide professional cleaning for homes, offices, and facilities. Our trained team uses safe materials and proven processes to deliver excellent results—every time.',
        bullets: [
          'Experienced and vetted staff',
          'Eco-friendly materials and methods',
          'Transparent pricing, no surprises',
          'On time, every time'
        ],
        whyTitle: 'Why choose us',
        features: ['24/7 availability', 'Custom schedules', 'Insured & professional', 'Satisfaction guaranteed']
      }
    },
    ar: {
      a11y: { skip: 'تخطّي إلى المحتوى' },
      map: { placeholder: 'خريطة' },
      nav: { home: 'الرئيسية', services: 'الخدمات', about: 'من نحن', gallery: 'المعرض', testimonials: 'آراء العملاء', contact: 'اتصل بنا' },
      cta: { whatsapp: 'واتساب', call: 'اتصال', bookWhatsApp: 'احجز عبر واتساب', callNow: 'اتصل الآن' },
      servicesSection: { title: 'خدماتنا', subtitle: 'تنظيف عالي الجودة مرن ومناسب لاحتياجاتك.' },
      gallerySection: { title: 'المعرض', subtitle: 'أعمالنا ونتائجنا الأخيرة.' },
      testimonialsSection: { title: 'ماذا يقول عملاؤنا', subtitle: 'آراء حقيقية من عملاء سعداء.' },
      contactSection: { title: 'تواصل معنا', subtitle: 'نستجيب سريعًا عبر الواتساب والهاتف.', phone: 'الهاتف', address: 'العنوان', hours: 'الساعات', chatWhatsapp: 'تحدث عبر الواتساب', callUs: 'اتصل بنا' },
      footer: { rights: 'جميع الحقوق محفوظة.' },
      about: {
        title: 'من نحن',
        body: 'نقدم خدمات تنظيف احترافية للمنازل والمكاتب والمرافق باستخدام مواد آمنة وطرق فعّالة لضمان نتائج ممتازة في كل مرة.',
        bullets: [
          'فريق ذو خبرة ومُعتمد',
          'مواد وأساليب صديقة للبيئة',
          'أسعار واضحة بدون مفاجآت',
          'دقة في المواعيد دائمًا'
        ],
        whyTitle: 'لماذا نحن',
        features: ['متاحون 24/7', 'جداول مرنة', 'فريق محترف ومؤمَّن', 'رضاكم مضمون']
      }
    }
  },
  hero: {
    title: "Professional Cleaning Services",
    subtitle: "Residential and commercial cleaning. Trusted team. Clear pricing.",
  },
  services: [
    { title: "Home Cleaning", description: "Regular and deep cleaning for apartments and villas." },
    { title: "Office Cleaning", description: "Daily, weekly, or monthly office cleaning schedules." },
    { title: "Move-in / Move-out", description: "Full property cleaning to handover standards." },
    { title: "Carpet Cleaning", description: "Steam and dry carpet care to remove stains and odors." },
    { title: "Sofa & Upholstery", description: "Fabric-safe washing and sanitizing for furniture." },
    { title: "Glass & Windows", description: "Streak-free interior and exterior window cleaning." },
    { title: "Kitchen & Appliances", description: "Grease removal and deep cleaning for appliances." },
    { title: "Post-Construction", description: "Dust removal and detailing after renovation works." },
    { title: "Disinfection", description: "Certified sanitizing for homes and offices." },
  ],
  testimonials: [
    { name: "Aisha", location: "Riyadh", rating: 5, text: "Excellent deep cleaning, very professional team!" },
    { name: "Omar", location: "Jeddah", rating: 5, text: "On time and great results. Highly recommend." },
    { name: "Fatimah", location: "Dammam", rating: 4, text: "Responsive on WhatsApp and good pricing." },
  ],
  gallery: [
    "https://images.unsplash.com/photo-1585421514738-01798e348b17?w=1200&auto=format&fit=crop&q=60",
    "https://images.unsplash.com/photo-1581578731548-c64695cc6952?w=1200&auto=format&fit=crop&q=60",
    "https://images.unsplash.com/photo-1556228453-efd1e3f0c45e?w=1200&auto=format&fit=crop&q=60",
    "https://images.unsplash.com/photo-1487014679447-9f8336841d58?w=1200&auto=format&fit=crop&q=60",
    "https://images.unsplash.com/photo-1533777419517-3e4017e2e15f?w=1200&auto=format&fit=crop&q=60",
    "https://images.unsplash.com/photo-1501045661006-fcebe0257c3f?w=1200&auto=format&fit=crop&q=60",
  ],
};