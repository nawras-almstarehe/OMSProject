import React from 'react';
import CIcon from '@coreui/icons-react';
import {
  cilSpeedometer,
  cilTags,
  cilSettings,
  cilGroup,
  cilBriefcase,
} from '@coreui/icons';
import { cilShield, cilRecentActors, cilChalkboardTeacher } from '@coreui/icons-pro';
import { CNavItem, CNavTitle, CNavGroup } from '@coreui/react';
import { useTranslation } from 'react-i18next';

const Enum_Privileges = {
  None: 0,
  SystemManager: 1 << 0
};

const _nav = () => {
  const { t } = useTranslation();

  return [
    {
      component: CNavItem,
      name: t('dashboard'),
      to: '/dashboard',
      icon: <CIcon icon={cilSpeedometer} customClassName="nav-icon" />,
      badge: {
        color: 'info',
        text: t('new'),
      }
    },
    {
      component: CNavTitle,
      name: t('menu'),
    },
    {
      component: CNavItem,
      name: t('category'),
      to: '/menue/category',
      icon: <CIcon icon={cilTags} customClassName="nav-icon" />
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
          icon: <CIcon icon={cilGroup} customClassName="nav-icon" />,
          classCustom: "nav-link-costum",
        },
        {
          component: CNavItem,
          name: t('departments'),
          to: '/configuration/departments',
          icon: <CIcon icon={cilChalkboardTeacher} customClassName="nav-icon" />,
          classCustom: "nav-link-costum",
        },
        {
          component: CNavItem,
          name: t('positions'),
          to: '/configuration/positions',
          icon: <CIcon icon={cilBriefcase} customClassName="nav-icon" />,
          classCustom: "nav-link-costum",
        },
        {
          component: CNavItem,
          name: t('privileges'),
          to: '/configuration/privileges',
          icon: <CIcon icon={cilShield} customClassName="nav-icon" />,
          classCustom: "nav-link-costum",
        },
        {
          component: CNavItem,
          name: t('roles'),
          to: '/configuration/roles',
          icon: <CIcon icon={cilRecentActors} customClassName="nav-icon" />,
          classCustom: "nav-link-costum",
        },
      ],
      permission: Enum_Privileges.SystemManager
    },
  ];
};

export default _nav;
