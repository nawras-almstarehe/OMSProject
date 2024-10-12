import React from 'react'
import CIcon from '@coreui/icons-react'
import {
  cilSpeedometer,
  cilCalendar,
  cilBullhorn,
  cilBook,
  cilUser,
  cilGroup,
  cilColumns,
  cilPenAlt,
  cilTask,
  cilColorBorder,
} from '@coreui/icons'
import { CNavItem, CNavTitle } from '@coreui/react'

const _nav = [
  {
    component: CNavItem,
    name: 'Dashboard',
    to: '/dashboard',
    icon: <CIcon icon={cilSpeedometer} customClassName="nav-icon" />,
    badge: {
      color: 'info',
      text: 'NEW',
    },
  },
  {
    component: CNavTitle,
    name: 'Menue',
  },
  {
    component: CNavItem,
    name: 'Calender',
    to: '/menue/calender',
    icon: <CIcon icon={cilCalendar} customClassName="nav-icon" />,
  },
  {
    component: CNavItem,
    name: 'Announcement',
    to: '/menue/announcement',
    icon: <CIcon icon={cilBullhorn} customClassName="nav-icon" />,
  },
  {
    component: CNavItem,
    name: 'Learning Materials',
    to: '/menue/learningMaterials',
    icon: <CIcon icon={cilBook} customClassName="nav-icon" />,
  },
  {
    component: CNavItem,
    name: 'Teachers',
    to: '/menue/Teachers',
    icon: <CIcon icon={cilUser} customClassName="nav-icon" />,
  },
  {
    component: CNavItem,
    name: 'Students',
    to: '/menue/Students',
    icon: <CIcon icon={cilGroup} customClassName="nav-icon" />,
  },
  {
    component: CNavItem,
    name: 'Schedule & Time Table',
    to: '/menue/ScheduleTimeTable',
    icon: <CIcon icon={cilColumns} customClassName="nav-icon" />,
  },
  {
    component: CNavItem,
    name: 'Teaching And Classes',
    to: '/menue/TeachingAndClasses',
    icon: <CIcon icon={cilPenAlt} customClassName="nav-icon" />,
  },
  {
    component: CNavItem,
    name: 'Quizes',
    to: '/menue/Quizes',
    icon: <CIcon icon={cilTask} customClassName="nav-icon" />,
  },
  {
    component: CNavItem,
    name: 'Grades',
    to: '/menue/Grades',
    icon: <CIcon icon={cilColorBorder} customClassName="nav-icon" />,
  },
]

export default _nav
