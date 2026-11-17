import i18n from 'i18next';
import { initReactI18next } from 'react-i18next';
import Backend from 'i18next-http-backend'; // ✅ Load translations from files
import LanguageDetector from 'i18next-browser-languagedetector'; // ✅ Detect user language

i18n
  .use(Backend) // Load translations from JSON files
  .use(LanguageDetector) // Detect browser language
  .use(initReactI18next)
  .init({
    fallbackLng: 'en', // Default language
    debug: false, // Set to false in production
    backend: {
      loadPath: '/locales/{{lng}}.json', // ✅ Load correct translation file
    },
    interpolation: {
      escapeValue: false, // React already escapes values
    },
  });

export default i18n;
