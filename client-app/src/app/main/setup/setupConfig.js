import { lazy } from 'react';
import { Navigate } from 'react-router-dom';

const School = lazy(() => import('./schools/School'));
const Schools = lazy(() => import('./schools/Schools'));
const SchoolType = lazy(() => import('./schools/type/SchoolType'));
const SchoolTypes = lazy(() => import('./schools/type/SchoolTypes'));

const SetupConfig = {
  settings: {
    layout: {},
  },
  routes: [
    {
      path: 'schools',
      element: <Schools />,
    },
    {
      path: 'schools/:schoolId/*',
      element: <School />,
    },
    {
      path: 'schooltypes',
      element: <SchoolTypes />,
    },
    {
      path: 'schooltypes/:schoolTypeId/*',
      element: <SchoolType />,
    },
  ],
};

export default SetupConfig;
