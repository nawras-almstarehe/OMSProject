import React from 'react'

const Dashboard = React.lazy(() => import('./views/dashboard/Dashboard'))
const Category = React.lazy(() => import('./views/menue/Category'))
const Announcement = React.lazy(() => import('./views/menue/Announcement'))
const LearningMaterials = React.lazy(() => import('./views/menue/LearningMaterials'))
const Teachers = React.lazy(() => import('./views/menue/Teachers'))
const Students = React.lazy(() => import('./views/menue/Students'))
const ScheduleTimeTable = React.lazy(() => import('./views/menue/ScheduleTimeTable'))
const TeachingAndClasses = React.lazy(() => import('./views/menue/TeachingAndClasses'))
const Quizes = React.lazy(() => import('./views/menue/Quizes'))
const Grades = React.lazy(() => import('./views/menue/Grades'))

const routes = [
  { path: '/', exact: true, name: 'Home' },
  { path: '/dashboard', name: 'Dashboard', element: Dashboard },
  { path: '/menue', name: 'menue', element: Category, exact: true },
  { path: '/menue/category', name: 'Category', element: Category },
  { path: '/menue/announcement', name: 'Announcement', element: Announcement },
  { path: '/menue/learningMaterials', name: 'Learning Materials', element: LearningMaterials },
  { path: '/menue/Teachers', name: 'Teachers', element: Teachers },
  { path: '/menue/Students', name: 'Students', element: Students },
  { path: '/menue/ScheduleTimeTable', name: 'Schedule & Time Table', element: ScheduleTimeTable },
  { path: '/menue/TeachingAndClasses', name: 'Teaching And Classes', element: TeachingAndClasses },
  { path: '/menue/Quizes', name: 'Quizes', element: Quizes },
  { path: '/menue/Grades', name: 'Grades', element: Grades },
]

export default routes
