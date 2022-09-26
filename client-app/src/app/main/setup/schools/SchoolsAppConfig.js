import { lazy } from 'react';

const School = lazy(() => import('./School'));
const Schools = lazy(() => import('./Schools'));
const SchoolType = lazy(() => import('./type/SchoolType'));
const SchoolTypes = lazy(() => import('./type/SchoolTypes'));

const SchoolsAppConfig = {
  settings: {
    layout: {},
  },
  routes: [
    {
      path: 'setup/schools',
      element: <Schools />,
    },
    {
      path: 'setup/schools/:schoolId/*',
      element: <School />,
    },
    {
      path: 'setup/schooltypes',
      element: <SchoolTypes />,
    },
    {
      path: 'setup/schooltypes/:schoolTypeId/*',
      element: <SchoolType />,
    },
  ],
};

export default SchoolsAppConfig;
