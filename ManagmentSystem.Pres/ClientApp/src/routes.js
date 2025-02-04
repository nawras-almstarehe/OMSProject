import React from 'react'
import { useTranslation } from 'react-i18next';

const Dashboard = React.lazy(() => import('./views/dashboard/Dashboard'))
const Category = React.lazy(() => import('./views/menue/Category'))

const routes = () => {
  const { t } = useTranslation();
  return [
    { path: '/', exact: true, name: 'Home' },
    { path: '/dashboard', name: t('dashboard'), element: Dashboard },
    { path: '/menue', name: t('menu'), element: Category, exact: true },
    { path: '/menue/category', name: t('category'), element: Category },
  ]
}

export default routes
