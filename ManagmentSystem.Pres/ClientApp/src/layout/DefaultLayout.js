import React, { useEffect, useState } from 'react'
import { AppContent, AppSidebar, AppFooter, AppHeader } from '../components/index'
import { useTranslation } from 'react-i18next';

const DefaultLayout = () => {
  const { i18n } = useTranslation();
  const [isRTL, setIsRTL] = useState(false);

  useEffect(() => {
    const direction = i18n.dir(); // Get current language direction (ltr/rtl)
    document.documentElement.dir = direction; // Apply direction to <html>
    setIsRTL(direction === 'rtl');
  }, [i18n.language]);

  return (
    <div>
      <AppSidebar isRTL={isRTL} />
      <div className="wrapper d-flex flex-column min-vh-100">
        <AppHeader />
        <div className="body flex-grow-1">
          <AppContent />
        </div>
        <AppFooter />
      </div>
    </div>
  )
}

export default DefaultLayout
