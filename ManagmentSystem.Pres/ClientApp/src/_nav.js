import React from 'react'
import CIcon from '@coreui/icons-react'
import {
  cilSpeedometer,
  cilTags
} from '@coreui/icons'
import { CNavItem, CNavTitle } from '@coreui/react'
import { useTranslation } from 'react-i18next';

const _nav = () => {
  const { t } = useTranslation(); // Initialize translation hook

  return [
    {
      component: CNavItem,
      name: t('dashboard'), // Translate 'Dashboard'
      to: '/dashboard',
      icon: <CIcon icon={cilSpeedometer} customClassName="nav-icon" />,
      badge: {
        color: 'info',
        text: t('new'), // Translate 'NEW'
      },
    },
    {
      component: CNavTitle,
      name: t('menu'), // Translate 'Menue'
    },
    {
      component: CNavItem,
      name: t('category'), // Translate 'Category'
      to: '/menue/category',
      icon: <CIcon icon={cilTags} customClassName="nav-icon" />,
    },
  ];
};

export default _nav;
