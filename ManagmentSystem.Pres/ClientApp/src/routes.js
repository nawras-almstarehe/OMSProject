import React from 'react';
import { useTranslation } from 'react-i18next';

const Dashboard = React.lazy(() => import('./views/dashboard/Dashboard'));
const Category = React.lazy(() => import('./views/menue/Category'));
const Users = React.lazy(() => import('./views/configuration/Users'));
const Departments = React.lazy(() => import('./views/configuration/Departments'));
const Positions = React.lazy(() => import('./views/configuration/Positions'));
const Roles = React.lazy(() => import('./views/configuration/Roles'));
const Privileges = React.lazy(() => import('./views/configuration/Privileges'));

const routes = () => {
  const { t } = useTranslation();
  return [
    { path: '/', exact: true, name: t('home') },
    { path: '/dashboard', name: t('dashboard'), element: Dashboard },
    { path: '/menue', name: t('menu'), element: Category, exact: true },
    { path: '/menue/category', name: t('category'), element: Category },
    { path: '/configuration', name: t('configuration'), element: Users, exact: true },
    { path: '/configuration/users', name: t('users'), element: Users },
    { path: '/configuration/departments', name: t('users'), element: Departments },
    { path: '/configuration/positions', name: t('positions'), element: Positions },
    { path: '/configuration/roles', name: t('roles'), element: Roles },
    { path: '/configuration/privileges', name: t('privileges'), element: Privileges },
  ]
}

export default routes;
