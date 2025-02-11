import React from 'react'
import CIcon from '@coreui/icons-react'
import {
  cilSpeedometer,
  cilTags,
  cilSettings
} from '@coreui/icons'
import { CNavItem, CNavTitle, CNavGroup } from '@coreui/react'
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
    {
      component: CNavGroup,
      name: t('configuration'),
      to: '/configuration',
      icon: <CIcon icon={cilSettings} customClassName="nav-icon" />,
      items: [
        {
          component: CNavItem,
          name: t('users'),
          to: '/configuration/users',
        },
      ],
    },
  ];
};

export default _nav;
